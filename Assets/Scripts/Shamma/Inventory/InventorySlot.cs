using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // the item that occupies this slot.
    [HideInInspector] public InventoryObject storedItem;
    [HideInInspector] public int itemCount;

    [SerializeField] Image slotImage; // i don't have the energy to try and fix the issue of not really being able to call start with anything tied to the phone, so in the meantime please assign these manually to the same image as on the gameobject
    Image defaultImage;

    [SerializeField] PhoneInventoryApp inventoryApp;


    public void Awake()
    {
        Button slotUIButton = GetComponent<Button>();
        slotUIButton.onClick.AddListener(ActivateConfirmationBox);

        //slotImage = GetComponent<Image>();
        defaultImage = slotImage;
        defaultImage.overrideSprite = slotImage.sprite;

        //this.gameObject.SetActive(false); // turn off after setup so phone won't open on inventory
    }

    void ActivateConfirmationBox()
    {
        if (storedItem != null)
        {
            inventoryApp.OpenConfirmationBox(this);
        }
    }


    public void AddItem(InventoryObject itemToAdd, int numOf)
    {
        defaultImage = slotImage;
        defaultImage.overrideSprite = slotImage.sprite;
        slotImage.overrideSprite = itemToAdd.itemImage;

        storedItem = itemToAdd;
        IncrementItem(numOf);

        Debug.Log("A " + itemToAdd.name + " was added to " + this.name);

    }

    public void IncrementItem(int numOf)
    {
        if (storedItem != null) // imagine having 3x null in your inventory...
        {
            itemCount += numOf;
            // update ui here.
        }
    }


    public void UseItem()
    {
        if (storedItem != null)
        {
            if (storedItem.OnUse())
            {
                Debug.Log("A " + storedItem.name + " was used from " + this.name);

                DiscardItem();
            }
        }
    }

    public void DiscardItem()
    {
        if (storedItem != null)
        {
            itemCount--;

            if (itemCount <= 0)
            {
                storedItem = null;
                slotImage.overrideSprite = defaultImage.sprite;
            }
        }
    }
}
