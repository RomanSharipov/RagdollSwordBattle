using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerSeeker : MonoBehaviour
{
    [SerializeField] private float _angele = 45;
    [SerializeField] private float _distant = 7;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private LayerMask _triggerLayerMask;

    public event Action<Vector3> PlayerDetected;
    private Vector3 _shootDirection;

    private void Start() => _shootDirection = -transform.right;

    private void OnTriggerStay(Collider other)
    {
        var playerPosition = other.transform.position;
        var targetDirection = playerPosition - transform.position;
        var realAngle = Vector3.Angle(_shootDirection, targetDirection);
        
        if (realAngle <= _angele / 2)
        {
            if (Physics.Raycast(transform.position, targetDirection, out var hit, _distant, _triggerLayerMask))
            {
                if (0 != (_targetLayerMask.value & (1 << hit.collider.gameObject.layer)))
                {
                    Debug.DrawLine(transform.position, playerPosition);
                    PlayerDetected?.Invoke(new Vector3(hit.point.x, hit.point.y));
                }
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        _shootDirection = -transform.right;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, _shootDirection);
        Gizmos.color = Color.red;
        var upVector = Quaternion.Euler(0, 0, _angele/2) * _shootDirection;
        var downVector = Quaternion.Euler(0, 0, -_angele/2) * _shootDirection;
        Gizmos.DrawRay(transform.position,upVector * _distant);
        Gizmos.DrawRay(transform.position,downVector * _distant);
    }
    

    private void OnValidate() => _sphereCollider.radius = _distant;
#endif
}