using System;
using UnityEngine;

public class HostageHealth : Health
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _ragDollParts;
    [SerializeField] private UIQuotation _uiQuoteTemplate;
    private UIQuotation _uiQuote;
    
    public override void Die()
    {
        base.Die();
        RemoveQuotation();
        ResetVelocity();
        _animator.enabled = false;
    }

    private void ResetVelocity()
    {
        foreach (var ragDollPart in _ragDollParts)
        {
            ragDollPart.velocity = Vector3.zero;
            ragDollPart.angularVelocity = Vector3.zero;
        }
    }
    
    public void CreateQuotation(Transform canvasParent)
    {
        _uiQuote = Instantiate(_uiQuoteTemplate, canvasParent);
        _uiQuote.Init(this);
    }

    public void RemoveQuotation()
    {
        if (_uiQuote == null)
            return;

        Destroy(_uiQuote.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DroppingTrap trap))
            TakeDamage(trap.Damage);
    }
}
