using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinKey : PointOfInterest, IInteractableObject
{
    [SerializeField] bool isCollectable = true;
    [SerializeField] KeyObject keyObject;
    public static bool hasBeenCollected = false; // probably want to change this to an event call of some kind

    public AudioSource playerAudio;
    public AudioClip AudioClipForPickingUpKey;

    public void Interact(PlayerInteractor user)
    {
        if (isCollectable)
        {
            user.inventoryHandler.AddToInventory(keyObject, 1);

            if (keyObject.countsAsAnUnlockingKey)
            {
                user.inventoryHandler.UpdateKeyObjectValue(true);
            }
            if(hasBeenCollected == false)
            {
            StartCoroutine(PickUpKey());
            }

            Debug.Log("you've picked up a key");
        }
        else
        {
            Debug.Log("you can't pick this up right now.");
        }
    }

    public bool IsInteractable()
    {
        return true;
    }


    public void OnDeHighlight() { }

    public void OnHighlight() { }

    public bool riverCheck()
    {
        return true;
    }

    IEnumerator PickUpKey()
    {
        PlayerAudioCaller.Instance.PlayAudio(AudioClipForPickingUpKey, playerAudio);

        hasBeenCollected = true;
        gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(3.5f);

    }
}
