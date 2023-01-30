using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour, ISaveSettingInstallable
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private NameVolume _nameVolume;

    public void ChangeVolume() => ChangeVolume(_nameVolume);

    private void ChangeVolume(NameVolume nameVolume, float value)
    {
        _audioMixer.audioMixer.SetFloat(nameVolume.ToString(), Mathf.Lerp(-80, 0, value));
        PlayerPrefs.SetFloat(nameVolume.ToString(), _slider.value);
    }

    public void Init()
    {
        var currentVolume = PlayerPrefs.GetFloat(_nameVolume.ToString(), 1);
        ChangeVolume(_nameVolume, currentVolume);
        _slider.value = currentVolume;
    }

    private void ChangeVolume(NameVolume nameVolume) => ChangeVolume(nameVolume, _slider.value);

    private enum NameVolume
    {
        BackGroundVolume,
        EnemyVolume,
        AllVolume,
        PlayerVolume
    }
}