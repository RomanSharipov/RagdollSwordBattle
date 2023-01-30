using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowHandler<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Image _arrowPrefab;

    private Dictionary<T, Image> _arrows = new Dictionary<T, Image>();

    protected void Init(T[] objectsArray)
    {
        foreach (T obj in objectsArray)
        {
            if (obj.gameObject.activeSelf)
            {
                var arrowPosition = Camera.main.WorldToScreenPoint(obj.transform.position);
                var arrowImage = Instantiate(_arrowPrefab, arrowPosition, Quaternion.identity, transform);

                _arrows.Add(obj, arrowImage);
                arrowImage.gameObject.SetActive(false);
            }
        }
    }

    protected void UpdateArrowsPosition()
    {
        foreach (var arrow in _arrows)
        {
            if (arrow.Key == null)
            {
                arrow.Value.gameObject.SetActive(false);
                continue;
            }

            var arrowPosition = Camera.main.WorldToScreenPoint(arrow.Key.transform.position);
            var health = arrow.Key.GetComponent<Health>();

            if (arrowPosition.x > 0f && arrowPosition.x < Screen.width && arrowPosition.y > 0f && arrowPosition.y < Screen.height)
            {
                arrow.Value.gameObject.SetActive(false);
                continue;
            }

            if (health != null && health.IsAlive == false)
            {
                arrow.Value.gameObject.SetActive(false);
                continue;
            }

            arrow.Value.gameObject.SetActive(true);

            var rectTransform = arrow.Value.GetComponent<RectTransform>();
            Vector3 clampedPosition = Vector3.zero;
            clampedPosition.x = Mathf.Clamp(arrowPosition.x, 0f + rectTransform.rect.width / 2, Screen.width - rectTransform.rect.width / 2);
            clampedPosition.y = Mathf.Clamp(arrowPosition.y, 0f + rectTransform.rect.height / 2, Screen.height - rectTransform.rect.height / 2);
            arrow.Value.transform.position = clampedPosition;

            var direction = arrowPosition - clampedPosition;
            RotateImageTowardsTarget(direction, arrow.Value);
        }
    }

    private void RotateImageTowardsTarget(Vector3 direction, Image rotationImage)
    {
        var rotation = Quaternion.LookRotation(direction, Vector3.back);
        rotation.x = 0f;
        rotation.y = 0f;
        rotationImage.transform.rotation = rotation;

        var scale = rotationImage.transform.localScale;
        scale.x = rotationImage.rectTransform.rotation.z > 0f ? -1 : 1;
        rotationImage.rectTransform.localScale = scale;
    }
}
