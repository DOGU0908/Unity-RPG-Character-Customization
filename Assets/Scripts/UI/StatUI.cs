using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUI : MonoBehaviour
{
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Button button;

    public void SetupStatInterface(CharacterInfo characterInfo, StatUI[] statUIs)
    {
        UpdateText(characterInfo.BaseStats[statType]);
        button.interactable = characterInfo.CanIncreaseStat();
        
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            characterInfo.IncreaseStat(statType);
            UpdateText(characterInfo.BaseStats[statType]);
            CheckButtonsEnable(characterInfo.CanIncreaseStat(), statUIs);
        });
    }
    
    private void UpdateText(int value)
    {
        valueText.text = value.ToString();
    }

    private void CheckButtonsEnable(bool isEnable, StatUI[] statUIs)
    {
        foreach (StatUI statUI in statUIs)
        {
            statUI.button.interactable = isEnable;
        }
    }
}
