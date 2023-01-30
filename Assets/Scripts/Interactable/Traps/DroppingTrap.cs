using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DroppingTrap : Damagable
{
    private DroppingTrapMovement _droppingTrapMovement;
    
    private const float TimeDeltaTime = 0.02f;
    
    private void Awake()
    {
        _droppingTrapMovement = GetComponent<DroppingTrapMovement>();
        _droppingTrapMovement.Init();
    }

    public void StartMovement()
    {
        _droppingTrapMovement.enabled = true;
    }

    public void StopMovement()
    {
        _droppingTrapMovement.enabled = false;
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.TryGetComponent(out Floor floor))
        {
            StopMovement();
            _droppingTrapMovement.SwitchOnGravity();
        }
    }


}