using UnityEngine;

public class SetterDefaultSetting : MonoBehaviour
{
    [SerializeField] private Panel _settingParent;

    private void Awake()
    {
        var settingInstallable = _settingParent.GetComponentsInChildren<ISaveSettingInstallable>();

        foreach (var saveSettingInstallable in settingInstallable)
        {
            saveSettingInstallable.Init();
        }
    }
}
