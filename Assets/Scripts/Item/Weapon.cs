using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    [SerializeField] private int id;
    public int Id => id;
    
    [SerializeField] private int damage;
    [SerializeField] private StatType attackStatType;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private WeaponElement weaponElement;
    
    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;
}

public enum WeaponType
{
    Sword,
    Staff,
}

public enum WeaponElement
{
    None,
    Fire,
    Ice,
    Electric,
    Light,
    Dark,
}