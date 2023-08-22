using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] GameObject flashLight;
    public bool flashLightIsOn = false;

    DayNightCycler TorchUI;
    public GameObject sun;

    public AudioSource playerAudio;
    public AudioClip voiceOverMuchBetter;

    bool useTorchTut = false;




    void Start()
    {
        flashLight.SetActive(false);
        TorchUI = sun.GetComponent<DayNightCycler>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && flashLightIsOn == false)
        {
            //Only play the Audio File and Sub after Any dialogue that is currently being played
            
            if (useTorchTut == false)
            {
                playerAudio.clip = voiceOverMuchBetter;
                playerAudio.PlayOneShot(voiceOverMuchBetter);
                useTorchTut = true;
            }

            flashLight.SetActive(true);
            flashLightIsOn = true;
            TorchUI.useTorchPopUp.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F) && flashLightIsOn == true)
        {
            flashLight.SetActive(false);
            flashLightIsOn = false;
        }

        if (playerAudio.clip == voiceOverMuchBetter && playerAudio.isPlaying)
        {
            StartCoroutine(TutorialManager.DisplaySubs("There we go!, Much Better.", 1.5f));
        }
    }
}
