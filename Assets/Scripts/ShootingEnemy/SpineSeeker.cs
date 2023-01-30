using UnityEngine;

public class SpineSeeker : MonoBehaviour
{
    [SerializeField] private Transform _spine;
    [SerializeField] private PlayerSeeker _playerSeeker;

    private Vector3 _currentRotate;

    private void LateUpdate()
    {
        if (_currentRotate == Vector3.zero)
            return;

        transform.LookAt(_currentRotate);
    }

    private void OnEnable() => _playerSeeker.PlayerDetected += Rotate;

    private void OnDisable() => _playerSeeker.PlayerDetected -= Rotate;

    private void Rotate(Vector3 targetPosition)
    {
        ;
        _spine.LookAt(targetPosition);
        _currentRotate = targetPosition;
    }
}