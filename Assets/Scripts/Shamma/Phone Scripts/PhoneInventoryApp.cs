using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PhoneInventoryApp : PhoneAppScreen
{
    [SerializeField] GameObject confirmBox; // object that slots use to execute the drop or use funcs
    [SerializeField] Button confUseButton;
    [SerializeField] Button confDropButton;
    [SerializeField] Button confBackButton; // buttons in the confirm box
    InventorySlot currentSlot; // temporarily saved item, for the confirmation box to reference

    public override void OnOpenApp()
    {
        if (confirmBox.activeInHierarchy)
        {
            confirmBox.SetActive(false);
        }
        base.OnOpenApp();
    }

    // set up confirmation box and pass over control
    public void OpenConfirmationBox(InventorySlot itemSlot)
    {
        currentSlot = itemSlot; // cache the item so we can use it in the following funcs

        confirmBox.SetActive(true);
        confUseButton.onClick.AddListener(UseItem);
        confDropButton.onClick.AddListener(DropItem);
        confBackButton.onClick.AddListener(CloseConfirmationBox);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(confUseButton.gameObject); // ensure navigation is set to the confirmation box
    }

    void UseItem()
    {
        currentSlot.UseItem();
        currentSlot = null; // make sure we get rid of reference after we use the item!

        CloseConfirmationBox();

        //Debug.Log("the item was used!");
    }

    void DropItem()
    {
        if (currentSlot.storedItem.IsDroppable())
        {
            currentSlot.DiscardItem();
            currentSlot = null;

            CloseConfirmationBox();
            //Debug.Log("the item was discarded!");
        }
        else
        {
            Debug.Log("this is a key item, you can't drop it!");
            // play a voice clip about needing to hold onto the item here
        }
    }

    // undo the confirmation box setup.
    void CloseConfirmationBox()
    {
        //Debug.Log("interaction finished");

        currentSlot = null;

        confUseButton.onClick.RemoveListener(UseItem);
        confDropButton.onClick.RemoveListener(DropItem);
        confBackButton.onClick.RemoveListener(CloseConfirmationBox);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstHighlightedButton);

        confirmBox.SetActive(false);
    }
}
