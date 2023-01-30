using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameStateHandler))]
public class GameStateHandlerEditor : Editor
{
    private GameStateHandler _changer;

    private void OnEnable()
    {
        _changer = (GameStateHandler) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Find&Add Enemy and Hostage"))
            _changer.FindAll();
    }
}