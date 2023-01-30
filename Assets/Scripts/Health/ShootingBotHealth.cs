using UnityEngine;

public class ShootingBotHealth : BotHealth
{
    [SerializeField] private GameObject _ragdollMesh;
    
    public override void Die()
    {
        base.Die();
        
        Instantiate(_ragdollMesh, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
