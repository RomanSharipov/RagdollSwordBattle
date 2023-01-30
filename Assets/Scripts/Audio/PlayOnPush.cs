using UnityEngine;

public class PlayOnPush : MonoBehaviour
{
    [SerializeField] private BouncingObject _bouncingObject;
    [SerializeField] private AudioSource _audio;

    private void OnEnable() => _bouncingObject.ObjectPushed += OnPush;

    private void OnDisable() => _bouncingObject.ObjectPushed -= OnPush;

    private void OnPush() => _audio.Play();
}
