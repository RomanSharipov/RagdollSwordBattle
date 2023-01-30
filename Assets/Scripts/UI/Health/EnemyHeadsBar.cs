using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadsBar : MonoBehaviour
{
    [SerializeField] private EnemyHeadUI _enemyHeadTemplate;

    private int _counterEnemyHeads = 0;
    private List<EnemyHeadUI> _enemyHeads = new List<EnemyHeadUI>();

    public void Init(IReadOnlyCollection<BotHealth> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            EnemyHeadUI newEnemyHead = Instantiate(_enemyHeadTemplate, transform);
            _enemyHeads.Add(newEnemyHead);
        }
    }

    public void TryCrossOutEnemy()
    {
        if (_counterEnemyHeads < _enemyHeads.Count)
        {
            _enemyHeads[_counterEnemyHeads].CrossOutEnemyHead();
            _counterEnemyHeads += 1;
        }
    }

    public void DisableHealthBar()
    {
        gameObject.SetActive(false);
    }
}
