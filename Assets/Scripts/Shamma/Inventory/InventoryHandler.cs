using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;


public class InventoryHandler : MonoBehaviour
{
    WayPointSystem wpSystem;
    TutorialManager tutorialManager;
    QuestManager qManager;


    [SerializeField] GameObject waypointSystemObj;

    [SerializeField] GameObject tutManagerObj;
    [SerializeField] GameObject questManagerObj;


    private void Start()
    {
       tutorialManager = tutManagerObj.GetComponent<TutorialManager>();
        wpSystem = waypointSystemObj.GetComponent<WayPointSystem>(); 
        qManager = questManagerObj.GetComponent<QuestManager>();

    }


    // in our case we specifically want six slots
    // can set them in inspector for now.
    [SerializeField] InventorySlot[] inventorySlots = new InventorySlot[6];
    public List<InventorySlot> InventorySlots
    {
        get
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                InventorySlots.Add(slot);
            } 
            return InventorySlots; 
        } 
    } // for the other scripts to read and not write to inventory.
    // nooo how do we convert list to array?? 


    public bool AddToInventory(InventoryObject itemToAdd, int numOf)
    {
    
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].storedItem == null)
            {
                inventorySlots[i].AddItem(itemToAdd, numOf);
                tutorialManager.popUpIndex++;
                tutorialManager.collectableCount++;

                tutorialManager.ActivateWayPoint = true;
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

    

}
