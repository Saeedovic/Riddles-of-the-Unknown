using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReplenishingObj", menuName = "Inventory/ReplenishingObject")]
public class ReplenishingObject : InventoryObject
{
    // determine if this object should refill thirst or hunger.
    [SerializeField] ReplenishingObjType type = ReplenishingObjType.Null;

    public override bool OnUse()
    {
        if (type == ReplenishingObjType.Water)
        {
            ThirstSystem.Instance.RefillThirst();
            //AudioSource.PlayClipAtPoint(userThirst.AudioForDrinking, transform.position);
            ThirstSystem.Instance.drankwater = true;

            return true;
        }
        else if (type == ReplenishingObjType.Food)
        {
            Debug.Log("Food eaten");
            HungerSystem.Instance.RefillHunger();
            return true;
        }

        // invalid types
        return false;
    }

    public override bool IsDroppable()
    {
        return true;
    }
}

enum ReplenishingObjType
{
    Food,
    Water,
    Null
}