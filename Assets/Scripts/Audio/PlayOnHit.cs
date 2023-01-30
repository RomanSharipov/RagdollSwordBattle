using UnityEngine;

public class PlayOnHit : MonoBehaviour
{
    [SerializeField] private BossHealth _bossHealth;
    [SerializeField] private AudioSource _audio;

    private void OnEnable() => _bossHealth.BotHit += OnBotHit;

    private void OnDisable() => _bossHealth.BotHit -= OnBotHit;

    private void OnBotHit() => _audio.Play();
}
