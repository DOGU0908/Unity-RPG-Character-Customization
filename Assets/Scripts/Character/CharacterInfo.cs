using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterInfo
{
    // body
    [Serializable]
    public struct BodyPart
    {
        [SerializeField] private BodyPartType bodyPartType;
        public BodyPartType BodyPartType => bodyPartType;
        
        [SerializeField] private int index;
        public int Index
        {
            get => index;
            set => index = _clampIndex(value, BodySpriteCollection.Instance.GetSpritesCount(bodyPartType) - 1);
        }

        public BodyPart(BodyPartType bodyPartType, int index)
        {
            this.bodyPartType = bodyPartType;
            this.index = index;
        }
    }

    [SerializeField] private BodyPart[] bodyParts;

    [SerializeField] private int hairColorIndex;
    [SerializeField] private int eyeColorIndex;
    [SerializeField] private int skinColorIndex;

    // stats
    // TODO

    // equipments
    // TODO
    
    // base character prefab
    private static GameObject baseCharacterPrefab = Resources.Load<GameObject>("Prefabs/Character/CharacterBody");
    // todo: add instantiating method
    
    private static Func<int, int, int> _clampIndex = (index, maxValue) => Mathf.Clamp(index, 0, maxValue);
    
    public CharacterInfo(BodyPart[] bodyParts, int hairColorIndex, int eyeColorIndex, int skinColorIndex)
    {
        this.bodyParts = bodyParts;
        this.hairColorIndex = hairColorIndex;
        this.eyeColorIndex = eyeColorIndex;
        this.skinColorIndex = skinColorIndex;
    }
}
