using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIControls _uiControls;

    [SerializeField] private GameObject characterInfoUICanvas;
    [SerializeField] private GameObject pauseUICanvas;

    private CurrentFieldUI _currentFieldUI = CurrentFieldUI.None;
    
    void Awake()
    {
        _uiControls = new UIControls();
    }
    
    private void OnEnable()
    {
        _uiControls.Enable();
    }

    private void OnDisable()
    {
        _uiControls.Disable();
    }

    private void Start()
    {
        characterInfoUICanvas.SetActive(false);
        
        pauseUICanvas.SetActive(false);
        PauseUI pauseUI = pauseUICanvas.GetComponent<PauseUI>();
        pauseUI.OnResumeButtonClicked += CloseUIScreen;
    }

    void Update()
    {
        if (_uiControls.UI.CharacterUI.WasPressedThisFrame())
        {
            OpenUIScreen(CurrentFieldUI.CharacterInfo);
        }

        if (_uiControls.UI.Cancel.WasPressedThisFrame())
        {
            if (_currentFieldUI != CurrentFieldUI.None)
            {
                CloseUIScreen();
            }
            else
            {
                OpenUIScreen(CurrentFieldUI.Pause);
            }
        }
    }

    private void OpenUIScreen(CurrentFieldUI currentFieldUI)
    {
        if (_currentFieldUI != CurrentFieldUI.None)
        {
            return;
        }
        
        switch (currentFieldUI)
        {
            case CurrentFieldUI.CharacterInfo:
                characterInfoUICanvas.SetActive(true);
                break;
            case CurrentFieldUI.Pause:
                pauseUICanvas.SetActive(true);
                break;
        }

        _currentFieldUI = currentFieldUI;
        Time.timeScale = 0f;
    }

    private void CloseUIScreen()
    {
        switch (_currentFieldUI)
        {
            case CurrentFieldUI.CharacterInfo:
                characterInfoUICanvas.SetActive(false);
                break;
            case CurrentFieldUI.Pause:
                pauseUICanvas.SetActive(false);
                break;
        }

        _currentFieldUI = CurrentFieldUI.None;
        Time.timeScale = 1f;
        FieldManager.Instance.ResetFieldCharacterSprite();
    }
}

public enum CurrentFieldUI
{
    None,
    CharacterInfo,
    Inventory,
    BattleMember,
    Pause,
}