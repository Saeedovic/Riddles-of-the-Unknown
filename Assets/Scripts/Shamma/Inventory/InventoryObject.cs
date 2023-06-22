using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for inventory scriptableobjects to inherit from. 
public abstract class InventoryObject : ScriptableObject
{
    // the icon for the item when in the inventory.
    public Sprite itemImage;

    // try using this item. return true if it could successfully be used (so we don't discard items we didn't use)
    public abstract bool OnUse();

    // return true if player is allowed to discard the item.
    public abstract bool IsDroppable();
    
}
