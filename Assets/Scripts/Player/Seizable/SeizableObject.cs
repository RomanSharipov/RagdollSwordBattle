using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SeizableObject : MonoBehaviour
{
    private const float MinDirectionMagnitude = 0.5f;
    private const float CloseDistanceSpeed = 150f;

    [Header("Speed Fly Setting")]
    [SerializeField, Min(0)] private float _speedFly = 65;
    [SerializeField, Min(0)] private float _slowDistant = 10f;
    [Tooltip("Curve slow fly about slow distant")]
    [SerializeField] private AnimationCurve _curveSpeedByDistance;

    [Header("Ragdoll Setting")]
    [SerializeField, Min(0)] private float _ragdollLimbWeigh = 0.3f;
    [SerializeField, Min(0)] private float _rotationToZeroTime = 0.3f;
    [Header("Ragdoll")] [SerializeField] private Rigidbody _grabHand;
    [SerializeField] private Rigidbody _grabPoint;
    [SerializeField] private Transform _hips;

    [SerializeField] private Particle _particle;


    private Rigidbody[] _rigidbodies;
    private Collider[] _colliders;
    private Coroutine _moveCoroutine;
    private Tween _rotationTween;


    private void Awake()
    {
        CacheCollidersAndRigidbodies();
    }

    public void Seize()
    {
        SetCollisionState(false);
        FlyTowardsObject();
        _particle.Play();
        _particle.isIgnoreCollision = true;
    }

    public void Release()
    {
        _particle.isIgnoreCollision = false;
        _grabHand.velocity = Vector3.zero;
        _rotationTween = _hips.DORotate(new Vector3(0f, 180f, 0f), _rotationToZeroTime);

        DisableMoveCoroutine();

        SetCollisionState(true);
    }

    public void DisableMoveCoroutine()
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
    }

    private void CacheCollidersAndRigidbodies()
    {
        var rigidbodiesInChildren = GetComponentsInChildren<Rigidbody>();
        _rigidbodies = rigidbodiesInChildren.Where(rigidbody =>
            _grabHand.gameObject != rigidbody.gameObject
            && rigidbody.gameObject != gameObject
            && rigidbody.gameObject.TryGetComponent(out PlayerHealth health) == false
        ).ToArray();

        var collidersInChildren = GetComponentsInChildren<Collider>();
        _colliders = collidersInChildren.Where(collider =>
            _grabHand.gameObject != collider.gameObject
            && collider.gameObject != gameObject
            && collider.gameObject.TryGetComponent(out PlayerHealth health) == false
        ).ToArray();

        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            if (rigidbody.TryGetComponent(out CharacterJoint characterJoint))
                characterJoint.massScale = _ragdollLimbWeigh;
        }
    }

    private void FlyTowardsObject()
    {
        DisableMoveCoroutine();
        _moveCoroutine = StartCoroutine(MoveTowardsJoint());
    }

    private void SetCollisionState(bool collisionState)
    {
        _grabHand.useGravity = collisionState;

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].useGravity = false;
            _colliders[i].isTrigger = !collisionState;

            _rigidbodies[i].velocity = Vector3.zero;
        }
    }

    private IEnumerator MoveTowardsJoint()
    {
        RotateMesh();

        while (enabled)
        {
            var direction = (_grabPoint.position - _grabHand.position).normalized;
            if (direction.magnitude < MinDirectionMagnitude)
                direction *= MinDirectionMagnitude;

            //Last Nikita speed. if you don't need this, Delete Ёпта
            // var speed = Vector3.Distance(_grabPoint.position, _grabHand.position) < 1f ? CloseDistanceSpeed : _seizeSpeed;\
            var distans = Vector3.Distance(_grabPoint.position, _grabHand.position);
            var speed = _speedFly * _curveSpeedByDistance.Evaluate(distans / _slowDistant);
            _grabHand.MovePosition(_grabHand.position + direction * (speed * Time.deltaTime));
            yield return null;
        }
    }

    private void RotateMesh()
    {
        _rotationTween?.Kill();
        _hips.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));

        var lookDirection = _grabPoint.transform.position - _hips.position;
        _hips.rotation = Quaternion.FromToRotation(Vector3.up, lookDirection);

        float zAngle = _hips.transform.rotation.eulerAngles.z;
        var percent = Mathf.Abs((float) Math.Sin(Math.PI / 180f * _hips.transform.rotation.eulerAngles.z));

        if (percent > 0.80f)
            zAngle += Mathf.Lerp(0f, 180f, percent);

        _hips.rotation = Quaternion.Euler(_hips.transform.rotation.eulerAngles.x,
            _hips.transform.rotation.eulerAngles.y + 180f,
            zAngle);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(_hips.transform.position, _hips.transform.position + _hips.transform.forward * 5f, Color.red);
        Debug.DrawLine(_hips.transform.position, _hips.transform.position + _hips.transform.up * 5f, Color.yellow);
    }
}