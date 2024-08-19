using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    private readonly Dictionary<StatType, string> _statTypeNames = new Dictionary<StatType, string>
    {
        { StatType.Strength, "STR" },
        { StatType.Magic, "MAG" },
        { StatType.Health, "HP" },
        { StatType.Speed, "SPD" },
        { StatType.Dexterity, "DEX" },
        { StatType.Defense, "DEF" },
        { StatType.Resistance, "RES" },
    };

    private readonly Dictionary<WeaponType, string> _weaponTypeNames = new Dictionary<WeaponType, string>
    {
        { WeaponType.Sword, "Sword" },
        { WeaponType.Staff, "Staff" },
    };

    private readonly Dictionary<WeaponElement, string> _weaponElementNames = new Dictionary<WeaponElement, string>
    {
        { WeaponElement.None, "None" },
        { WeaponElement.Fire, "Fire" },
        { WeaponElement.Ice, "Ice" },
        { WeaponElement.Electric, "Electric" },
        { WeaponElement.Light, "Light" },
        { WeaponElement.Dark, "Dark" },
    };
    
    // weapon
    [SerializeField] private Button weaponButton;
    [SerializeField] private TextMeshProUGUI weaponDamageText;
    [SerializeField] private TextMeshProUGUI weaponStatTypeText;
    [SerializeField] private TextMeshProUGUI weaponTypeText;
    [SerializeField] private TextMeshProUGUI weaponElementText;
    
    [SerializeField] private GameObject weaponInventoryPanel;
    [SerializeField] private Transform weaponContentObject;
    
    // armor
    [SerializeField] private Button armorButton;
    [SerializeField] private TextMeshProUGUI armorDefenseText;
    [SerializeField] private TextMeshProUGUI armorResistanceText;

    [SerializeField] private GameObject armorInventoryPanel;
    [SerializeField] private Transform armorContentObject;

    [SerializeField] private GameObject equipmentButtonPrefab;

    public void SetupEquipmentInterface(CharacterInfo characterInfo, SpriteManager spriteManager)
    {
        weaponInventoryPanel.SetActive(false);
        armorInventoryPanel.SetActive(false);
        UpdateWeaponDataText(characterInfo.GetEquippedWeapon());
        UpdateArmorDataText(characterInfo.GetEquippedArmor());
        
        weaponButton.onClick.RemoveAllListeners();
        weaponButton.onClick.AddListener(() =>
        {
            armorInventoryPanel.SetActive(false);
            weaponInventoryPanel.SetActive(true);
            UpdateWeaponPanel((newWeaponId) =>
            {
                characterInfo.ChangeWeapon(newWeaponId, spriteManager);
            });
        });
        
        armorButton.onClick.RemoveAllListeners();
        armorButton.onClick.AddListener(() =>
        {
            weaponInventoryPanel.SetActive(false);
            armorInventoryPanel.SetActive(true);
            UpdateArmorPanel((newArmorId) =>
            {
                characterInfo.ChangeArmor(newArmorId, spriteManager);
            });
        });
    }

    private void UpdateWeaponPanel(Action<int> action)
    {
        DestroyChildObjects(weaponContentObject);

        foreach (int itemIndex in PlayerDataManagerSingleton.Instance.Inventory.GetItemList(ItemType.Weapon))
        {
            GameObject buttonGameObject = Instantiate(equipmentButtonPrefab, weaponContentObject);
            
            TextMeshProUGUI textMeshProUGUI = buttonGameObject.GetComponentInChildren<TextMeshProUGUI>();
            Weapon weapon = WeaponCollection.Instance.GetWeapon(itemIndex);
            textMeshProUGUI.text = weapon.ItemName;

            Button button = buttonGameObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                action(itemIndex);
                UpdateWeaponDataText(weapon);
                weaponInventoryPanel.SetActive(false);
            });
        }
    }

    private void UpdateWeaponDataText(Weapon weapon)
    {
        TextMeshProUGUI weaponNameText = weaponButton.GetComponentInChildren<TextMeshProUGUI>();
        weaponNameText.text = weapon.ItemName;
        weaponDamageText.text = "Damage: " + weapon.Damage;
        weaponStatTypeText.text = "Stat: " + _statTypeNames[weapon.AttackStatType];
        weaponTypeText.text = "Weapon: " + _weaponTypeNames[weapon.WeaponType];
        weaponElementText.text = "Element: " + _weaponElementNames[weapon.WeaponElement];
    }

    private void UpdateArmorPanel(Action<int> action)
    {
        DestroyChildObjects(armorContentObject);

        foreach (int itemIndex in PlayerDataManagerSingleton.Instance.Inventory.GetItemList(ItemType.Armor))
        {
            GameObject buttonGameObject = Instantiate(equipmentButtonPrefab, armorContentObject);

            TextMeshProUGUI textMeshProUGUI = buttonGameObject.GetComponentInChildren<TextMeshProUGUI>();
            Armor armor = ArmorCollection.Instance.GetArmor(itemIndex);
            textMeshProUGUI.text = armor.ItemName;

            Button button = buttonGameObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                action(itemIndex);
                UpdateArmorDataText(armor);
                armorInventoryPanel.SetActive(false);
            });
        }
    }

    private void UpdateArmorDataText(Armor armor)
    {
        TextMeshProUGUI armorNameText = armorButton.GetComponentInChildren<TextMeshProUGUI>();
        armorNameText.text = armor.ItemName;
        armorDefenseText.text = "DEF: +" + armor.DefenseBonus;
        armorResistanceText.text = "RES: +" + armor.ResistanceBonus;
    }

    private void DestroyChildObjects(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
}
