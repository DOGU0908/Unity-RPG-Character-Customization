using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class FieldManager : MonoBehaviour
{
    private const int PlayerCharacterIndex = 0;
    
    private void Start()
    {
        CharacterInfo playerCharacterInfo = PartyManagerSingleton.Instance.GetPartyMember(PlayerCharacterIndex);

        if (playerCharacterInfo != null)
        {
            GameObject playerCharacter = Object.Instantiate(CharacterInfo.BaseCharacterFieldPrefab,
                PlayerDataManagerSingleton.Instance.PlayerLastLocation, Quaternion.identity);

            SpriteManager spriteManager = playerCharacter.GetComponent<SpriteManager>();
            
            playerCharacterInfo.ApplyBodyAppearance(spriteManager);
        }
    }
}
