using OD.Effect.HDRP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : PointOfInterest, IInteractableObject
{
    [SerializeField] KeyObject keyType;
    [SerializeField] GameObject keyNeededTextbox;
    [SerializeField] float boxDisplayTime = 2f;
    bool noKeyfound = true;

    [SerializeField] private Animator CabinDoor;
    [SerializeField] private string doorOpen = "CabinDoorOpen";

    public BoxCollider DoorBoxCollider;

    public AudioSource playerAudio;
    public AudioClip AudioClipForDoorOpening;
    public AudioClip NeedKeyVoiceOver;

    OnScanHighlight onScanHighlight;



    public void Interact(PlayerInteractor user)
    {
        if (!InventoryHandler.containsKeyObject)
        {
            StartCoroutine(DisplayKeyNeededTextbox());
            return;
        }

        for (int i = 0; i < 6; i++)
        {
            // check for which object is the key
            if (user.inventoryHandler.InventorySlots[i].storedItem == keyType)
            {
                // use the key, make sure key-holding status is set to false if we have no more keys 
                user.inventoryHandler.InventorySlots[i].DiscardItem();

                if (user.inventoryHandler.InventorySlots[i].storedItem == null)
                    user.inventoryHandler.UpdateKeyObjectValue(false);

                KeyItemInteractable.hasBeenCollected = false;

                // would want to play the door opening anim here.

                CabinDoor.Play(doorOpen, 0, 0.0f);
                DoorBoxCollider.enabled = false;

               // onScanHighlight.highlightMaterial = null;


                playerAudio.clip = AudioClipForDoorOpening;

                playerAudio.loop = false;
                playerAudio.Play();

                SetScannabilityOff();
                Debug.Log("door was opened!");
                noKeyfound = false;

                break;
            }
        }

        if (noKeyfound)
        {
            StartCoroutine(DisplayKeyNeededTextbox());
        }
    }

    IEnumerator DisplayKeyNeededTextbox()
    {
        keyNeededTextbox.SetActive(true);

        playerAudio.clip = NeedKeyVoiceOver;

        playerAudio.loop = false;
        playerAudio.Play();

        StartCoroutine(TutorialManager.DisplaySubs("I need to find the key to unlock this door...It must be here somewhere!", 4f));


        yield return new WaitForSeconds(boxDisplayTime);

        keyNeededTextbox.SetActive(false);
    }

    public bool IsInteractable() { return true; }

    public void OnDeHighlight() { }

    public void OnHighlight() { }

    public bool riverCheck()
    {
        return true;
    }

}
