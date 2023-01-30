using System;
using System.Collections;
using System.Collections.Generic;
using TimeScale;
using UnityEngine;

public class DroppingTrapMovement : MonoBehaviour
{
    [SerializeField] private float _speedFalling = 0.2f;
    [SerializeField] private TimeScaleObserver _timeScaleObserver;
	private Rigidbody _rigidbody;

    private float _currentSpeed;
    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable() => _timeScaleObserver.Subscribe();

    private void OnDisable() => _timeScaleObserver.UnSubscribe();

    private void Start()
    {
        _currentSpeed = _speedFalling;
        _currentSpeed = _speedFalling * _timeScaleObserver.GetCurrentTimeScale();
        _timeScaleObserver.OnTimeScaleChanged(x => _currentSpeed = _speedFalling * x);
    }

    public void FixedUpdate() 
    {
        transform.Translate(0, -_currentSpeed, 0);
    }

    public void SwitchOnGravity()
    {
        _rigidbody.useGravity = true;
    }

    public void SwitchOffIsKinematic()
    {
        _rigidbody.isKinematic = false;
    }
}
