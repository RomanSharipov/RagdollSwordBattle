using UnityEngine;

public class SeekerGun : Gun
{
    [Header("PlayerSeeker")] 
    [SerializeField] private PlayerSeeker _playerSeeker;

    protected override void OnEnable() => _playerSeeker.PlayerDetected += TryAttack;

    protected override void OnDisable() => _playerSeeker.PlayerDetected -= TryAttack;
}