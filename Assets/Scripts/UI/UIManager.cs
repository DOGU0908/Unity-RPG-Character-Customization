using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIControls _uiControls;

    [SerializeField] private GameObject characterInfoUICanvas;

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
    }

    void Update()
    {
        if (_uiControls.UI.CharacterUI.WasPressedThisFrame())
        {
            OpenUIScreen(CurrentFieldUI.CharacterInfo);
        }

        if (_uiControls.UI.Cancel.WasPressedThisFrame() && _currentFieldUI != CurrentFieldUI.None)
        {
            CloseUIScreen();
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
}