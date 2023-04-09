using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : PointOfInterest, IInteractableObject
{
    public bool isDrinkable = true;

    public void Interact(PlayerInteractor user)
    {
        user.GetComponent<ThirstSystem>().RefillThirst();
        
    }

    public bool IsInteractable()
    {
        return isDrinkable;
    }

    public void OnHighlight(){}

    public void OnDeHighlight(){}


}
