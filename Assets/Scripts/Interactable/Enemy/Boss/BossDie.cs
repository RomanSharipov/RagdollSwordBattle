using UnityEngine;

public class BossDie : MonoBehaviour
{
     [SerializeField] private Gun _gun;
     [SerializeField] private BossAttack _bossAttack;
     [SerializeField] private Health _health;
     [SerializeField] private EnemyMovement _enemyMovement;
     [SerializeField] private Rigidbody _rigidbody;

     private void OnEnable() => _health.Died += Die;

     private void OnDisable() => _health.Died -= Die;

     private void Die()
     {
          _bossAttack.DisableGun();
          _enemyMovement.enabled = false;
          _gun.DisableGun();
          _rigidbody.isKinematic = false;
     }
}
