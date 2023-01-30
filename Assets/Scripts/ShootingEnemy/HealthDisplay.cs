using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField, Min(0f)] private float _animationTime = 0.3f; 

    private Health _health;
    private Camera _camera;
    
    private void OnEnable()
    {
        _camera = Camera.main;
    }

    private void OnDisable() => _health.HealthChanged -= ChangeHealth;

    private void Update()
    {
        transform.position = _camera.WorldToScreenPoint(_health.transform.position);
    }

    public void Init(Health health)
    {
        _health = health;
        ChangeHealth();
        _health.HealthChanged += ChangeHealth;
        gameObject.SetActive(true);
    }
    
    private void ChangeHealth() => _image.DOFillAmount((float)_health.HealthCount / _health.MaxHealth, _animationTime);
}