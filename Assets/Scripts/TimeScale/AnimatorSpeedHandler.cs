using System;
using TimeScale;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorSpeedHandler : MonoBehaviour
{
    [SerializeField] private TimeScaleObserver _timeScaleObserver;
    private readonly int _speed = Animator.StringToHash("Speed");
    private Animator _animator;

    private float _currentСoeficent = 1;
    private void OnEnable() => _timeScaleObserver.Subscribe();

    private void OnDisable() => _timeScaleObserver.UnSubscribe();

    private void Start()
    {
        _animator = GetComponent<Animator>();
        SetSpeed(1);
        _timeScaleObserver.OnTimeScaleChanged(SetSpeed);
    }

    private void SetSpeed(float speed) => _animator.SetFloat(_speed, speed);
}