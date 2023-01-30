using System;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Rope : MonoBehaviour
{
    [SerializeField] private Joint _trapJoint;
    
    private HingeJoint _joint;

    public event Action<Rope> RopeDestroyed; 

    private void Awake()
    {
        _joint = GetComponent<HingeJoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Weapon sword))
        {
            RopeDestroyed?.Invoke(this);
            
            Destroy(_trapJoint);
            Destroy(_joint.gameObject);
        }
    }
}
