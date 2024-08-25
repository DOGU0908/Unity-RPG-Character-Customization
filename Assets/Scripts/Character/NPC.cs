using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private CharacterInfo characterInfo;
    
    [SerializeField] private SpriteManager spriteManager;

    // dialog
    [SerializeField] private bool isRecruitable;

    [SerializeField] private string dialogBaseText;
    [SerializeField] private string dialogRecruitText;

    private void Start()
    {
        characterInfo.ResetSprite(spriteManager);
    }

    public void Interact()
    {
        if (isRecruitable && PartyManagerSingleton.Instance.GetPartyMember(characterInfo.Id) == null)
        {
            DialogManager.Instance.DisplayDialogText(dialogRecruitText);
            PartyManagerSingleton.Instance.AddPartyMember(characterInfo);
        }
        else
        {
            DialogManager.Instance.DisplayDialogText(dialogBaseText);
        }
    }
}
