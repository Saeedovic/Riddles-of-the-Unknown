using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractableObject
{
    [SerializeField] bool isInteractable = true;
    [SerializeField] int xpGiven;

    TutorialManager tutorialManager;

    [SerializeField] GameObject tManagerRef;

    private void Awake()
    {
        tutorialManager = tManagerRef.GetComponent<TutorialManager>();
    }

    public void Interact(PlayerInteractor user)
    {
        Debug.Log("hello! you just clicked me");

        // set this mesh off (just to visually show something happened)
        MeshRenderer mesh = GetComponent<MeshRenderer>();

        mesh.enabled = false;

        user.xP.AddXp(xpGiven);

        tutorialManager.popUpIndex++;
        tutorialManager.collectableCount++;

        tutorialManager.phoneObjRef.SetActive(false);

        tutorialManager.ActivateWayPoint = true;



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
