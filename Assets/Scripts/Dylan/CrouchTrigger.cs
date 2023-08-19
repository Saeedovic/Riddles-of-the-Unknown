using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchTrigger : MonoBehaviour
{

    public GameObject CrouchUIPopUp;

    public AudioSource playerAudio;
    public AudioClip AudioClipForCrouching;

    private void Start()
    {
    
        CrouchUIPopUp.SetActive(false);

    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            CrouchUIPopUp.SetActive(true);


            playerAudio.clip = AudioClipForCrouching;

            playerAudio.loop = false;
            playerAudio.volume = 1;
            playerAudio.Play();

            StartCoroutine(TutorialManager.DisplaySubs("I'll need to crouch here to be able to pass through.", 2.5f));
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            CrouchUIPopUp.SetActive(false);
        }
    }
}
