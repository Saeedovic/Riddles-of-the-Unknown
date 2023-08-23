using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchTrigger : MonoBehaviour
{
    public GameObject SunObj;

    public GameObject useTorchPopUp;
    public AudioSource playerAudio;
    public AudioClip voiceOverItsGettingDark;
    public AudioClip voiceOverMuchBetter;

    public GameObject torchObjRef;
    public FlashlightController flashCont;

    bool audio1HasPLayed = false;
    bool audio2HasPLayed = false;

    void Start()
    {
        flashCont.GetComponent<FlashlightController>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && SunObj.transform.localRotation.eulerAngles.x > 170 && audio1HasPLayed == false)
        {
         flashCont.GetComponent<FlashlightController>().enabled = true;

         if (!audio1HasPLayed)
         {

             playerAudio.PlayOneShot(voiceOverItsGettingDark);
             StartCoroutine(TutorialManager.DisplaySubs("It's getting dark!, Exploring will be very challenging!", 3.5f));

                if(flashCont.flashLightIsOn == false)
                {
                    StartCoroutine(DayNightCycler.UseTorch());
                    audio1HasPLayed = true;
                }
         }

         Debug.Log("Its night Time");
        }
    }
}
