using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;


public class InventoryHandler : MonoBehaviour
{
    // in our case we specifically want six slots
    // can set them in inspector for now.
    [SerializeField] InventorySlot[] inventorySlots = new InventorySlot[6];
    public static bool containsKeyObject { get; private set; } 

    public List<InventorySlot> InventorySlots
    {
        get
        {
            List<InventorySlot> invSlots = new List<InventorySlot>();

            foreach (InventorySlot slot in inventorySlots)
            {
                invSlots.Add(slot);
            } 
            return invSlots; 
        } 
    } // for the other scripts to read and not write to inventory.
    // nooo how do we convert list to array?? 

    WayPointSystem wpSystem;
    TutorialManager tutorialManager;
    QuestManager qManager;


    [SerializeField] GameObject waypointSystemObj;

    [SerializeField] GameObject tutManagerObj;
    [SerializeField] GameObject questManagerObj;

    public WaterBottle waterBottle { get; private set; }
    [SerializeField] Button bottleButton;
    [SerializeField] Slider bottleSlider;

    void Start()
    {
        waterBottle = new WaterBottle(bottleButton, bottleSlider);

        tutorialManager = tutManagerObj.GetComponent<TutorialManager>();
        wpSystem = waypointSystemObj.GetComponent<WayPointSystem>();
        qManager = questManagerObj.GetComponent<QuestManager>();
    }


    public bool AddToInventory(InventoryObject itemToAdd, int numOf)
    {
    
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].storedItem == null)
            {
                inventorySlots[i].AddItem(itemToAdd, numOf);

                if (tutorialManager != null)
                {
                    tutorialManager.popUpIndex++;
                    tutorialManager.collectableCount++;

                    tutorialManager.ActivateWayPoint = true;
                }

                return true;
            }

            if (inventorySlots[i].storedItem == itemToAdd)
            {
                inventorySlots[i].IncrementItem(numOf);
                return true;
            }
        }

        // if no slots could be allocated, send error message 
        Debug.LogError("no slots left!");
        return false;
    }

    
    public void UpdateKeyObjectValue(bool containsKeyObject)
    {
        if (containsKeyObject)
            InventoryHandler.containsKeyObject = true;
        else
            InventoryHandler.containsKeyObject = false;
    }

}
