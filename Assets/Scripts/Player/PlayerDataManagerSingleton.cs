using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManagerSingleton : MonoBehaviour
{
    public static PlayerDataManagerSingleton Instance { get; private set; }
    
    public int PlayerLastSceneIndex { get; private set; } = 1;
    public Vector3 PlayerLastLocation { get; private set; } = Vector3.zero;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SavePlayerLastLocation(Vector3 playerLocation, int sceneIndex)
    {
        PlayerLastLocation = playerLocation;
        PlayerLastSceneIndex = sceneIndex;
    }
}
