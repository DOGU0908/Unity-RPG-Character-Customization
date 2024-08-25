using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManagerSingleton : MonoBehaviour
{
    public static PlayerDataManagerSingleton Instance { get; private set; }
    
    public int PlayerLastSceneIndex { get; private set; } = 1;
    public Vector3 PlayerLastLocation { get; private set; } = Vector3.zero;
    
    public Inventory Inventory { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Inventory = new Inventory();
            
            // testing feature code
            Inventory.AddItemToInventory(ItemType.Armor, 1);
            Inventory.AddItemToInventory(ItemType.Armor, 2);
            Inventory.AddItemToInventory(ItemType.Armor, 3);
            Inventory.AddItemToInventory(ItemType.Weapon, 1);
            Inventory.AddItemToInventory(ItemType.Weapon, 2);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SavePlayerLastLocation(Vector3 playerLocation, int sceneIndex)
    {
        PlayerLastLocation = playerLocation;
        PlayerLastSceneIndex = sceneIndex;
    }
}
