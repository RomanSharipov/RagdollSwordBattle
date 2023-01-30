using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private BouncingObject _bouncingObject;
    [SerializeField] private SeizableObject _seizableObject;
    [Header("Slow-mo")]
    [SerializeField, Min(0)] private float _slowTimeScale = 0.3f;
    [Header("Push settings")]
    [SerializeField] private float _maxForce;
    [Header("Arrow View")]
    [SerializeField] private ArrowView _arrowView;

    public void SeizeObject()
    {
        _seizableObject.Seize();
        TimeScaleHandler.Instance.SetSlowedTimeScale(_slowTimeScale);
    } 
    
    public void StartObjectPush()
    {
        _arrowView.Enable();
        SeizeObject();
    }

    public void StopMove() => _seizableObject.Release();

    public void BouncingObjectLookAt(Vector3 direction)
    {
        var lookDirection = new Vector3(direction.x, direction.y, transform.position.z);

        _bouncingObject.LookAtDirection(lookDirection);
        _arrowView.PointAt(direction);
    }

    public void PushBouncingObject(Vector3 forceDirection)
    {
        DisableArrowView();
        _seizableObject.Release();

        TimeScaleHandler.Instance.ResetTimeScale(_slowTimeScale);

        forceDirection = Vector3.ClampMagnitude(forceDirection, _maxForce);
        _bouncingObject.Push(forceDirection);
    }

    private void DisableArrowView() => _arrowView.Disable();
}