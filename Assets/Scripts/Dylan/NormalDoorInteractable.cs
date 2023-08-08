using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoorInteractable : PointOfInterest, IInteractableObject
{
    [SerializeField] bool isInteractable = true;

    [SerializeField] private Animator CabinDoor;
    [SerializeField] private string doorOpen = "CabinDoorOpen";

    public BoxCollider DoorBoxCollider;

    public AudioSource playerAudio;
    public AudioClip AudioClipForDoorOpening;


    public void Interact(PlayerInteractor user)
    {
        Debug.Log("hello! you just clicked me");

        SetScannabilityOff();
        //gameObject.SetActive(false);

        CabinDoor.Play(doorOpen, 0, 0.0f);
        DoorBoxCollider.enabled = false;
        isInteractable = false;

        playerAudio.clip = AudioClipForDoorOpening;

        playerAudio.loop = false;
        playerAudio.Play();



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
