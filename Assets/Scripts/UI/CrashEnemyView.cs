using UnityEngine;

public class CrashEnemyView : MonoBehaviour
{
    [SerializeField] private BotHealth _enemyToCrash;

    private void OnEnable()
    {
        _enemyToCrash.BotDied += OnEnemyDied;
    }

    private void OnEnemyDied(BotHealth enemy)
    {
        enemy.BotDied -= OnEnemyDied;
        Destroy(gameObject);
    }
}
