using System;
using System.Collections;
using TimeScale;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    private const float DeltaTimeValue = 0.02f;

    [SerializeField] private TimeScaleObserver _timeScaleObserver;
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;
    [SerializeField] private float _minDistanceToTargetPoint = 0.01f;

    private Transform[] _points;
    private int _currentPoint;
    private Transform _targetPoint;
    private float _currentDistanceToTargetPoint;

    private float _currentSpeed;

    private void OnEnable() => _timeScaleObserver.Subscribe();

    private void OnDisable() => _timeScaleObserver.UnSubscribe();

    private void Start()
    {
        _currentSpeed = _speed;
        _currentSpeed = _speed * _timeScaleObserver.GetCurrentTimeScale();
        _timeScaleObserver.OnTimeScaleChanged(x => _currentSpeed = _speed * x);
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }

        _targetPoint = _points[_currentPoint];

        StartCoroutine(UpdateOnRealTime());
    }

    private IEnumerator UpdateOnRealTime()
    {
        while (true)
        {
            MoveOnPoints();

            yield return new WaitForSecondsRealtime(DeltaTimeValue);
        }
    }

    private void MoveOnPoints()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, _targetPoint.position, _currentSpeed * DeltaTimeValue);

        TrySetNewTargetPoint();
    }

    private void TrySetNewTargetPoint()
    {
        _currentDistanceToTargetPoint = Vector3.Distance(transform.position, _targetPoint.position);

        if (_currentDistanceToTargetPoint < _minDistanceToTargetPoint)
            SetNewTargetPoint();
    }

    private void SetNewTargetPoint()
    {
        _currentPoint++;

        if (_currentPoint == _points.Length)
            _currentPoint = 0;

        _targetPoint = _points[_currentPoint];
    }
}