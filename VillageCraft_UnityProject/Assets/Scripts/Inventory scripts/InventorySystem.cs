using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject inventory;
    bool activeInventory = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // faire apparaitre et disparaitre l'inventaire
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            /*if(activeInventory == false)
            {
                activeInventory = true;
            }
            else if (activeInventory == true)
            {
                activeInventory = false;
            }*/
            activeInventory = !activeInventory;
            inventory.SetActive(activeInventory);
        }
    }
}
