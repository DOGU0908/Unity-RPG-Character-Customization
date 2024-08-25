using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    public string ItemName => itemName;
}

public enum ItemType
{
    Weapon,
    Armor,
}