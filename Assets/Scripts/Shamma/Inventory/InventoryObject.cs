using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryObject : ScriptableObject
{
    public Texture2D itemImage;

    public abstract bool OnUse();
    
}
