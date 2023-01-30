#if UNITY_EDITOR
using UnityEngine;

public class CubeDrower : MonoBehaviour
{
    [SerializeField] private Transform _cube;

    private void OnDrawGizmos()
    {
        if (_cube == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _cube.localScale);
    }
}
#endif
