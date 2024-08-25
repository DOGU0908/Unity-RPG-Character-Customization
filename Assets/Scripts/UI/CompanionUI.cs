using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompanionUI : MonoBehaviour
{
    [SerializeField] private GameObject companionButtonPrefab;

    [SerializeField] private Transform contentObject;

    public void SetupCompanionInterface(Action<int> onButtonClicked)
    {
        ClearExistingButtons();

        foreach (CharacterInfo characterInfo in PartyManagerSingleton.Instance.CompanionList)
        {
            GameObject companionButtonInstance = Instantiate(companionButtonPrefab, contentObject);

            Button button = companionButtonInstance.GetComponent<Button>();
            TextMeshProUGUI textMeshProUGUI = companionButtonInstance.GetComponentInChildren<TextMeshProUGUI>();
            
            textMeshProUGUI.text = characterInfo.Name;
            
            button.onClick.AddListener(() =>
            {
                onButtonClicked(characterInfo.Id);
            });
        }
    }

    private void ClearExistingButtons()
    {
        foreach (Transform child in contentObject)
        {
            Destroy(child.gameObject);
        }
    }
}
