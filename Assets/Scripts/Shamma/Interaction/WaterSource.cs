using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : PointOfInterest, IInteractableObject
{
    public bool isDrinkable = true;
    [SerializeField] int xpGiven = 5;

    // the water that would be added to the inventory
    [SerializeField] ReplenishingObject waterScriptableObj;
    static ReplenishingObject waterItem;
    public int amountToAddToInventory = 1;
    static ThirstSystem userThirst;

    public void Interact(PlayerInteractor user)
    {
        if (userThirst == null)
            userThirst = user.GetComponent<ThirstSystem>();

        waterItem = waterScriptableObj;


        if (userThirst.questManager != null &&
            userThirst.questManager.quests[userThirst.questManager.currentQuestIndex] == "Drink water")
        {
            ProcessInteraction(user);
            userThirst.questManager.CompleteCurrentQuest();
        }
        else
        {
            ProcessInteraction(user);
        }
    }

    void ProcessInteraction(PlayerInteractor user)
    {
        
        // if water is successfully added to inventory, despawn and award xp
        if (user.inventoryHandler.AddToInventory(waterItem, amountToAddToInventory))
        {
            userThirst.RefillThirst();
            AudioSource.PlayClipAtPoint(userThirst.AudioForDrinking, transform.position); // could move this to the water inventory item?
            userThirst.drankwater = true;

            user.xP.AddXp(xpGiven);
            gameObject.SetActive(false);
            Debug.Log("water collected!");
        }
        
    }

    public bool IsInteractable()
    {
        return isDrinkable;
    }

    public void OnHighlight(){}

    public void OnDeHighlight(){}


}
