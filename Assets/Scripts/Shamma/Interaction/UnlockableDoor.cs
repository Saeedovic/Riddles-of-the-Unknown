using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : PointOfInterest, IInteractableObject
{
    [SerializeField] KeyObject keyType;
    [SerializeField] GameObject keyNeededTextbox;
    [SerializeField] float boxDisplayTime = 2f;
    bool noKeyfound = true;

    public void Interact(PlayerInteractor user)
    {
        if (!InventoryHandler.containsKeyObject)
        {
            StartCoroutine(DisplayKeyNeededTextbox());
            return;
        }

        for (int i = 0; i < 6; i++)
        {
            // check for which object is the key
            if (user.inventoryHandler.InventorySlots[i].storedItem == keyType)
            {
                // use the key, make sure key-holding status is set to false if we have no more keys 
                user.inventoryHandler.InventorySlots[i].DiscardItem();

                if (user.inventoryHandler.InventorySlots[i].storedItem == null)
                    user.inventoryHandler.UpdateKeyObjectValue(false);

                KeyItemInteractable.hasBeenCollected = false;

                // would want to play the door opening anim here.

                Debug.Log("door was opened!");
                noKeyfound = false;
                gameObject.SetActive(false);

                break;
            }
        }

        if (noKeyfound)
        {
            StartCoroutine(DisplayKeyNeededTextbox());
        }
    }

    IEnumerator DisplayKeyNeededTextbox()
    {
        keyNeededTextbox.SetActive(true);

        yield return new WaitForSeconds(boxDisplayTime);

        keyNeededTextbox.SetActive(false);
    }

    public bool IsInteractable() { return true; }

    public void OnDeHighlight() { }

    public void OnHighlight() { }

}
