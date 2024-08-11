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
    public static readonly GameObject BaseCharacterBattlePrefab =
        Resources.Load<GameObject>("Prefabs/Character/BattleCharacterBody");
    public static readonly GameObject BaseCharacterFieldPrefab =
        Resources.Load<GameObject>("Prefabs/Character/FieldCharacterBody");
    
    private static Func<int, int, int> _clampIndex = (index, maxValue) => Mathf.Clamp(index, 0, maxValue);
    
    public CharacterInfo(BodyPart[] bodyParts, int hairColorIndex, int eyeColorIndex, int skinColorIndex)
    {
        this.bodyParts = bodyParts;
        this.hairColorIndex = hairColorIndex;
        this.eyeColorIndex = eyeColorIndex;
        this.skinColorIndex = skinColorIndex;
    }

    public void ApplyBodyAppearance(SpriteManager spriteManager)
    {
        if (spriteManager == null)
        {
            return;
        }
        
        foreach (BodyPart bodyPart in bodyParts)
        {
            spriteManager.ChangeBodySprite(bodyPart.BodyPartType,
                BodySpriteCollection.Instance.GetSprite(bodyPart.BodyPartType, bodyPart.Index));
        }

        spriteManager.ChangeHairColor(BodySpriteCollection.Instance.GetColorData(ColorType.Base, hairColorIndex)
            .Color);
        spriteManager.ChangeEyeColor(BodySpriteCollection.Instance.GetColorData(ColorType.Base, eyeColorIndex)
            .Color);
        spriteManager.ChangeBodyColor(BodySpriteCollection.Instance.GetColorData(ColorType.Skin, skinColorIndex)
            .Color);
    }
}
