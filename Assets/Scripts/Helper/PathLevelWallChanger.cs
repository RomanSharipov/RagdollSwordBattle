using System;
using UnityEngine;

public class PathLevelWallChanger : MonoBehaviour
{
    [NonSerialized] public Material NewWallMaterial;
    [NonSerialized] public Material NewWallInsideMaterial;
    private Wall[] _walls;

    [SerializeField] private bool _isWallInside = false;

    private void FindAllWall() => _walls = GetComponentsInChildren<Wall>();

    public void ChangeMaterial()
    {
        FindAllWall();
        foreach (var wall in _walls)
        {
            var meshFilter = wall.GetComponent<MeshRenderer>();
            if (_isWallInside == false)
                meshFilter.material = NewWallMaterial;
            else
                meshFilter.material = NewWallInsideMaterial;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(wall.gameObject);
#endif
        }
    }

    public void SetWallLayer()
    {
        FindAllWall();
        foreach (var wall in _walls)
        {
            wall.gameObject.layer = LayerMask.NameToLayer("Wall");
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(wall.gameObject);
#endif
        }
    }
}