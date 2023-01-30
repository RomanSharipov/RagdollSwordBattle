using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraHitMovement : MonoBehaviour
{
   [SerializeField] private CinemachineVirtualCamera _mainCamera;
   [SerializeField, Min(0f)] private float _hitCameraTime = 0.1f;

   private CinemachineBasicMultiChannelPerlin _noiseComponent;

   private void Awake()
   {
      _noiseComponent = _mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
   }

   public void SwitchToHitCamera()
   {
      StartCoroutine(SwitchCameraCoroutine());
   }

   private IEnumerator SwitchCameraCoroutine()
   {
      _noiseComponent.m_AmplitudeGain = 1;

      yield return new WaitForSecondsRealtime(_hitCameraTime);
      
      _noiseComponent.m_AmplitudeGain = 0;
   }
}
