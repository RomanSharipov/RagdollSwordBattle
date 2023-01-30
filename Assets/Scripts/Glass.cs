using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    private const float MinPushForce = 0.1f;
    private const float MaxPushForce = 10f;

    [SerializeField] private BotHealth[] _enemiesOnGlass;

    private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

    private void Awake()
    {
        Transform[] childrens = GetComponentsInChildren<Transform>();

        foreach (var children in childrens)
        {
            Rigidbody chunkGlassRigidbody = children.gameObject.AddComponent<Rigidbody>();
            chunkGlassRigidbody.isKinematic = true;
            chunkGlassRigidbody.useGravity = false;

            _rigidbodies.Add(chunkGlassRigidbody);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BouncingObject bouncingObject))
        {
            BreakGlass();

            foreach (BotHealth enemyOnGlass in _enemiesOnGlass)
            {
                enemyOnGlass.FallDown();
            }
        }
    }

    private void BreakGlass()
    {
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;

            var modifier = Random.Range(MinPushForce, MaxPushForce);
            rigidbody.AddForce(Vector3.down * modifier, ForceMode.Impulse);
        }
    }
}
