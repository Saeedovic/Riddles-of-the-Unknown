using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// deactivate nature sounds and activate caves sounds
public class UseTorch : MonoBehaviour
{
    FlashlightController flashCont;

    TorchTrigger TorchUI;

    public GameObject TorchPrefab;

    public AudioSource playerAudio;
    public AudioClip voiceOverGladIGotMyTorch;
    public AudioClip voiceOverIShouldTakeOutMyTorch;

    public AudioSource playerBackgroundAudio;
    public AudioClip caveAudioClip;

    public GameObject switchObj;
    public TutorialManager tManager;

    public GameObject TorchUIFixed;

   // public PlayerCon playerConRef;

    public bool ifTorchIsOn = false;
    public bool ifTorchIsOff = false;


    private void Start()
    {
        flashCont = TorchPrefab.GetComponent<FlashlightController>();
        TorchUI = GetComponent<TorchTrigger>();
        tManager.GetComponent<TutorialManager>();
      //  playerConRef.GetComponent<PlayerCon>(); 

        switchObj.SetActive(false);

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && flashCont.flashLightIsOn == false)
        {
            StartCoroutine(TorchIsOff());
            
            SetBackgroundNoiseSFX();
        }

        if (collider.gameObject.tag == "Player" && flashCont.flashLightIsOn == true)
        {
            TorchUIFixed.SetActive(false);
            StartCoroutine(TorchIsOn());

            SetBackgroundNoiseSFX();
        }

       // playerAudio.clip = playerConRef.AudioClipForGameEnvironment;
       // playerAudio.clip = caveAudioClip;
       // playerAudio.loop = true;
       // playerAudio.Play();
    }

    IEnumerator TorchIsOn()
    {
        playerAudio.clip = voiceOverGladIGotMyTorch;

        playerAudio.PlayOneShot(voiceOverGladIGotMyTorch);

        StartCoroutine(TutorialManager.DisplaySubs("It seems these caves are a bit dark glad I bought that torch with me.", 4.5f));
        
        switchObj.SetActive(true);

        // StartCoroutine(TutorialManager.DisplaySubs("There we go!, Much Better.", 1.5f));

        yield return new WaitForSecondsRealtime(2);
        gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(2.5f);
        tManager.subtitleObj.SetActive(false);


    }

    IEnumerator TorchIsOff()
    {
        //Add Voice that says Probably best to use my Torch right now..
        playerAudio.clip = voiceOverIShouldTakeOutMyTorch;

        playerAudio.PlayOneShot(voiceOverIShouldTakeOutMyTorch);

        StartCoroutine(TutorialManager.DisplaySubs("Uhh, It's quite dark in here probably need to use my torch.", 3.5f));
        
        switchObj.SetActive(true);

        yield return new WaitForSecondsRealtime(2);

        TorchUIFixed.SetActive(true);
        gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(1.5f);

        tManager.subtitleObj.SetActive(false);



    }


    void SetBackgroundNoiseSFX()
    {
        if (playerBackgroundAudio.clip != caveAudioClip)
        {
            playerBackgroundAudio.Stop();

            playerBackgroundAudio.clip = caveAudioClip;
            playerBackgroundAudio.loop = true;
            playerBackgroundAudio.Play();
        }
    }
}