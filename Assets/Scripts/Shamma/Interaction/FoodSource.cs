using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSource : PointOfInterest, IInteractableObject
{
    public bool isEatable = true;
    [SerializeField] int xpGiven = 5;

    static HungerSystem userHunger;
    public static bool foodQuestActive;

    [SerializeField] ReplenishingObject foodScriptableObj;
    static ReplenishingObject foodItem;
    public int amountToAddToInventory = 1;


    public void Interact(PlayerInteractor user)
    {
        if (userHunger == null)
            userHunger = user.GetComponent<HungerSystem>();

        foodItem = foodScriptableObj;
        /*if (!userHunger.ate)
        {
            if (userHunger.questManager != null &&
                userHunger.questManager.quests[userHunger.questManager.currentQuestIndex] == "Find some food")
            {
                ProcessInteraction(user);
                userHunger.questManager.CompleteCurrentQuest();
            }
        }
        else
        {
            ProcessInteraction(user);
        }*/
        ProcessInteraction(user);
    }


    void ProcessInteraction(PlayerInteractor user)
    {
        if (user.inventoryHandler.AddToInventory(foodItem, amountToAddToInventory))
        {
            userHunger.RefillHunger();
            AudioSource.PlayClipAtPoint(userHunger.AudioForEating, transform.position);
            userHunger.ate = true;

            user.xP.AddXp(xpGiven);
            gameObject.SetActive(false);
        }   
    }

    public bool IsInteractable()
    {
        return isEatable;
    }

    public void OnHighlight() { }

    public void OnDeHighlight() { }

    public bool riverCheck()
    {
        return true;
    }


}
