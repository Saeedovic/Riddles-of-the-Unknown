using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnlockableRock : PointOfInterest, IInteractableObject
{
    [SerializeField] KeyObject keyType;
    [SerializeField] GameObject keyNeededTextbox;
    [SerializeField] float boxDisplayTime = 2f;
    bool noKeyfound = true;


    [SerializeField] GameObject explosivesModel;
    [SerializeField] Transform explosivesPositioning;

    [SerializeField] GameObject explosionVFX;
    [SerializeField] Transform vfxPositioning;

    bool explosionSequenceActive = false;

    [SerializeField] float detontationTimer = 5f;
    [SerializeField] float lengthBetweenBeeps = 2f;
    float timeToNextBeep;


    public AudioSource playerAudio;
    public AudioClip audioClipForExplosiveBeep;
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
                // use the key, make sure key-holding status is set to false if we have no more keys 
                user.inventoryHandler.InventorySlots[i].DiscardItem();

                if (user.inventoryHandler.InventorySlots[i].storedItem == null)
                    user.inventoryHandler.UpdateKeyObjectValue(false);

                KeyItemInteractable.hasBeenCollected = false;

                // would want to play the door opening anim here.

                explosionSequenceActive = true;
                StartCoroutine(PlayExplosiveSequence());
              //  RockExplode.Play(RockExplode, 0, 0.0f);
             //   RockCollider.enabled = false;


              //  playerAudio.clip = AudioClipForRockExploding;

             //   playerAudio.loop = false;
               // playerAudio.Play();


                Debug.Log("Rock was Blown Up!");
                noKeyfound = false;
                

                break;
            }
        }

        if (noKeyfound)
        {
            StartCoroutine(DisplayKeyNeededTextbox());
        }
    }

    IEnumerator PlayExplosiveSequence()
    {
        // add the explosives model, then wait for the detonation timer to expire
        GameObject explosivesObj = GameObject.Instantiate(explosivesModel, 
                                                   explosivesPositioning.position, 
                                                   explosivesPositioning.rotation);
        explosivesObj.SetActive(true);

        yield return new WaitForSeconds(detontationTimer);

        explosionSequenceActive = false;
        // play explosive effect 

        GameObject.Instantiate(explosionVFX, vfxPositioning.position, vfxPositioning.rotation);
        explosionVFX.GetComponent<Exploder>().enabled = true;

        explosivesObj.SetActive(false);

        yield return null;

        playerAudio.clip = AudioClipForRockExploding;
        playerAudio.Play();

        gameObject.SetActive(false); // probably want to replace this with an animation of the rocks breaking apart
    }

    void Update()
    {
        if (explosionSequenceActive)
        {
            PlayExplosiveBeep();
        }
    }

    // should become continually faster to signify bomb is going off
    void PlayExplosiveBeep()
    {
        timeToNextBeep -= Time.deltaTime;

        if (timeToNextBeep <= 0)
        {
            playerAudio.clip = audioClipForExplosiveBeep;
            playerAudio.Play();

            timeToNextBeep = detontationTimer / lengthBetweenBeeps;
            lengthBetweenBeeps *= 2;
            Debug.Log("Beep");
        }
    }

    IEnumerator DisplayKeyNeededTextbox()
    {
        keyNeededTextbox.SetActive(true);

       // playerAudio.clip = NeedExplosiveVoiceOver;

        //PlayerAudioCaller.Instance.PlayAudio(NeedExplosiveVoiceOver, playerAudio);
        playerAudio.PlayOneShot(NeedExplosiveVoiceOver);

        StartCoroutine(TutorialManager.DisplaySubs("Damn I need to find something to blow up this rock maybe if I explore a bit more I could find something that might help me out with this.", 7.5f));
        gameObject.layer = 0;
        yield return new WaitForSeconds(7.5f);
        gameObject.layer = 3;


        keyNeededTextbox.SetActive(false);
    }

    public bool IsInteractable() { return !explosionSequenceActive; } // shouldn't be interactable in the middle of blowing up

    public void OnDeHighlight() { }

    public void OnHighlight() { }

    public bool riverCheck()
    {
        return true;
    }

}