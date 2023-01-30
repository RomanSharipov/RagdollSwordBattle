using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Weapon : MonoBehaviour
{
    [SerializeField, Min(0f)] private int _damage;
    [SerializeField, Min(0f)] private float _minHitVelocity = 5f;

    [Header("Hit effects")] [SerializeField]
    private ParticleSystem _wallHitEffect;

    [SerializeField] private ParticleSystem _enemyHitEffect;
    [SerializeField] private ParticleSystem _bossHitEffect;

    private float _invulnerabilityTime = 0.2f;
    private List<Health> _healths = new List<Health>();
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var collisionPoint = other.ClosestPointOnBounds(transform.position);

        if (other.TryGetComponent(out Health health) && _rigidbody.velocity.magnitude >= _minHitVelocity &&
            health is PlayerHealth == false && _healths.Contains(health) == false)
        {
            health.TakeDamage(_damage);
            _healths.Add(health);
            StartCoroutine(RemoveHealth(health));
            if (health is BossHealth)
            {
                Instantiate(_bossHitEffect, collisionPoint, quaternion.identity, health.transform);
                return;
            }
            if (health is BotHealth  || health is HostageHealth)
                InstantiateHitEffect(_enemyHitEffect, collisionPoint);
        }

        if (other.TryGetComponent(out Wall _))
        {
            InstantiateHitEffect(_wallHitEffect, collisionPoint);
        }
    }

    public IEnumerator RemoveHealth(Health health)
    {
        yield return new WaitForSecondsRealtime(_invulnerabilityTime);
        _healths.Remove(health);
    }

    private void InstantiateHitEffect(ParticleSystem hitEffect, Vector3 contactPoint)
    {
        Instantiate(hitEffect, contactPoint, hitEffect.transform.rotation);
    }
}