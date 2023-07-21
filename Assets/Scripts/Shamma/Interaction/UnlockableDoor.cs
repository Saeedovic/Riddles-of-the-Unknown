using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : PointOfInterest, IInteractableObject
{
    public void Interact(PlayerInteractor user)
    {
        for (int i = 0; i < 6; i++)
        {
            // check for which object is the key
            if (user.inventoryHandler.InventorySlots[i].storedItem is KeyObject)
            {
                // use the key, make sure key-holding status is set to false if we have no more keys 
                user.inventoryHandler.InventorySlots[i].DiscardItem();

                if (user.inventoryHandler.InventorySlots[i].storedItem == null)
                    user.inventoryHandler.UpdateKeyObjectValue(false);

                Debug.Log("door was opened!");
                gameObject.SetActive(false);

                break;
            }
        }
    }

    public bool IsInteractable()
    {
        return InventoryHandler.containsKeyObject; // only interactable if you've got a key
    }

    public void OnDeHighlight() { }

    public void OnHighlight() { }

}
