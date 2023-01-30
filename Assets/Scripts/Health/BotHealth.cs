using System;
using UnityEngine;


public class BotHealth : Health
{
    [Tooltip("Might be empty if no movement required!")] [SerializeField]
    private EnemyMovement _movement;

    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _ragDollParts;
    [SerializeField] private Rigidbody _hips;

    private bool _isFalling;
    private float _forcePower = 50f;
    private Vector3 _directionAddForceOnHit;

    protected Animator Animator => _animator;

    public event Action BotHit;
    public event Action<BotHealth> BotDied;

    private void OnTriggerEnter(Collider other)
    {
        _directionAddForceOnHit = (transform.position - other.ClosestPointOnBounds(transform.position)).normalized;

        if (other.TryGetComponent(out DroppingTrap trap))
            TakeDamage(trap.Damage);

        if (_isFalling && (other.TryGetComponent(out Wall wall) || other.TryGetComponent(out Damagable damagable)))
            Die();
    }

    public override void TakeDamage(int damage)
    {
        AddForceHips(_directionAddForceOnHit);
        base.TakeDamage(damage);

        BotHit?.Invoke();
    }

    public override void Die()
    {
        base.Die();
        _movement?.SwithOffMovement();
        ResetVelocity();

        _animator.enabled = false;
        BotDied?.Invoke(this);
    }

    public void FallDown()
    {
        _movement?.StopPatrol();
        ResetVelocity();

        _animator.enabled = false;
        _isFalling = true;
    }

    private void ResetVelocity()
    {
        foreach (var ragDollPart in _ragDollParts)
        {
            ragDollPart.velocity = Vector3.zero;
            ragDollPart.angularVelocity = Vector3.zero;
        }
    }

    private void AddForceHips(Vector3 direction)
    {
        if (_hips == null)
            return;
        ResetVelocity();
        if ((_directionAddForceOnHit.y < 0))
            _directionAddForceOnHit = new Vector3(_directionAddForceOnHit.x, _directionAddForceOnHit.y/5, _directionAddForceOnHit.z);

        foreach (var ragDollPart in _ragDollParts)
        {
            ragDollPart.AddForce(_directionAddForceOnHit * _forcePower, ForceMode.VelocityChange);
        }
    }
}