using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryObject : ScriptableObject
{
    public Sprite itemImage;

    public abstract bool OnUse();
    
}
