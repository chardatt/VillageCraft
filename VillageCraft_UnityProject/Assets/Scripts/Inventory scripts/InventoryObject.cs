using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    //cr�ation d'une liste d'item slot
    public List<InventorySlot> Container = new List<InventorySlot>();
    // on v�rifie que l'objet que l'on va mettre dans la liste n'est pas encore comprise dans cette liste
    public void AddItem(ItemObject _item, int _amount)
    {
        // on consid�re que le joueur n'a pas l'item qu'il a ramass�
        bool hasItem = false;
        // parcour la liste
        for(int i = 0; i < Container.Count; i++)
        {
            // si dans la liste, un item contien le m�me nom que l'item re�u, 
            if(Container[i].item == _item)
            {
                // on ajoute une quantit� +1 et on affirme que l'on a d�j� l'item
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            // si le l'oppos� du bool alors on ajoute a l'inventaire l'item et la quantit�
            Container.Add(new InventorySlot(_item, _amount));
        }
    }
}
[System.Serializable]
//cr�ation d'une m�thode liste InventorySlot
public class InventorySlot
{
    // dans le scripte itemObject on va y donner le nom d'un item et sa quantit�
    public ItemObject item;
    public int amount;
    // on assigne dans la liste m�thode le scripte itemObject avec un _ pour diff�rencer les deux m�thodes et scriptes
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    } 
    // m�thode pour ajouter un montant
    public void AddAmount(int value)
    {
        amount += value;
    }
}
