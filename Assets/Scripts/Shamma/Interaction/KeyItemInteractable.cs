using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class KeyItemInteractable : PointOfInterest, IInteractableObject
{
    [SerializeField] bool isCollectable = true;
    [SerializeField] KeyObject keyObject;
    public static bool hasBeenCollected = false; // probably want to change this to an event call of some kind
    

    public void Interact(PlayerInteractor user)
    {
        if (isCollectable)
        {
            user.inventoryHandler.AddToInventory(keyObject, 1);

            if (keyObject.countsAsAnUnlockingKey)
            {
                user.inventoryHandler.UpdateKeyObjectValue(true);
            }

            hasBeenCollected = true;
            gameObject.SetActive(false);

            Debug.Log("you've picked up a key");
        }
        else
        {
            Debug.Log("you can't pick this up right now.");
        }
    }

    public bool IsInteractable()
    {
        return true;
    }


    public void OnDeHighlight() { }

    public void OnHighlight() { }
}
