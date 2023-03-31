using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractableObject
{
    public void Interact(GameObject user)
    {
        Debug.Log("hello! you just clicked me");
    }

    public bool IsInteractable()
    {
        return true;
    }

    public void OnDeHighlight()
    {
        Debug.Log("hi, I'm now unhighlighted");
    }

    public void OnHighlight()
    {
        Debug.Log("hi, I'm highlighted");
    }
}
