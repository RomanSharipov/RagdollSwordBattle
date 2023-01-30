using TMPro;
using UnityEngine;

public class SetLevelNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _prefix = "Level ";

    private void Start()
    {
        var text = "Level ";
        if (PlayerPrefs.HasKey("CompletedLevels"))
            text += +PlayerPrefs.GetInt("CompletedLevels");
        else
            text += 1;
        _text.text = text;
    }
}