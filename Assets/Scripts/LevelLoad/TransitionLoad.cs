using IJunior.TypedScenes;
using Prefs;
using UnityEngine;

public class TransitionLoad : MonoBehaviour
{
    [SerializeField] private int _levelLoopStartLevel = 21;
    
    private void Awake()
    {
        DefineAndLoadLevel();
    }
    
    private void DefineAndLoadLevel()
    {
        int currentLevel = CustomPlayerPrefs.GetInt(LevelLoadPrefs.CompletedLevels, 1);

        if (currentLevel >= _levelLoopStartLevel)
        {
            currentLevel = Random.Range(4, _levelLoopStartLevel);
        }

        if (currentLevel < 6)
        {
            CloudsLevel.Load();
            return;
        }

        if (currentLevel < 11)
        {
            MountainLevel.Load();
            
            return;
        }

        if (currentLevel < 16)
        {
            CityLevel.Load();
            return;
        }
        
        MoonLevel.Load();
    }
}
