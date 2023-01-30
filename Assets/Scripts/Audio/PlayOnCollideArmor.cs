using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayOnCollideArmor : MonoBehaviour
{
    private AudioSource _audio;

    private void Start() 
    {
        _audio = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BouncingObject bouncingObject))
        {
            _audio.Play();
        }
    }
}
