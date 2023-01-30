using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private DroppingTrap _droppingTrap;
    [SerializeField] private DroppingTrapMovement _roppingTrapMovement;
    [SerializeField] private ParticleSystem _explosionTemplate;
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out BouncingObject bouncingObject))
        {
            _roppingTrapMovement.SwitchOffIsKinematic();
            _droppingTrap.StartMovement();
            Instantiate(_explosionTemplate,transform.position,transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
