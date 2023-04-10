using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSource : PointOfInterest, IInteractableObject
{
    public bool isEatable = true;

    public void Interact(PlayerInteractor user)
    {
        user.GetComponent<HungerSystem>().RefillHunger();

    }

    public bool IsInteractable()
    {
        return isEatable;
    }

    public void OnHighlight() { }

    public void OnDeHighlight() { }


}
