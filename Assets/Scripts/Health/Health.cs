using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private int _maxHealth;
    [SerializeField] private DeathMaterial _deathMaterial = new DeathMaterial ();
    private int _currentHealth;
    private HealthDisplay _healthDisplay;
    private Collider _collider;

    public int HealthCount => _currentHealth;
    public int MaxHealth => _maxHealth;
    public bool IsAlive => _currentHealth > 0;

    public event Action HealthChanged;
    public event Action Died;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _collider = GetComponent<Collider>();
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException($"Damage can't be less, than 0!");

        _currentHealth -= damage;
        
        HealthChanged?.Invoke();

        if (_currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        _currentHealth = 0;
        _collider.enabled = false;
        HealthChanged?.Invoke();
        Died?.Invoke();
        if (_deathMaterial.IsMaterialChanged == false)
            return;
        _deathMaterial.SetDieMaterial();
    }

    public void TryCreateHealthDisplay(HealthDisplay healthDisplayTemplate, Transform canvasParent)
    {
        if (_maxHealth > 1)
        {
            
            _healthDisplay = Instantiate(healthDisplayTemplate, canvasParent);
            _healthDisplay.Init(this);
        }
    }

    public void TryRemoveHealthDisplay()
    {
        if (_healthDisplay != null)
            Destroy(_healthDisplay.gameObject);
    }
}