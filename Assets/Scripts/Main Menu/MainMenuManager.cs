using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // only exists in main menu scene
    public static MainMenuManager Instance { get; private set; }
    
    [SerializeField] private int startSceneIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ConfirmCustomization(CharacterInfo characterInfo)
    {
        PartyManagerSingleton.Instance.AddPartyMember(characterInfo);

        StartNewGame();
    }

    private void StartNewGame()
    {
        PlayerDataManagerSingleton.Instance.SavePlayerLastLocation(Vector3.zero, startSceneIndex);

        SceneManager.LoadScene(PlayerDataManagerSingleton.Instance.PlayerLastSceneIndex, LoadSceneMode.Single);
    }
}
