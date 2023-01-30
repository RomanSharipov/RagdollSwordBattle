using IJunior.TypedScenes;
using Prefs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : Singleton<LevelLoad>
{
    [SerializeField] private int _levelsCount = 20;
    [Header("Level pool")]
    [SerializeField] private LevelPool _levelPool;

    private Level _currentLevel;
    private int _isLevelRandom;
    private string _levelDiff;
    private string _levelType;
    private int _levelLoop;

    private void Awake()
    {
        InstantiateLevel();
    }

    public void LoadNewLevel()
    {
        int currentLevelID = CustomPlayerPrefs.GetInt(LevelLoadPrefs.CompletedLevels, 1);
        currentLevelID += 1;
        CustomPlayerPrefs.SetInt(LevelLoadPrefs.CompletedLevels, currentLevelID);

        currentLevelID -= 1;
        IntegrationMetrica.Instance.OnLevelComplete(currentLevelID, _currentLevel.gameObject.name, _levelDiff,
            _levelLoop, _isLevelRandom, _levelType, "win", 100);

        PlayerPrefs.SetInt(LevelLoadPrefs.StartsCount, PlayerPrefs.GetInt(LevelLoadPrefs.StartsCount, 1) + 1);
        TransitionScene.Load();
    }

    public void RestartCurrentLevel()
    {
        int currentLevelID = CustomPlayerPrefs.GetInt(LevelLoadPrefs.CompletedLevels, 1);
        var gameStateHandler = FindObjectOfType<GameStateHandler>();
        var progress = (int)(100 - ((float)gameStateHandler.CurrentEnemyCount / gameStateHandler.AllEnemyCount) * 100);

        IntegrationMetrica.Instance.OnLevelComplete(currentLevelID, _currentLevel.gameObject.name, _levelDiff,
            _levelLoop, _isLevelRandom, _levelType, "lose", progress);

        PlayerPrefs.SetInt(LevelLoadPrefs.StartsCount, PlayerPrefs.GetInt(LevelLoadPrefs.StartsCount, 1) + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void InstantiateLevel()
    {
        int currentLevelID = CustomPlayerPrefs.GetInt(LevelLoadPrefs.CompletedLevels, 1);
        int level = currentLevelID >= _levelsCount
            ? Random.Range(0, _levelPool.Levels.Count)
            : GetCurrentLevel(currentLevelID);

        _currentLevel = Instantiate(_levelPool.Levels[level]);
        SentLevelStartEvent(currentLevelID);
    }

    private void SentLevelStartEvent(int currentLevelID)
    {
        _levelDiff = _currentLevel.Data.GetLevelDifficulty();
        _levelType = _currentLevel.Data.GetLevelType();
        _levelLoop = currentLevelID / (_levelsCount + 1);
        _isLevelRandom = _levelLoop >= 1 ? 1 : 0;

        IntegrationMetrica.Instance.OnLevelStart(currentLevelID, _currentLevel.gameObject.name, _levelDiff,
            _levelLoop,
            _isLevelRandom, _levelType);
    }

    private int GetCurrentLevel(int currentLevelPref)
    {
        var level = (int)((float)currentLevelPref % _levelPool.Levels.Count);
        return level == 0 ? _levelPool.Levels.Count - 1 : level - 1;
    }
}