using UnityEngine;

public class PlayOnDie : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private AudioSource _audio;

    private void OnEnable() => _health.Died += OnDie;

    private void OnDisable() => _health.Died -= OnDie;

    private void OnDie() => _audio.Play();
}