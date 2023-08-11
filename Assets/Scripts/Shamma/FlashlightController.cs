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




    void Start()
    {
        flashLight.SetActive(false);
        TorchUI = sun.GetComponent<DayNightCycler>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && flashLightIsOn == false)
        {
            playerAudio.clip = voiceOverMuchBetter;

            playerAudio.loop = false;
            playerAudio.Play();

            flashLight.SetActive(true);
            flashLightIsOn = true;
            TorchUI.useTorchPopUp.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F) && flashLightIsOn == true)
        {
            flashLight.SetActive(false);
            flashLightIsOn = false;
        }
    }
}
