using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TimeScale;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private const float TimeFixedDeltaTime = 0.02f;

    [SerializeField] private List<Point> _points = new List<Point>();
    [SerializeField] private TimeScaleObserver _timeScaleObserver;
    [SerializeField, Min(0)] private float _speed = 3f;
    [SerializeField, Min(0)] private float _rotationTime = 0.5f;
    [SerializeField] private bool _isRotating = true;
    [SerializeField] private AttackNearbyPlayer _attackNearbyPlayer ;

    private Vector3 _currentPoint;
    private int _currentPointIndex = 0;
    private float _lastAttackTime;
    private float _currentSpeed;

    private void OnEnable() => _timeScaleObserver.Subscribe();

    private void OnDisable() => _timeScaleObserver.UnSubscribe();

    private void Start()
    {
        _currentPoint = _points[0].transform.position;
        StartPatrol();
        _currentSpeed = _timeScaleObserver.GetCurrentTimeScale();
        _timeScaleObserver.OnTimeScaleChanged(x => _currentSpeed = x * _speed);
        LookAtNewPoint();
    } 

    private void Update() 
    {
        if (_lastAttackTime <= 0)
        {
            Patroll();
            _lastAttackTime = TimeFixedDeltaTime;
        }
        _lastAttackTime -= Time.deltaTime;
    } 

    
    public void StopPatrol()
    {
        enabled = false;
    }

    public void SwithOffMovement()
    {
        StopPatrol();
        _attackNearbyPlayer?.gameObject.SetActive(false);
    }

    public void StartPatrol()
    {
        enabled = true;
    }
    
    private void TryUpdateCurrentPoint()
    {
        if (Vector3.Distance(transform.position, _currentPoint) <= 0.001f)
        {
            _currentPointIndex += 1;

            if (_currentPointIndex == _points.Count)
            {
                _currentPointIndex = 0;
            }

            _currentPoint = _points[_currentPointIndex].transform.position;

            if (_isRotating)
                LookAtNewPoint();
        }
    }
    
    protected virtual void LookAtNewPoint()
    {
        var lookPoint = new Vector3(_currentPoint.x, transform.position.y, transform.position.z);
        transform.DOLookAt(lookPoint, _rotationTime);
    }

    private void Patroll()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentPoint, _currentSpeed * TimeFixedDeltaTime);
        TryUpdateCurrentPoint();
    }
    
}
