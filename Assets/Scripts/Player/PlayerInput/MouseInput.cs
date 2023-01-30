using System.Collections;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Coroutine _pushCoroutine;

    private void Start()
    {
        EnableInput();
    }

    public void EnableInput()
    {
        ResetInput();
    }

    public void DisableInput()
    {
        if (_pushCoroutine != null)
            StopCoroutine(_pushCoroutine);

        enabled = false;
        _player.StopMove();

        TimeScaleHandler.Instance.SetSlowedTimeScale(0.1f);
    }

    private void ResetInput()
    {
        if (_pushCoroutine != null)
            StopCoroutine(_pushCoroutine);
        
        _pushCoroutine = StartCoroutine(GetForceDirectionAndPush());
    }

    private IEnumerator GetForceDirectionAndPush()
    {
        while (Input.GetMouseButtonDown(0) == false)
        {
            yield return null;
        }

        var initialPoint = Input.mousePosition;
        var forceDirection = Vector3.zero;

        _player.StartObjectPush();

        while (Input.GetMouseButton(0))
        {
            forceDirection = Input.mousePosition - initialPoint;
            _player.BouncingObjectLookAt(forceDirection);
            yield return null;
        }

        _player.PushBouncingObject(forceDirection);

        ResetInput();
    }
}