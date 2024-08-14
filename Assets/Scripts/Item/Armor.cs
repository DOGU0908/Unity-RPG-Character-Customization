using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "Item/Armor")]
public class Armor : Item
{
    [SerializeField] private int id;
    public int Id => id;

    [SerializeField] private int defenseBonus;
    [SerializeField] private int resistanceBonus;

    [SerializeField] private BattleAttribute armorBattleAttribute;

    [SerializeField] private ArmorSprite sprite;
    public ArmorSprite Sprite => sprite;
}

[Serializable]
public class ArmorSprite
{
    [SerializeField] private Sprite leftArmArmor;
    [SerializeField] private Sprite rightArmArmor;
    [SerializeField] private Sprite fingerArmor;
    [SerializeField] private Sprite leftForearmArmor;
    [SerializeField] private Sprite rightForearmArmor;
    [SerializeField] private Sprite leftHandArmor;
    [SerializeField] private Sprite rightHandArmor;
    [SerializeField] private Sprite legArmor;
    [SerializeField] private Sprite pelvisArmor;
    [SerializeField] private Sprite shinArmor;
    [SerializeField] private Sprite torsoArmor;

    public Sprite this[ArmorPart armorPart]
    {
        get
        {
            return armorPart switch
            {
                ArmorPart.LeftArm => leftArmArmor,
                ArmorPart.RightArm => rightArmArmor,
                ArmorPart.Finger => fingerArmor,
                ArmorPart.LeftForearm => leftForearmArmor,
                ArmorPart.RightForearm => rightForearmArmor,
                ArmorPart.LeftHand => leftHandArmor,
                ArmorPart.RightHand => rightHandArmor,
                ArmorPart.Leg => legArmor,
                ArmorPart.Pelvis => pelvisArmor,
                ArmorPart.Shin => shinArmor,
                ArmorPart.Torso => torsoArmor,
                _ => null,
            };
        }
    }
}

public enum ArmorPart
{
    LeftArm,
    RightArm,
    Finger,
    LeftForearm,
    RightForearm,
    LeftHand,
    RightHand,
    Leg,
    Pelvis,
    Shin,
    Torso,
}

// TODO: move to battle units class file
[Serializable]
public class BattleAttribute
{
    [SerializeField] private int avoidChance;
    [SerializeField] private int hitChance;
    [SerializeField] private int criticalChance;

    [SerializeField] private int nonElementDamageBonus;
    [SerializeField] private int fireDamageBonus;
    [SerializeField] private int iceDamageBonus;
    [SerializeField] private int electricDamageBonus;
    [SerializeField] private int lightDamageBonus;
    [SerializeField] private int darkDamageBonus;

    public int GetDamageBonus(WeaponElement weaponElement)
    {
        return weaponElement switch
        {
            WeaponElement.None => nonElementDamageBonus,
            WeaponElement.Fire => fireDamageBonus,
            WeaponElement.Ice => iceDamageBonus,
            WeaponElement.Electric => electricDamageBonus,
            WeaponElement.Light => lightDamageBonus,
            WeaponElement.Dark => darkDamageBonus,
            _ => 0,
        };
    }

    public void Add(BattleAttribute battleAttribute)
    {
        avoidChance += battleAttribute.avoidChance;
        hitChance += battleAttribute.hitChance;
        criticalChance += battleAttribute.criticalChance;

        nonElementDamageBonus += battleAttribute.nonElementDamageBonus;
        fireDamageBonus += battleAttribute.fireDamageBonus;
        iceDamageBonus += battleAttribute.iceDamageBonus;
        electricDamageBonus += battleAttribute.electricDamageBonus;
        lightDamageBonus += battleAttribute.lightDamageBonus;
        darkDamageBonus += battleAttribute.darkDamageBonus;
    }

    public void Subtract(BattleAttribute battleAttribute)
    {
        avoidChance -= battleAttribute.avoidChance;
        hitChance -= battleAttribute.hitChance;
        criticalChance -= battleAttribute.criticalChance;

        nonElementDamageBonus -= battleAttribute.nonElementDamageBonus;
        fireDamageBonus -= battleAttribute.fireDamageBonus;
        iceDamageBonus -= battleAttribute.iceDamageBonus;
        electricDamageBonus -= battleAttribute.electricDamageBonus;
        lightDamageBonus -= battleAttribute.lightDamageBonus;
        darkDamageBonus -= battleAttribute.darkDamageBonus;
    }
}
