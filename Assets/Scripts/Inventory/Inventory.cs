using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private readonly Dictionary<int, int> _weaponsInventory = new();
    private readonly Dictionary<int, int> _armorsInventory = new();

    public void AddItemToInventory(ItemType itemType, int index)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                if (!_weaponsInventory.TryAdd(index, 1))
                {
                    ++_weaponsInventory[index];
                }
                break;
            case ItemType.Armor:
                if (!_armorsInventory.TryAdd(index, 1))
                {
                    ++_armorsInventory[index];
                }
                break;
        }
    }

    public void RemoveItemFromInventory(ItemType itemType, int index)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                if (_weaponsInventory.ContainsKey(index))
                {
                    --_weaponsInventory[index];

                    if (_weaponsInventory[index] <= 0)
                    {
                        _weaponsInventory.Remove(index);
                    }
                }
                break;
            case ItemType.Armor:
                if (_armorsInventory.ContainsKey(index))
                {
                    --_armorsInventory[index];

                    if (_armorsInventory[index] <= 0)
                    {
                        _armorsInventory.Remove(index);
                    }
                }
                break;
        }
    }

    public List<int> GetItemList(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.Weapon => _weaponsInventory.Keys.ToList(),
            ItemType.Armor => _armorsInventory.Keys.ToList(),
            _ => null
        };
    }

    public int GetItemQuantity(ItemType itemType, int index)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                if (_weaponsInventory.TryGetValue(index, out int weaponQuantity))
                {
                    return weaponQuantity;
                }
                break;
            case ItemType.Armor:
                if (_armorsInventory.TryGetValue(index, out int armorQuantity))
                {
                    return armorQuantity;
                }
                break;
        }

        return 0;
    }
}
