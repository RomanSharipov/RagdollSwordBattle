using Unity.Mathematics;
using UnityEngine;

public class ArmorParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BouncingObject _) && collision.contacts.Length != 0)
        {
            var particle = Instantiate(_particle, collision.contacts[0].point, quaternion.identity, transform);
            var particleTransform = particle.transform;
            particleTransform.rotation =
                Quaternion.FromToRotation(particleTransform.forward, collision.contacts[0].normal) *
                particleTransform.rotation;
        }
    }
}