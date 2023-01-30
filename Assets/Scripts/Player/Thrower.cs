using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float multiplize;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPlayerDamagable _))
        {
            var triggerPoint = other.ClosestPointOnBounds(other.transform.position);
            var direction = (transform.position - triggerPoint).normalized;
            Throw(direction);
        }
    }

    private void Throw(Vector3 direction)
    {
        _rigidbody.AddForce(direction * multiplize, ForceMode.VelocityChange);
    }
}