using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : PointOfInterest, IInteractableObject
{
    public bool isDrinkable = true;
    [SerializeField] int xpGiven = 5;

    static ThirstSystem userThirst;

    public void Interact(PlayerInteractor user)
    {
        //Debug.Log("Interact Now");
        if (userThirst == null)
            userThirst = user.GetComponent<ThirstSystem>();


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
        userThirst.RefillThirst();
        AudioSource.PlayClipAtPoint(userThirst.AudioForDrinking, transform.position);
        userThirst.drankwater = true;

        user.xP.AddXp(xpGiven);
        gameObject.SetActive(false);
    }

    public bool IsInteractable()
    {
        return isDrinkable;
    }

    public void OnHighlight(){}

    public void OnDeHighlight(){}


}
