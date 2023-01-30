using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TimeScaleHandler : Singleton<TimeScaleHandler>
{
    private const float DefaultTimeScale = 1f;
    private const float DefaultFixedDeltaTime = 0.02f;

    private List<float> _timeScaleValues;

    public event UnityAction<float> TimeScaleChanged; 

    private void Start()
    {
        _timeScaleValues = new List<float>();
        _timeScaleValues.Add(DefaultTimeScale);
        
        Time.timeScale = DefaultTimeScale;
        Time.fixedDeltaTime = DefaultFixedDeltaTime * Time.timeScale;
    }

    public void SetSlowedTimeScale(float slowmoTimeScale)
    {
        if (slowmoTimeScale < 0f)
            throw new ArgumentOutOfRangeException($"{nameof(slowmoTimeScale)} can't be less, than 0! Value is {slowmoTimeScale}");
        
        _timeScaleValues.Add(slowmoTimeScale);
        SetTimeScale();
    }
    
    public void ResetTimeScale(float slowmoTimeScale)
    {
        _timeScaleValues.RemoveAll(value => value == slowmoTimeScale);
        SetTimeScale();
    }

    private void SetTimeScale()
    {
        var minTimeScale = _timeScaleValues.Min();
        Time.timeScale = minTimeScale;
        Time.fixedDeltaTime = DefaultFixedDeltaTime * Time.timeScale;
        TimeScaleChanged?.Invoke(minTimeScale);
    }
}
