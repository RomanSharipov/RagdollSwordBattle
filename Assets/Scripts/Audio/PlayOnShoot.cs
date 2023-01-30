using UnityEngine;

public class PlayOnShoot : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private AudioSource _audio;

    private void OnEnable() => _gun.Attacked += OnShoot;
    private void OnDisable() => _gun.Attacked -= OnShoot;

    private void OnShoot() => _audio.Play();
}
