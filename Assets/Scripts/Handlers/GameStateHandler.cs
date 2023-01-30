using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GameStateHandler : MonoBehaviour
{
    [FormerlySerializedAs("_winFailPanel")]
    [Header("Win/Fail UI")]
    [SerializeField] private WinLouseEffect _winLouseEffect;

    [SerializeField] private EnemyHeadsBar _enemyHeadsBar;
    [SerializeField] private HealthDisplay _healthDisplayTemplate;
    [SerializeField] private Transform _healthDisplaysContainer;

    [Header("Enemies and player")]
    [SerializeField] private List<BotHealth> _enemies;
    [SerializeField] private PlayerHealth _player;
    [SerializeField] private MouseInput _input;
    [SerializeField] private CameraHitMovement _cameraHitMovement;

    [Header("Hostage")]
    [Tooltip("Hostage health might be null, if there is no hostage")]
    [SerializeField] private HostageHealth _hostageHealth;

    private bool _isOneEndScreenActive = false;
    public int AllEnemyCount { get; private set; }
    public int CurrentEnemyCount { get; private set; }

    public event Action WinUIEnabled;

    private void Start()
    {
        AllEnemyCount = _enemies.Count;
        CurrentEnemyCount = AllEnemyCount;

        foreach (var enemy in _enemies)
        {
            enemy.BotDied += OnEnemyDied;
            enemy.TryCreateHealthDisplay(_healthDisplayTemplate, _healthDisplaysContainer);
        }

        _player.TryCreateHealthDisplay(_healthDisplayTemplate, _healthDisplaysContainer);
        _player.Died += OnPlayerDied;

        if (_hostageHealth != null)
        {
            _hostageHealth.CreateQuotation(_healthDisplaysContainer);
            _hostageHealth.Died += OnPlayerDied;
        }

        _enemyHeadsBar.Init(_enemies);
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.BotDied -= OnEnemyDied;
        }

        if (_hostageHealth != null)
            _hostageHealth.Died -= OnPlayerDied;

        _player.TryRemoveHealthDisplay();
        _player.Died -= OnPlayerDied;
    }

    private void OnEnemyDied(BotHealth enemy)
    {
        CurrentEnemyCount--;
        enemy.TryRemoveHealthDisplay();
        enemy.BotDied -= OnEnemyDied;

        _enemies.Remove(enemy);
        _enemyHeadsBar.TryCrossOutEnemy();
        _cameraHitMovement.SwitchToHitCamera();

        if (_enemies.Count <= 0 && _isOneEndScreenActive == false)
        {
            WinUIEnabled?.Invoke();

            _isOneEndScreenActive = true;

            _input.DisableInput();
            _enemyHeadsBar.DisableHealthBar();

            _winLouseEffect.ShowWinUI();
        }
    }

    private void OnPlayerDied()
    {
        _isOneEndScreenActive = true;

        _input.DisableInput();
        _enemyHeadsBar.DisableHealthBar();

        _winLouseEffect.ShowFailUI();
    }

#if UNITY_EDITOR
    public void FindAll()
    {
        _enemies = FindObjectsOfType<BotHealth>().ToList();
        _hostageHealth = FindObjectOfType<HostageHealth>();
        Save();
    }

    private void Save() => UnityEditor.EditorUtility.SetDirty(this);
#endif
}