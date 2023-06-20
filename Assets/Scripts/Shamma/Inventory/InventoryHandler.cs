using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryHandler : MonoBehaviour
{

    // in our case we specifically want six slots
    // can set them in inspector for now.
    [SerializeField] InventorySlot[] inventorySlots = new InventorySlot[6];

    public bool AddToInventory(InventoryObject itemToAdd, int numOf)
    {

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].storedItem == null)
            {
                inventorySlots[i].AddItem(itemToAdd, numOf);
                return true;
            }

            if (inventorySlots[i].storedItem == itemToAdd)
            {
                inventorySlots[i].IncrementItem(numOf);
                return true;
            }
        }

        // if no slots could be allocated, send error message 
        Debug.LogError("no slots left!");
        return false;
    }

}
