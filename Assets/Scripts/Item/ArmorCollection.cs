using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorCollection", menuName = "Collection/ArmorCollection")]
public class ArmorCollection : ScriptableObject
{
    private static ArmorCollection _instance;

    [SerializeField] private Armor[] armors;

    public Armor GetArmor(int id)
    {
        return armors.FirstOrDefault(armor => armor.Id == id);
    }

    public static ArmorCollection Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<ArmorCollection>("ArmorCollection");
            }

            return _instance;
        }
    }
}
