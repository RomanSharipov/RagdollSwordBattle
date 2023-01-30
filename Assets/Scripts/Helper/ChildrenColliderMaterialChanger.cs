using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Helper
{
    public class ChildrenColliderMaterialChanger : MonoBehaviour
    {
        [SerializeField] private PhysicMaterial _physicMaterial;
        public List<Collider> Colliders { get; private set; }

        public void FindChildren() => Colliders = GetComponentsInChildren<Collider>().ToList();

        public void Change() => Colliders.ForEach(x => x.material = _physicMaterial);
    }
}