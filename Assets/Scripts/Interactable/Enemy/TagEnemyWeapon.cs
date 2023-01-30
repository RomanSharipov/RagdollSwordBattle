using UnityEngine;

public class TagEnemyWeapon : MonoBehaviour
{
    public void Hide() => gameObject.SetActive(false);
    
    public void Show() => gameObject.SetActive(true);
}