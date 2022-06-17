using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public List<Item> itemList = new List<Item>();
    bool listUpdate = false;

    private void Update()
    {
        if (itemList.Count > 0)
        {
            Item item;
            item = itemList[0];
            if (listUpdate)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (Vector3.Distance(item.gameObject.transform.position, transform.position) > Vector3.Distance(itemList[i].gameObject.transform.position, transform.position))
                    {
                        item = itemList[i];
                    }
                }
            }

            if (Input.GetButtonDown("GetItem"))
            {
                EnterItemInInventory(item);
            }
        }
    }

    void EnterItemInInventory(Item item)
    {
        if (item)
        {
            inventory.AddItem(item.item, 1);
            itemList.Remove(item);
            Destroy(item.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnterTrigger " + other.gameObject.name);
        if (other.GetComponent<Item>())
        {
            var item = other.GetComponent<Item>();
            if (!itemList.Contains(item))
            {
                itemList.Add(item);
                listUpdate = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (itemList.Contains(other.GetComponent<Item>()))
        {
            itemList.Remove(other.GetComponent<Item>());
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
