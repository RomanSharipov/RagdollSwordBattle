using UnityEngine;

public class PlayOnWin : MonoBehaviour
{
    [SerializeField] private GameStateHandler _gameStateHandler;
    [SerializeField] private AudioSource _audio;

    private void OnEnable() => _gameStateHandler.WinUIEnabled += OnWinUIEnabled;
    private void OnDisable() => _gameStateHandler.WinUIEnabled -= OnWinUIEnabled;

    private void OnWinUIEnabled() => _audio.Play();
}
