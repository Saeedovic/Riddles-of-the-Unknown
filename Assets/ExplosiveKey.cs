using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveKey : PointOfInterest, IInteractableObject
{
    [SerializeField] bool isCollectable = true;
    [SerializeField] KeyObject keyObject;
    public static bool hasBeenCollected = false; // probably want to change this to an event call of some kind

    public AudioSource playerAudio;
    public AudioClip AudioClipForPickingExplosive;

    public void Interact(PlayerInteractor user)
    {
        if (isCollectable)
        {
            user.inventoryHandler.AddToInventory(keyObject, 1);

            if (keyObject.countsAsAnUnlockingKey)
            {
                user.inventoryHandler.UpdateKeyObjectValue(true);
            }

            hasBeenCollected = true;

            playerAudio.clip = AudioClipForPickingExplosive;

            playerAudio.loop = false;
            playerAudio.volume = 1;
            playerAudio.Play();

            gameObject.SetActive(false);




        }
        else
        {
            Debug.Log("you can't pick this up right now.");
        }
    }

    public bool IsInteractable()
    {
            StartCoroutine(TutorialManager.DisplaySubs("These sticks of dynamite might come in handy.Better save it for later.", 3.5f));
        return true;
    }


    public void OnDeHighlight() { }

    public void OnHighlight() { }

    public bool riverCheck()
    {
        return true;
    }
}
