using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    public delegate void OnResumeButtonClickedDelegate();
    public OnResumeButtonClickedDelegate OnResumeButtonClicked;
    
    private const int MainMenuIndex = 0;
    
    private void Start()
    {
        resumeButton.onClick.AddListener(() =>
        {
            OnResumeButtonClicked.Invoke();
        });
        
        mainMenuButton.onClick.AddListener(() =>
        {
            // save game
            
            Time.timeScale = 1f;
            
            SceneManager.LoadScene(MainMenuIndex, LoadSceneMode.Single);
        });
    }
}
