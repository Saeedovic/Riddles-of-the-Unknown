using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable3 : MonoBehaviour, IInteractableObject
{
    [SerializeField] bool isInteractable = true;
    [SerializeField] int xpGiven;

    public void Interact(PlayerInteractor user)
    {
        Debug.Log("hello! you just clicked me");

        // spawn a cube and set this mesh off (just to visually show something happened)
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        //GameObject.CreatePrimitive(PrimitiveType.Cube);
        var test = GameObject.CreatePrimitive(PrimitiveType.Cube);
        test.transform.position = this.transform.position;
        mesh.enabled = false;

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

    public bool riverCheck()
    {
        return true;
    }
}
