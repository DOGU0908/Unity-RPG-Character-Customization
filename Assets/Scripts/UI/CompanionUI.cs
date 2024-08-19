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

        for (int i = 0; i < PartyManagerSingleton.Instance.GetCompanionLength(); ++i)
        {
            GameObject companionButtonInstance = Instantiate(companionButtonPrefab, contentObject);

            Button button = companionButtonInstance.GetComponent<Button>();
            TextMeshProUGUI textMeshProUGUI = companionButtonInstance.GetComponentInChildren<TextMeshProUGUI>();
            
            int characterIndex = i;
            textMeshProUGUI.text = PartyManagerSingleton.Instance.GetPartyMember(characterIndex).Name;
            
            button.onClick.AddListener(() =>
            {
                onButtonClicked(characterIndex);
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
