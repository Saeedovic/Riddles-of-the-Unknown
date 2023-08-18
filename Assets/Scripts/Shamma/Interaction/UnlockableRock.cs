using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableRock : PointOfInterest, IInteractableObject
{
    [SerializeField] KeyObject keyType;
    [SerializeField] GameObject keyNeededTextbox;
    [SerializeField] float boxDisplayTime = 2f;
    bool noKeyfound = true;

  //  [SerializeField] private Animator RockExplode;
   // [SerializeField] private string RockExplode = "RockExplode";

  //  public SphereCollider RockCollider;

    public AudioSource playerAudio;
    public AudioClip AudioClipForRockExploding;

    public AudioClip NeedExplosiveVoiceOver;





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
                this.gameObject.name = "Explosive on Rock";
                // use the key, make sure key-holding status is set to false if we have no more keys 
                user.inventoryHandler.InventorySlots[i].DiscardItem();

                if (user.inventoryHandler.InventorySlots[i].storedItem == null)
                    user.inventoryHandler.UpdateKeyObjectValue(false);

                KeyItemInteractable.hasBeenCollected = false;

                // would want to play the door opening anim here.

              //  RockExplode.Play(RockExplode, 0, 0.0f);
             //   RockCollider.enabled = false;


              //  playerAudio.clip = AudioClipForRockExploding;

             //   playerAudio.loop = false;
               // playerAudio.Play();


                Debug.Log("Rock was Blown Up!");
                noKeyfound = false;
                gameObject.SetActive(false);

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

        playerAudio.clip = NeedExplosiveVoiceOver;

        playerAudio.loop = false;
        playerAudio.Play();

        StartCoroutine(TutorialManager.DisplaySubs("Damn.. I need to find something to blow up this rock...", 2.5f));
        yield return new WaitForSeconds(2.7f);
        StartCoroutine(TutorialManager.DisplaySubs("Maybe if I explore a bit more I could find something that might help me out with this.", 4.5f));



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