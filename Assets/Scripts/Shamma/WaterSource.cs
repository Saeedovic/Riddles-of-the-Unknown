using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : PointOfInterest, IInteractableObject
{
    public bool isDrinkable = true;
    [SerializeField] int xpGiven = 5;

    public void Interact(PlayerInteractor user)
    {
        user.GetComponent<ThirstSystem>().RefillThirst();
        user.xP.AddXp(xpGiven);
    }

    public bool IsInteractable()
    {
        return isDrinkable;
    }

    public void OnHighlight(){}

    public void OnDeHighlight(){}


}
