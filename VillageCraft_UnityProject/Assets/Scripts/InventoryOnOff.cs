using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOnOff : MonoBehaviour
{
    public GameObject inventoryItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryItem.SetActive(!inventoryItem.activeSelf);
        }
    }
}
