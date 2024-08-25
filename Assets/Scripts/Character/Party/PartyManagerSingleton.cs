using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartyManagerSingleton : MonoBehaviour
{
    public static PartyManagerSingleton Instance { get; private set; }
    
    private readonly List<CharacterInfo> _companionList = new();
    public List<CharacterInfo> CompanionList => _companionList;
    private readonly List<int> _battleMemberIndex = new();
    
    private const int MaxBattleMemberCount = 3;
    
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
        if (GetPartyMember(characterInfo.Id) != null)
        {
            return;
        }
        
        _companionList.Add(characterInfo);

        if (_battleMemberIndex.Count < MaxBattleMemberCount)
        {
            _battleMemberIndex.Add(characterInfo.Id);
        }
    }

    public CharacterInfo GetPartyMember(int id)
    {
        return _companionList.FirstOrDefault(characterInfo => characterInfo.Id == id);
    }
}
