using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    //création d'une liste d'item slot
    public List<InventorySlot> Container = new List<InventorySlot>();
    // on vérifie que l'objet que l'on va mettre dans la liste n'est pas encore comprise dans cette liste
    public void AddItem(ItemObject _item, int _amount)
    {
        // on considère que le joueur n'a pas l'item qu'il a ramassé
        bool hasItem = false;
        // parcour la liste
        for(int i = 0; i < Container.Count; i++)
        {
            // si dans la liste, un item contien le même nom que l'item reçu, 
            if(Container[i].item == _item)
            {
                // on ajoute une quantité +1 et on affirme que l'on a déjà l'item
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            // si le l'opposé du bool alors on ajoute a l'inventaire l'item et la quantité
            Container.Add(new InventorySlot(_item, _amount));
        }
    }
}
[System.Serializable]
//création d'une méthode liste InventorySlot
public class InventorySlot
{
    // dans le scripte itemObject on va y donner le nom d'un item et sa quantité
    public ItemObject item;
    public int amount;
    // on assigne dans la liste méthode le scripte itemObject avec un _ pour différencer les deux méthodes et scriptes
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    } 
    // méthode pour ajouter un montant
    public void AddAmount(int value)
    {
        amount += value;
    }
}
