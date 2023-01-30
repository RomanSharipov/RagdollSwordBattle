using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelLoadButton : MonoBehaviour
{
    private Button _levelLoadButton;

    private void Awake()
    {
        _levelLoadButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _levelLoadButton.onClick.AddListener(OnLevelLoadButtonClick);
    }

    private void OnDisable()
    {
        _levelLoadButton.onClick.RemoveListener(OnLevelLoadButtonClick);
    }

    private void OnLevelLoadButtonClick()
    {
        LevelLoad.Instance.LoadNewLevel();
    }
}
