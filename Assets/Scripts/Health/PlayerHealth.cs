using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerHealth : Health
{
    [SerializeField] private Rigidbody _rigidbodyHips;
    [SerializeField] private ParticleSystem _hitParticle;

    public Rigidbody RigidbodyHips => _rigidbodyHips;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPlayerDamagable weapon))
        {
            TakeDamage(weapon.Damage);
            InstantiateHitParticle(other);
        }
    }

    private void InstantiateHitParticle(Collider other)
    {
        var collisionPoint = other.ClosestPointOnBounds(transform.position);

        Instantiate(_hitParticle, collisionPoint, _hitParticle.transform.rotation);
    }
}