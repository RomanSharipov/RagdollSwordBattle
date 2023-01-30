using UnityEngine;
using UnityEngine.UI;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private Image _arrowImage;
    [Header("Heught and distance")]
    [SerializeField, Min(0)] private float _minHeight = 1f;
    [SerializeField, Min(0)] private float _maxHeight = 3f;
    [SerializeField, Min(0f)] private float _maxDistance = 500f;
    [SerializeField] private BouncingObject _playerBouncingObject;

    private float _defaultHeight;
    private RectTransform _imageRectTransform;
    private Coroutine _lookAtCoroutine;

    private void Awake()
    {
        _arrowImage.gameObject.SetActive(false);

        _imageRectTransform = _arrowImage.rectTransform;
        _defaultHeight = _imageRectTransform.sizeDelta.y;
    }

    public void Enable()
    {
        _arrowImage.gameObject.SetActive(true);

    }

    public void Disable()
    {
        _arrowImage.gameObject.SetActive(false);
    }

    public void ChangeArrowScaleByDirection(Vector3 direction)
    {
        float magnitudePercent = direction.magnitude / _maxDistance;
        float newHeight = Mathf.Lerp(_minHeight, _maxHeight, magnitudePercent);

        _imageRectTransform.sizeDelta = new Vector2(_imageRectTransform.sizeDelta.x, newHeight);
        _imageRectTransform.transform.localPosition = new Vector3(0, 0, newHeight / 2f + _defaultHeight);
    }

    public void PointAt(Vector3 direction)
    {
        SetPositionArrowImage();
        ChangeArrowScaleByDirection(direction);
        LookAt(direction);
    }

    private void SetPositionArrowImage()
    {
        transform.position = Camera.main.WorldToScreenPoint(_playerBouncingObject.transform.position);
    }

    public void LookAt(Vector3 direction)
    {
        Vector3 targetDirection = direction.normalized;
        targetDirection.z = 0f;
        transform.LookAt(targetDirection + transform.position);
    }
}