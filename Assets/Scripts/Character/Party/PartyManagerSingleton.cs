using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManagerSingleton : MonoBehaviour
{
    public static PartyManagerSingleton Instance { get; private set; }
    
    private readonly List<CharacterInfo> _companionList = new();
    private readonly List<int> _battleMemberIndex = new();
    
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

        if (_battleMemberIndex.Count < MaxBattleMemberCount)
        {
            _battleMemberIndex.Add(_companionList.Count - 1);
        }
    }

    public CharacterInfo GetPartyMember(int index)
    {
        return _companionList[Mathf.Clamp(index, 0, _companionList.Count - 1)];
    }
}
