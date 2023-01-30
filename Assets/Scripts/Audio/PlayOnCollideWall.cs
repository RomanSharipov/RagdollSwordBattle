using UnityEngine;

public class PlayOnCollideWall : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _triggerImpulse = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall _) && collision.impulse.magnitude > _triggerImpulse)
        {
            _audio.Play();
        }
    }
}
