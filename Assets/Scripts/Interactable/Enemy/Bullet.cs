using System.Collections;
using TimeScale;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : Damagable
{
    private const float FixedTimeScale = 0.02f;
    [Header("Bullet")] [SerializeField] private float _speed = 3;

    [SerializeField] private TimeScaleObserver _timeScaleObserver;
    [SerializeField] private LayerMask _destroyLayer;
    [SerializeField] private ParticleSystem _destroyParticle;

    private Rigidbody _rigidbody;
    private float _currentSpeed;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        Starts();
    }

    private void OnEnable() => _timeScaleObserver.Subscribe();

    private void OnDisable() => _timeScaleObserver.UnSubscribe();

    private void Starts()
    {
        _currentSpeed = _speed * _timeScaleObserver.GetCurrentTimeScale();
        _timeScaleObserver.OnTimeScaleChanged(x => _currentSpeed = _speed * x);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((_destroyLayer.value & (1 << collision.gameObject.layer)) != 0)
            Destroy();
    }

    public void StartMove(Vector3 direction) => StartCoroutine(UpdateOnRealTime(direction));

    private void Destroy()
    {
        Instantiate(_destroyParticle, transform.position, _destroyParticle.transform.rotation);
        Destroy(gameObject);
    }

    private IEnumerator UpdateOnRealTime(Vector3 direction)
    {
        var wait = new WaitForSecondsRealtime(FixedTimeScale);

        while (enabled)
        {
            MoveTowards(direction);
            yield return wait;
        }
    }

    private void MoveTowards(Vector3 direction) =>
        _rigidbody.MovePosition(transform.position + direction * (FixedTimeScale * _currentSpeed));
}