using System;
using UnityEngine;

[Serializable]
public class DeathMaterial
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _dieMaterial;

    public bool IsMaterialChanged => _dieMaterial != null && _skinnedMeshRenderer != null;

    public void SetDieMaterial()
    {
        Material[] newMaterials = new Material[_skinnedMeshRenderer.materials.Length];

        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = _dieMaterial;
        }

        _skinnedMeshRenderer.materials = newMaterials;
    }
}