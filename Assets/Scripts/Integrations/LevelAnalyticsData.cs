using System;
using UnityEngine;

[Serializable]
public class LevelAnalyticsData
{
    [SerializeField] private LevelDifficulty _levelDifficulty;
    [SerializeField] private LevelType _levelType;

    public string GetLevelDifficulty()
    {
        return _levelDifficulty switch
        {
            LevelDifficulty.Easy => "easy",
            LevelDifficulty.Normal => "normal",
            LevelDifficulty.Hard => "hard",
        };
    }

    public string GetLevelType()
    {
        return _levelType switch
        {
            LevelType.KillAll => "kill_all",
            LevelType.Hostage => "hostage",
            LevelType.Boss => "boss",
        };
    }
}

public enum LevelDifficulty
{
    Easy,
    Normal,
    Hard
}

public enum LevelType
{
    KillAll,
    Hostage,
    Boss
}
