using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // the item that occupies this slot.
    [HideInInspector] public InventoryObject storedItem;
    [HideInInspector] public int itemCount;

    Texture2D slotImage;
    Texture2D defaultImage;

    public void Start()
    {
        Button slotUIButton = GetComponent<Button>();
        slotUIButton.onClick.AddListener(UseItem);

        slotImage = GetComponent<Texture2D>();
        defaultImage = slotImage;
    }

    public void AddItem(InventoryObject itemToAdd, int numOf)
    {
        storedItem = itemToAdd;
        IncrementItem(numOf);
        slotImage = itemToAdd.itemImage;

        Debug.Log("A " + itemToAdd.name + " was added to " + this.name);

    }

    public void IncrementItem(int numOf)
    {
        itemCount += numOf;
    }

    public void UseItem()
    {
        if (storedItem.OnUse())
        {
            Debug.Log("A " + storedItem.name + " was used from " + this.name);
            itemCount--;

            if (itemCount <= 0) 
            {
                storedItem = null;
                slotImage = defaultImage;
            }
                
        }
    }
}
