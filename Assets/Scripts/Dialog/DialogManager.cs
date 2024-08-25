using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    [SerializeField] private GameObject dialogCanavs;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Transform choiceContainerObject;

    [SerializeField] private GameObject choiceButtonPrefab;

    private const string BaseChoice = "continue";
    
    public bool IsOpened { get; private set; }

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
        
        dialogCanavs.SetActive(false);
        IsOpened = false;
    }

    public void DisplayDialogText(string text)
    {
        dialogCanavs.SetActive(true);

        dialogText.text = text;

        ClearChoice();
        GenerateButton(BaseChoice, CloseDialog);
        
        IsOpened = true;
    }

    private void ClearChoice()
    {
        foreach (Transform child in choiceContainerObject)
        {
            Destroy(child.gameObject);
        }
    }

    private void GenerateButton(string text, Action action)
    {
        GameObject continueButtonObject = Instantiate(choiceButtonPrefab, choiceContainerObject);
        Button continueButton = continueButtonObject.GetComponent<Button>();
        continueButton.onClick.AddListener(() =>
        {
            action();
        });
        TextMeshProUGUI continueButtonText = continueButtonObject.GetComponentInChildren<TextMeshProUGUI>();
        continueButtonText.text = text;
    }

    private void CloseDialog()
    {
        dialogCanavs.SetActive(false);

        IsOpened = false;
    }
}
