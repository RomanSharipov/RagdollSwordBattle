using System;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}