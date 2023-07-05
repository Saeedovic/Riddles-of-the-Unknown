using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryHandler : MonoBehaviour
{

    // in our case we specifically want six slots
    // can set them in inspector for now.
    [SerializeField] InventorySlot[] inventorySlots = new InventorySlot[6];
    public List<InventorySlot> InventorySlots
    {
        get
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                InventorySlots.Add(slot);
            } 
            return InventorySlots; 
        } 
    } // for the other scripts to read and not write to inventory.
    // nooo how do we convert list to array?? 


    public bool AddToInventory(InventoryObject itemToAdd, int numOf)
    {
        if (inventorySlots[0].storedItem != null &&
            inventorySlots[1].storedItem != null &&
            inventorySlots[2].storedItem != null &&
            inventorySlots[3].storedItem != null &&
            inventorySlots[4].storedItem != null)
        {

        }

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
