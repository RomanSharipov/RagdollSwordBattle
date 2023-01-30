using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FullLevelMaterialChanger))]
public class FullLevelMaterialChangerEditor : Editor
{
    private FullLevelMaterialChanger _changer;

    private void OnEnable()
    {
        _changer = (FullLevelMaterialChanger) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("ChangeMaterial"))
        {
            _changer.ChangeMaterial();
        }

        if (GUILayout.Button("Set WallLayerInWall"))
        {
            _changer.SetWallsLayer();
        }
    }
}