using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollisionHandler : MonoBehaviour,IPlayerDamagable
{
    [SerializeField] private int _damage = 1;
    public int Damage => _damage;
}
