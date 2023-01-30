using System;
using UnityEngine;

public class Noise : MonoBehaviour
{
    [SerializeField] private CameraHitMovement _cameraHitMovement;
    [SerializeField] private float _triggerImpulse = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.TryGetComponent(out Wall _) ||
             collision.gameObject.TryGetComponent(out BossHealth _)) &&
             collision.impulse.magnitude > _triggerImpulse)
        {
            _cameraHitMovement.SwitchToHitCamera();
        }
    }
}