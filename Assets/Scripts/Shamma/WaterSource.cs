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
        if (userThirst == null)
            userThirst = user.GetComponent<ThirstSystem>();
            
        userThirst.RefillThirst();
        AudioSource.PlayClipAtPoint(userThirst.AudioForDrinking, transform.position);
        userThirst.drankwater = true;

        userThirst.questManager.CompleteCurrentQuest();

        user.xP.AddXp(xpGiven);
        Destroy(gameObject);
    }

    public bool IsInteractable()
    {
        return isDrinkable;
    }

    public void OnHighlight(){}

    public void OnDeHighlight(){}


}
