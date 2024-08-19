using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoUI : MonoBehaviour
{
    private int _currentCharacterInfoIndex;
    private GameObject _currentCharacterInstance;

    // use object pooling to reduce the number of times of ui character instantiation
    private readonly Dictionary<int, GameObject> _displayCharacterPool = new();

    private const int BaseCharacterInfoIndex = 0;
    
    // stat ui
    [SerializeField] private StatUI[] statUIs;
    
    // equipment ui
    [SerializeField] private EquipmentUI equipmentUI;
    
    // companion list ui
    [SerializeField] private CompanionUI companionUI;
    
    private void OnEnable()
    {
        ShowCharacter(BaseCharacterInfoIndex);
        companionUI.SetupCompanionInterface(ChangeCharacter);
    }

    private void OnDisable()
    {
        _currentCharacterInstance?.SetActive(false);
    }

    private void ShowCharacter(int characterInfoIndex)
    {
        _currentCharacterInstance?.SetActive(false);
        
        if (!_displayCharacterPool.TryGetValue(characterInfoIndex, out _currentCharacterInstance))
        {
            // instantiate new character object
            _currentCharacterInstance = Instantiate(CharacterInfo.BaseCharacterUIPrefab,
                PlayerDataManagerSingleton.Instance.PlayerLastLocation, Quaternion.identity);
            
            _displayCharacterPool.Add(characterInfoIndex, _currentCharacterInstance);
        }

        _currentCharacterInfoIndex = characterInfoIndex;
        _currentCharacterInstance.SetActive(true);
        
        // change appearance of the character object
        CharacterInfo characterInfo = PartyManagerSingleton.Instance.GetPartyMember(_currentCharacterInfoIndex);
        SpriteManager spriteManager = _currentCharacterInstance.GetComponent<SpriteManager>();
        characterInfo.ResetSprite(spriteManager);
        
        foreach (StatUI statUI in statUIs)
        {
            statUI.SetupStatInterface(characterInfo, statUIs);
        }

        equipmentUI.SetupEquipmentInterface(characterInfo, _currentCharacterInstance.GetComponent<SpriteManager>());
    }

    private void ChangeCharacter(int newCharacterInfoIndex)
    {
        if (newCharacterInfoIndex == _currentCharacterInfoIndex)
        {
            return;
        }
        
        ShowCharacter(newCharacterInfoIndex);
    }
}
