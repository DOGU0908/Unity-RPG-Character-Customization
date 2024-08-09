using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }
    
    private readonly List<CharacterInfo> _companionList = new();
    private readonly List<CharacterInfo> _battleMemberList = new();
    
    private const int MaxBattleMemberCount = 4;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPartyMember(CharacterInfo characterInfo)
    {
        _companionList.Add(characterInfo);

        if (_battleMemberList.Count < MaxBattleMemberCount)
        {
            _battleMemberList.Add(characterInfo);
        }
        
        Debug.Log(_battleMemberList.Count);
    }
}
