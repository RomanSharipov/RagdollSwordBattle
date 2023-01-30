using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _speedBar;
    [Header("Slider")]
    [SerializeField] private Slider _sliderMain;
    [SerializeField] private Slider _sliderBackground;

    private bool _settingValueBar;

    private void Awake()
    {
        StartSetBarValue();
    }

    private void OnEnable()
    {
        _health.HealthChanged += StartSetBarValue;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= StartSetBarValue;
    }

    private void StartSetBarValue()
    {
        _sliderMain.maxValue = _health.MaxHealth;
        _sliderMain.value = _health.HealthCount;

        _sliderBackground.maxValue = _sliderMain.maxValue;

        if (_settingValueBar == false)
            StartCoroutine(SetValueBar());
    }

    private IEnumerator SetValueBar()
    {
        _settingValueBar = true;

        while (Mathf.Abs(_sliderBackground.value - _sliderMain.value) > 0.001f)
        {
            _sliderBackground.value = Mathf.Lerp(_sliderBackground.value, _sliderMain.value, _speedBar * Time.deltaTime);
            yield return null;
        }

        _sliderBackground.value = _sliderMain.value;

        _settingValueBar = false;
    }
}
