using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShootAnimator : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Vector3 _offcet;

    private Pool<ParticleSystem> _poolParticle;

    private void Awake() => _poolParticle = new Pool<ParticleSystem>(_particle);

    private void OnEnable() => _gun.Attacked += OnAttacked;
    private void OnDisable() => _gun.Attacked -= OnAttacked;

    private void OnAttacked()
    {
        var particle = _poolParticle.Get(_spawnPoint.position + _offcet, transform.root.parent, quaternion.identity);
        particle.Play();
        StartCoroutine(_poolParticle.WaitAndReturn(particle, 1));
    }
}