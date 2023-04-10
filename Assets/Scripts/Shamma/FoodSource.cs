using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSource : PointOfInterest, IInteractableObject
{
    public bool isEatable = true;
    [SerializeField] int xpGiven = 5;

    public void Interact(PlayerInteractor user)
    {
        user.GetComponent<HungerSystem>().RefillHunger();
        user.xP.AddXp(xpGiven);
    }

    public bool IsInteractable()
    {
        return isEatable;
    }

    public void OnHighlight() { }

    public void OnDeHighlight() { }


}
