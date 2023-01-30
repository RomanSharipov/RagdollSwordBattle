using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleButton : MonoBehaviour, ISaveSettingInstallable
{
    [Header("Toggle buttons")]
    [SerializeField] private GameObject _enabledView;
    [SerializeField] private GameObject _disabledView;
    [Header("Audio settings")]
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private List<NameVolume> _nameVolumes;

    private Button _button;
    private bool _isEnabled = false;

    public bool IsEnabled => _isEnabled;

    public event Action ButtonClicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke();

        _isEnabled = !_isEnabled;
        ChangeButtonState();
    }

    private void ChangeButtonState()
    {
        if (_isEnabled)
        {
            ChangeVolume(1f);

            EnableButton();
            return;
        }

        ChangeVolume(0f);
        DisableButton();
    }

    private void EnableButton()
    {
        _disabledView.SetActive(false);
        _enabledView.SetActive(true);
    }

    private void DisableButton()
    {
        _enabledView.SetActive(false);
        _disabledView.SetActive(true);
    }

    private void ChangeVolume(float value)
    {
        var soundValue = value == 1 ? 0 : -80;

        foreach (var volume in _nameVolumes)
        {
            _audioMixer.audioMixer.SetFloat(volume.ToString(), soundValue);
            PlayerPrefs.SetFloat(volume.ToString(), value);
        }
    }

    public void Init()
    {
        foreach (var volume in _nameVolumes)
        {
            var currentVolume = PlayerPrefs.GetFloat(volume.ToString(), 1);
            _isEnabled = currentVolume != 1 ? false : true;
            ChangeButtonState();
        }
    }

    private enum NameVolume
    {
        BackGroundVolume,
        EnemyVolume,
        AllVolume,
        PlayerVolume
    }
}

public interface ISaveSettingInstallable
{
    void Init();
}
