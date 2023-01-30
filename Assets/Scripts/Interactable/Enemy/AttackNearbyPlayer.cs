using System.Collections;
using UnityEngine;

public class AttackNearbyPlayer : MonoBehaviour
{
    private const string AttackParameter = "Attack";
    
    [SerializeField] private Animator _animator;
    [SerializeField] private Damagable _weapon;
    [SerializeField] private float _forceHit;
    [SerializeField] private EnemyMovement _enemyMovement;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            if (Time.timeScale < 1)
                return;

            Attack(playerHealth);
            
        }
        
        if (other.TryGetComponent(out HostageHealth hostageHealth))
        {
            Attack(hostageHealth);
        }
    }
    
    private void Attack(PlayerHealth playerHealth)
    {
        _animator.SetBool(AttackParameter,true);
        _enemyMovement.StopPatrol();
    }

    private void Attack(HostageHealth hostageHealth)
    {
        _animator.SetBool(AttackParameter, true);
        hostageHealth.TakeDamage(_weapon.Damage);
        
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            _animator.SetBool(AttackParameter, false);
            _enemyMovement.StartPatrol();
        }
    }
}
