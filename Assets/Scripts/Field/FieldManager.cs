using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class FieldManager : MonoBehaviour
{
    // only exists in field scene
    public static FieldManager Instance { get; private set; }

    [SerializeField] private int battleSceneIndex;

    [SerializeField] private bool isBattleEncounterEnabled;
    public bool IsBattleEncounterEnabled => isBattleEncounterEnabled;
    
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
        CharacterInfo playerCharacterInfo = PartyManagerSingleton.Instance.GetPartyMember(PlayerCharacterIndex);

        if (playerCharacterInfo != null)
        {
            playerCharacterInfo.InstantiateFieldCharacter(PlayerDataManagerSingleton.Instance.PlayerLastLocation);
        }
    }
}
