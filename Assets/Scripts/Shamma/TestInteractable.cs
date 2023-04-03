using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractableObject
{
    [SerializeField] bool isInteractable = true;

    public void Interact(GameObject user)
    {
        Debug.Log("hello! you just clicked me");

        // spawn a cube and set this mesh off (just to visually show something happened)
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;

        GameObject.CreatePrimitive(PrimitiveType.Cube);

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
