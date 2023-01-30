using System.Collections;
using Cinemachine;
using UnityEngine;

public class PriorityInstaller : MonoBehaviour
{
     [SerializeField] private CinemachineVirtualCamera _virtualCamera;
     [SerializeField,Min(0)] private int _nextPriority;

     private IEnumerator Start()
     {
         yield return null;
         _virtualCamera.Priority = _nextPriority;
     }
}
