using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for any items that are crucial to story and game progression. (eg dynamite, literal keys)

[CreateAssetMenu(fileName = "KeyObj", menuName = "Inventory/KeyObject")]
public class KeyObject : InventoryObject
{
    [SerializeField] Vector3 locationToBeUsed;
    [SerializeField] float checkRadius = 5f;
    [SerializeField] LayerMask playerLayer;
    public bool countsAsAnUnlockingKey = true;

    public override bool OnUse()
    {
        // never mind, in this case key should just sit in inventory for door interaction purposes :p
        /*
        // probably want to do some sort of check for if you're in the corrrect location?
        if (Physics.CheckSphere(locationToBeUsed, checkRadius, playerLayer))
        {
            // execute the code relevant to the key being used here (eg calling a lockopening function)
            // could maybe make inheriting classes that change what execute use does for different key items
            ExecuteUse();
            return true;
        }*/

        // when if statement doesn't execute
        Debug.Log("you can't use this item here.");
        return false;
    }

    void ExecuteUse()
    {
        // don't really need to do anything atm
    }

    // cannot drop key items
    public override bool IsDroppable()
    {
        return false;
    }

}
