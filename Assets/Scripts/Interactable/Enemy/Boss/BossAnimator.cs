using System;
using DG.Tweening;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;

    private readonly int TakeHit = Animator.StringToHash("TakeHIt");

    private void OnEnable() => _health.HealthChanged += PlayHit;

    private void OnDisable() => _health.HealthChanged -= PlayHit;

    private void PlayHit()
    {
        _animator.Play(TakeHit);
    }
}