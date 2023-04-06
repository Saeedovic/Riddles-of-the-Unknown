using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable2 : MonoBehaviour, IInteractableObject
{
    [SerializeField] bool isInteractable = true;
    [SerializeField] int xpGiven;

    public void Interact(PlayerInteractor user)
    {
        Debug.Log("hello! you just clicked me");

        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        user.xP.AddXp(xpGiven);
        Debug.Log($"you got {xpGiven} XP!");

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
}
