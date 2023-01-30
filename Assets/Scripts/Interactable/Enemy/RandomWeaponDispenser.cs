using System.Collections.Generic;
using UnityEngine;

public class RandomWeaponDispenser : MonoBehaviour
{
    [SerializeField] private List<TagEnemyWeapon> _enemyWeapon;

    private void Start()
    {
        _enemyWeapon.ForEach(x => x.Hide());
        _enemyWeapon[Random.Range(0, _enemyWeapon.Count)].Show();
    }
}