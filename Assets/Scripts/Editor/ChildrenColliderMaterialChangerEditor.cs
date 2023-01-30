using Helper;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChildrenColliderMaterialChanger))]
    public class ChildrenColliderMaterialChangerEditor : Editor
    {
        private ChildrenColliderMaterialChanger _changer;

        private void OnEnable()
        {
            _changer = (ChildrenColliderMaterialChanger) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Set Children Material"))
            {
                _changer.FindChildren();
                _changer.Change();
                _changer.Colliders.ForEach(x => UnityEditor.EditorUtility.SetDirty(x));
            }
        }
    }
