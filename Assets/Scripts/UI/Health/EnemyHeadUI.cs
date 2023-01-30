using UnityEngine;
using UnityEngine.UI;

public class EnemyHeadUI : MonoBehaviour
{
    [SerializeField] private Image _croosImage;

    public void CrossOutEnemyHead()
    {
        _croosImage.enabled = true;
    }
}
