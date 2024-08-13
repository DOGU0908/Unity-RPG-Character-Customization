using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class CharacterInfo
{
    // body
    [SerializeField] private BodyPart[] bodyParts;

    [SerializeField] private int hairColorIndex;
    [SerializeField] private int eyeColorIndex;
    [SerializeField] private int skinColorIndex;

    // stats
    [SerializeField] private StatSet baseStats = new StatSet(27, 13, 6, 8, 9, 6, 6);
    private static readonly int StatIncreaseValue = 1;
    
    [SerializeField] private int level = 1;
    private int _exp = 0;
    public int Exp
    {
        get => _exp;
        set
        {
            _exp = value;
            
            while (_exp >= MaxExpPerLevel)
            {
                LevelUp();
            }
        }
    }
    private static readonly int MaxExpPerLevel = 100;
    
    private int _statPoint = 0;
    private static readonly int LevelUpStatPoints = 3;
    
    // equipments
    [SerializeField] private int weaponId = 44;
    
    // base character prefab
    private static readonly GameObject BaseCharacterBattlePrefab =
        Resources.Load<GameObject>("Prefabs/Character/BattleCharacterBody");
    private static readonly GameObject BaseCharacterFieldPrefab =
        Resources.Load<GameObject>("Prefabs/Character/FieldCharacterBody");
    
    public CharacterInfo(BodyPart[] bodyParts, int hairColorIndex, int eyeColorIndex, int skinColorIndex)
    {
        this.bodyParts = bodyParts;
        this.hairColorIndex = hairColorIndex;
        this.eyeColorIndex = eyeColorIndex;
        this.skinColorIndex = skinColorIndex;
    }

    // instantiate prefab
    public GameObject InstantiateFieldCharacter(Vector3 spawnPosition)
    {
        GameObject playerCharacter = Object.Instantiate(CharacterInfo.BaseCharacterFieldPrefab,
            PlayerDataManagerSingleton.Instance.PlayerLastLocation, Quaternion.identity);

        SpriteManager spriteManager = playerCharacter.GetComponent<SpriteManager>();
            
        ApplyBodyAppearance(spriteManager);
        
        ApplyWeaponAppearance(spriteManager, WeaponCollection.Instance.GetWeapon(weaponId).Sprite);

        return playerCharacter;
    }
    
    // body
    private void ApplyBodyAppearance(SpriteManager spriteManager)
    {
        if (spriteManager == null)
        {
            return;
        }
        
        foreach (BodyPart bodyPart in bodyParts)
        {
            spriteManager.SetBodySprite(bodyPart.BodyPartType,
                BodySpriteCollection.Instance.GetSprite(bodyPart.BodyPartType, bodyPart.Index));
        }

        spriteManager.SetHairColor(BodySpriteCollection.Instance.GetColorData(ColorType.Base, hairColorIndex)
            .Color);
        spriteManager.SetEyeColor(BodySpriteCollection.Instance.GetColorData(ColorType.Base, eyeColorIndex)
            .Color);
        spriteManager.SetBodyColor(BodySpriteCollection.Instance.GetColorData(ColorType.Skin, skinColorIndex)
            .Color);
    }
    
    //stats
    public void LevelUp()
    {
        ++level;

        _exp -= MaxExpPerLevel;

        _statPoint += LevelUpStatPoints;
    }

    public void IncreaseStat(StatType statType)
    {
        if (_statPoint < 1)
        {
            return;
        }
        
        baseStats[statType] += StatIncreaseValue;

        --_statPoint;
    }
    
    // equipments
    public Weapon GetEquippedWeapon()
    {
        return WeaponCollection.Instance.GetWeapon(weaponId);
    }

    public void ChangeWeapon(int id, SpriteManager spriteManager)
    {
        Weapon newWeapon = WeaponCollection.Instance.GetWeapon(id);

        if (newWeapon == null)
        {
            return;
        }
        
        // TODO: change inventory

        weaponId = id;
        ApplyWeaponAppearance(spriteManager, newWeapon.Sprite);
    }

    private void ApplyWeaponAppearance(SpriteManager spriteManager, Sprite sprite)
    {
        spriteManager.SetWeaponSprite(sprite);
    }
}

[Serializable]
public struct BodyPart
{
    [SerializeField] private BodyPartType bodyPartType;
    public BodyPartType BodyPartType => bodyPartType;
        
    [SerializeField] private int index;
    public int Index
    {
        get => index;
        set => index = Mathf.Clamp(value, 0, BodySpriteCollection.Instance.GetSpritesCount(bodyPartType) - 1);
    }

    public BodyPart(BodyPartType bodyPartType, int index)
    {
        this.bodyPartType = bodyPartType;
        this.index = index;
    }
}

public enum StatType
{
    MaxHealth,
    Strength,
    Magic,
    Speed,
    Dexterity,
    Defense,
    Resistance,
}

[Serializable]
public class StatSet
{
    [SerializeField] private int[] stats;

    public StatSet(int maxHealth, int strength, int magic, int speed, int dexterity, int defense, int resistance)
    {
        stats = new int[Enum.GetValues(typeof(StatType)).Length];
        stats[(int)StatType.MaxHealth] = maxHealth;
        stats[(int)StatType.Strength] = strength;
        stats[(int)StatType.Magic] = magic;
        stats[(int)StatType.Speed] = speed;
        stats[(int)StatType.Dexterity] = dexterity;
        stats[(int)StatType.Defense] = defense;
        stats[(int)StatType.Resistance] = resistance;
    }

    public int this[StatType statType]
    {
        get => stats[(int)statType];
        set => stats[(int)statType] = value;
    }
}