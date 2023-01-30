using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BouncingObject : MonoBehaviour
{
    [SerializeField, Min(0)] private float _maxSpeed = 1f;
    [SerializeField] SpawnerCrack _spawnerCrack = new SpawnerCrack();
    
    private Rigidbody _rigidbody;

    public event Action ObjectPushed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Push(Vector3 forceDirection)
    {
        _rigidbody.AddForce(forceDirection, ForceMode.VelocityChange);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
        
        ObjectPushed?.Invoke();
    }

    public void LookAtDirection(Vector3 direction)
    {
        transform.LookAt(direction, transform.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > _spawnerCrack.TriggerImpulse)
        {
            _spawnerCrack.CreateCrack(collision);
        }
    }
    
    
}