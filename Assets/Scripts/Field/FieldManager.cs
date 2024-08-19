using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    // only exists in field scene
    public static FieldManager Instance { get; private set; }

    // scene settings
    [SerializeField] private int battleSceneIndex;

    [SerializeField] private bool isBattleEncounterEnabled;
    public bool IsBattleEncounterEnabled => isBattleEncounterEnabled;
    
    // player camera
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    
    // player field character
    private CharacterInfo _playerCharacterInfo;
    private SpriteManager _playerFieldSpriteManager;
    
    // base data
    private const int PlayerCharacterIndex = 0;

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

    private void Start()
    {
        _playerCharacterInfo = PartyManagerSingleton.Instance.GetPartyMember(PlayerCharacterIndex);

        if (_playerCharacterInfo != null)
        {
            GameObject playerCharacter = Instantiate(CharacterInfo.BaseCharacterFieldPrefab,
                PlayerDataManagerSingleton.Instance.PlayerLastLocation, Quaternion.identity);

            _playerFieldSpriteManager = playerCharacter.GetComponent<SpriteManager>();
            ResetFieldCharacterSprite();
            
            playerCamera.Follow = playerCharacter.transform;
        }
        
        // TODO: Erase TEST Inventory
        PlayerDataManagerSingleton.Instance.Inventory.AddItemToInventory(ItemType.Weapon, 1);
        PlayerDataManagerSingleton.Instance.Inventory.AddItemToInventory(ItemType.Weapon, 2);
        PlayerDataManagerSingleton.Instance.Inventory.AddItemToInventory(ItemType.Weapon, 1);
        PlayerDataManagerSingleton.Instance.Inventory.AddItemToInventory(ItemType.Weapon, 2);
        PlayerDataManagerSingleton.Instance.Inventory.AddItemToInventory(ItemType.Armor, 1);
        PlayerDataManagerSingleton.Instance.Inventory.AddItemToInventory(ItemType.Armor, 2);
        PlayerDataManagerSingleton.Instance.Inventory.AddItemToInventory(ItemType.Armor, 1);
        PlayerDataManagerSingleton.Instance.Inventory.AddItemToInventory(ItemType.Armor, 2);
    }

    public void ResetFieldCharacterSprite()
    {
        _playerCharacterInfo.ResetSprite(_playerFieldSpriteManager);
    }
}
