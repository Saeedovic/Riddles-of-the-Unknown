using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycler : MonoBehaviour
{
    public bool dayNightCycleActive = true;
    [SerializeField] float timeFactor = 1;

    public GameObject SunObj;
    public GameObject useTorchPopUp;

    bool torchTutorialDone = false;


    public AudioSource playerAudio;

    public AudioClip voiceOverItsGettingDark;

    public AudioClip voiceOverBeautifulSky;

    bool audio1HasPLayed = false;
    bool audio2HasPLayed = false;




    void Update()
    {
        transform.Rotate(0.01f * timeFactor, 0, 0);

        if(SunObj.transform.localRotation.eulerAngles.x > 170)
        {
            if (!audio1HasPLayed)
            {
            playerAudio.PlayOneShot(voiceOverBeautifulSky);
                audio1HasPLayed = true;

                StartCoroutine(TutorialManager.DisplaySubs("Wow..., being on this island is wonderful!, the sky looks beautiful not like in the city.", 4.5f));

            }

            Debug.Log("Its night Time");
           
            StartCoroutine(UseTorch());
        }
    }

    IEnumerator UseTorch()
    {
        
        yield return new WaitForSeconds(5);
        //Add Voice that says mmm Its Quite Dark, Good thing i Have a Torch
        if (!audio2HasPLayed)
        {
            playerAudio.PlayOneShot(voiceOverItsGettingDark);
            audio2HasPLayed = true;
            StartCoroutine(TutorialManager.DisplaySubs("It's getting dark!, Exploring will be very challenging!", 3.5f));


        }

        yield return new WaitForSeconds(4);

        if(torchTutorialDone == false) 
        {

            useTorchPopUp.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                useTorchPopUp.SetActive(false);

                torchTutorialDone = true;

            }
        }
    }

}
