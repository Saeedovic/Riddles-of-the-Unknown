using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseTorch : MonoBehaviour
{
    FlashlightController flashCont;

    DayNightCycler TorchUI;

    public GameObject TorchPrefab;
    public GameObject sun;

    public AudioSource playerAudio;
    public AudioClip voiceOverGladIGotMyTorch;

    public AudioClip voiceOverIShouldTakeOutMyTorch;

    public GameObject switchObj;


    private void Start()
    {
        flashCont = TorchPrefab.GetComponent<FlashlightController>();
        TorchUI = sun.GetComponent<DayNightCycler>();

        switchObj.SetActive(false);

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && flashCont.flashLightIsOn == false)
        {
            StartCoroutine(TorchIsOff());
            
        }

        if (collider.gameObject.tag == "Player" && flashCont.flashLightIsOn == true)
        {
            TorchUI.useTorchPopUp.SetActive(false);
            StartCoroutine(TorchIsOn());

        }
    }

    IEnumerator TorchIsOn()
    {
        //Add Voice that says Glad I got my FlashLight Along!

        playerAudio.clip = voiceOverGladIGotMyTorch;

        playerAudio.loop = false;
        playerAudio.Play();

        StartCoroutine(TutorialManager.DisplaySubs("It seems these caves are a bit dark glad I bought that torch with me.", 4.5f));

        switchObj.SetActive(true);
        gameObject.SetActive(false);


        // StartCoroutine(TutorialManager.DisplaySubs("There we go!, Much Better.", 1.5f));

        yield return new WaitForSeconds(2);


    }

    IEnumerator TorchIsOff()
    {
        //Add Voice that says Probably best to use my Torch right now..
        playerAudio.clip = voiceOverIShouldTakeOutMyTorch;

        playerAudio.loop = false;
        playerAudio.Play();

        StartCoroutine(TutorialManager.DisplaySubs("Uhh, It's quite dark in here probably need to use my torch.", 3.5f));

        switchObj.SetActive(true);
        gameObject.SetActive(false);


        yield return new WaitForSeconds(2);

        TorchUI.useTorchPopUp.SetActive(true);


    }
}