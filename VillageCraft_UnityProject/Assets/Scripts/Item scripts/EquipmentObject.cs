using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//créer l'item type Equipement que l'on pourra modifier en therme de stats dans l'inspector
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public float attackBonus;
    public float defenseBonus;
    public void Awake()
    {
        type = ItemType.Equipement;
    }
}
