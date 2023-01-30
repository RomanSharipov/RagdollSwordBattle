using System;
using ch.sycoforge.Decal;
using UnityEngine;

[Serializable]
public class SpawnerCrack 
{
    [Tooltip("Crack settings")]
    [SerializeField] private Crack _crack;
    [SerializeField] private float _triggerImpulse;
    [Tooltip("Scale settings")]
    [SerializeField] private float _maxScale;
    [SerializeField] private float _minScale;
    [SerializeField] private float _offsetScale;
    [Tooltip("Z position offset")]
    [SerializeField] private float _minOffsetZ;
    [SerializeField] private float _maxOffsetZ;
    [Tooltip("Look at camera settings")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _angleBetweenCameraAndNormalPlane;

    public float TriggerImpulse =>_triggerImpulse;

    public void CreateCrack(Collision collision)
    {
        float targetScale = Mathf.InverseLerp(_minScale, _maxScale, collision.impulse.magnitude) + _offsetScale;
        float targetOffsetZposition = Mathf.InverseLerp(_minOffsetZ, _maxOffsetZ, collision.impulse.magnitude);
        
        Vector3 collisionPoint = collision.contacts[0].point;
        EasyDecal crack = EasyDecal.Project(_crack.gameObject, collisionPoint, collision.contacts[0].normal);
        
        crack.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        Quaternion lookAtCameraRotation = Quaternion.LookRotation(_mainCamera.transform.position - crack.transform.position);
        crack.transform.rotation = lookAtCameraRotation;
        crack.transform.rotation = Quaternion.Lerp(_crack.transform.rotation, lookAtCameraRotation, _angleBetweenCameraAndNormalPlane);
        crack.transform.Rotate(new Vector3(90,0, 0), Space.Self);
        crack.transform.Rotate(new Vector3(0,UnityEngine.Random.Range(0, 360), 0), Space.Self);
        crack.transform.position = new Vector3(crack.transform.position.x,crack.transform.position.y,crack.transform.position.z - targetOffsetZposition);
    }
}
