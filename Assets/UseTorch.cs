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


    private void Start()
    {
        flashCont = TorchPrefab.GetComponent<FlashlightController>();
        TorchUI = sun.GetComponent<DayNightCycler>();
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


        yield return new WaitForSeconds(2);


    }

    IEnumerator TorchIsOff()
    {
        //Add Voice that says Probably best to use my Torch right now..
        playerAudio.clip = voiceOverIShouldTakeOutMyTorch;

        playerAudio.loop = false;
        playerAudio.Play();


        yield return new WaitForSeconds(2);

        TorchUI.useTorchPopUp.SetActive(true);


    }
}