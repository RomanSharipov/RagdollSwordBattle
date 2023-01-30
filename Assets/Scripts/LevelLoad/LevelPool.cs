using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelPool", menuName = "Levels/LevelPool", order = 54)]
public class LevelPool : ScriptableObject
{
    [SerializeField] private List<Level> _levels;

    public List<Level> Levels => _levels;
}
