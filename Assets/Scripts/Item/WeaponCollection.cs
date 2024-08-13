using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCollection", menuName = "Collection/WeaponCollection")]
public class WeaponCollection : ScriptableObject
{
    private static WeaponCollection _instance;

    [SerializeField] private Weapon[] weapons;

    public Weapon GetWeapon(int id)
    {
        return weapons.FirstOrDefault(weapon => weapon.Id == id);
    }
    
    public static WeaponCollection Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<WeaponCollection>("WeaponCollection");
            }

            return _instance;
        }
    }
}