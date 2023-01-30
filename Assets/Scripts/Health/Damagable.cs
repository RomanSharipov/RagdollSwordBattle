using UnityEngine;

public class Damagable : MonoBehaviour, IPlayerDamagable
{
    [Header("Damagaeble")]
    [SerializeField] private int _damage;

    public int Damage => _damage;
}
