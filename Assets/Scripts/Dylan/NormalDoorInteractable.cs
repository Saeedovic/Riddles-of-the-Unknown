using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoorInteractable : PointOfInterest, IInteractableObject
{
    [SerializeField] bool isInteractable = true;

    public void Interact(PlayerInteractor user)
    {
        Debug.Log("hello! you just clicked me");

        SetScannabilityOff();
        gameObject.SetActive(false);
        isInteractable = false;

    }

    public bool IsInteractable()
    {
        if (isInteractable)
            return true;
        else
            return false;
    }

    public void OnDeHighlight()
    {
        Debug.Log("hi, I'm now unhighlighted");
    }

    public void OnHighlight()
    {
        Debug.Log("hi, I'm highlighted");
    }

    public bool riverCheck()
    {
        return true;
    }
}
