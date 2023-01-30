using System;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [NonSerialized]
    public bool isIgnoreCollision = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (isIgnoreCollision)
            return;
        Stop();
    }

    public void Play()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();
    }

    private void Stop()
    {
        _particleSystem.gameObject.SetActive(false);
        _particleSystem.Stop();
    }
}