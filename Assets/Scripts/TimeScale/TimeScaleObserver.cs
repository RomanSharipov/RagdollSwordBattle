using System;
using UnityEngine;

namespace TimeScale
{
    [Serializable]
    public class TimeScaleObserver
    {
        public const float DefaultTimeScale = 1;
        
        [Header("Present 0 - 1")] [SerializeField, Min(0)]
        
        private float _presentTimeScale = 0.5f;
        private Action<float> _changeSpeedAction;
        
        public float presetTimeScale => _presentTimeScale;

        public void Subscribe() => TimeScaleHandler.Instance.TimeScaleChanged += ChangeTimeScale;

        public void UnSubscribe()
        {
            if (TimeScaleHandler.Instance != null)
                TimeScaleHandler.Instance.TimeScaleChanged -= ChangeTimeScale;
        }


        public float GetCurrentTimeScale() => Time.timeScale < 1 ? _presentTimeScale : DefaultTimeScale;

        public void OnTimeScaleChanged(Action<float> action) => _changeSpeedAction = action;
        
        private void ChangeTimeScale(float timeScale) => _changeSpeedAction(timeScale < 1 ? _presentTimeScale : DefaultTimeScale);
    }
}