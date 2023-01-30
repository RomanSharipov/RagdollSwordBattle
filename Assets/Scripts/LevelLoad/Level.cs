using UnityEngine;

public class Level : MonoBehaviour 
{
    [SerializeField] private LevelAnalyticsData _data;

    public LevelAnalyticsData Data => _data;
}
