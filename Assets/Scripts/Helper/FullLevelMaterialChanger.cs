using UnityEngine;


    public class FullLevelMaterialChanger : MonoBehaviour
    {
        [SerializeField] private Material _newWallMaterial;
        [SerializeField] private Material _newWallInsideMAterial;
        private PathLevelWallChanger[] _changers;
        
        private void FindAllPath() => _changers = GetComponentsInChildren<PathLevelWallChanger>();

        public void ChangeMaterial()
        {
            FindAllPath();
            foreach (var pathLevelWallChanger in _changers)
            {
                pathLevelWallChanger.NewWallMaterial = _newWallMaterial;
                pathLevelWallChanger.NewWallInsideMaterial = _newWallInsideMAterial;
                pathLevelWallChanger.ChangeMaterial();
            }
        }

        public void SetWallsLayer()
        {
            FindAllPath();
            foreach (var pathLevelWallChanger in _changers)
            {
              pathLevelWallChanger.SetWallLayer();   
            }
        }
            
    }