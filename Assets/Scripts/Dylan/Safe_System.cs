using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class Safe_System : MonoBehaviour
{
    [SerializeField] private Animator safeDoor;
    [SerializeField] private string doorOpen = "SafeDoorOpen";


    public Button NextNumDown_1st_Digit;
    [SerializeField] public Button NextNumDown_2nd_Digit;
    [SerializeField] public Button NextNumDown_3rd_Digit;

    [SerializeField] public Button NextNumUp_1st_Digit;
    [SerializeField] public Button NextNumUp_2nd_Digit;
    [SerializeField] public Button NextNumUp_3rd_Digit;

     public TextMeshProUGUI First_Digit_Text;
    [SerializeField] public TextMeshProUGUI Second_Digit_Text;
    [SerializeField] public TextMeshProUGUI Third_Digit_Text;

    [SerializeField] public GameObject UnlockedText;
    [SerializeField] public GameObject Safe_Panel;
    [SerializeField] public GameObject Safe_Guide_Text;


    public MeshCollider SafeObjMeshCollider;
    public GameObject Safe_Gameobject;
    public GameObject SafeCam;
    public GameObject DefaultCam;

    public Transform Safe_Dial;

    public GameObject flashLightRef;


    public int First_Digit_Amount;
    [SerializeField] public int Second_Digit_Amount;
    [SerializeField] public int Third_Digit_Amount;

    [SerializeField] public int Safe_Code = 234;

    private int minDigit = 0;
    private int maxDigit = 99;

    StarterAssets.FirstPersonController cont;

    public AudioSource playerAudio;
    public AudioClip AudioClipForSafeDial;
    public AudioClip voiceOverUnlockedSafe;
    public AudioClip AudioSafeOpening;
    public AudioClip voiceOverForgotCodeCheckNotes;

    bool dialogueSaid = false;

    TutorialManager tManager;
    public GameObject tutorialManager;


    bool firstDialogueWhenUnlockingSafe = false;


    private void Start()
    {
        //Safe_Code = Random.Range(100, 900);
        tManager = tutorialManager.GetComponent<TutorialManager>(); 
        First_Digit_Amount = Random.Range(minDigit, maxDigit);
        Second_Digit_Amount = Random.Range(minDigit, maxDigit);
        Third_Digit_Amount = Random.Range(minDigit, maxDigit);

        First_Digit_Text.text =  First_Digit_Amount.ToString();
        Second_Digit_Text.text = Second_Digit_Amount.ToString();
        Third_Digit_Text.text = Third_Digit_Amount.ToString();

        UnlockedText.SetActive(false);

        Safe_Gameobject.tag = "Safe";

    }


    private void Update()
    {
        if(SafeCam.activeInHierarchy == true)
        {
           if(tManager.popUpIndex == 20 && dialogueSaid == false)
           {
              //  playerAudio.clip = voiceOverForgotCodeCheckNotes;

               // playerAudio.loop = false;
               // playerAudio.volume = 1;
               // playerAudio.Play();
             // PlayerAudioCaller.Instance.PlayAudio(voiceOverForgotCodeCheckNotes, playerAudio);
                playerAudio.PlayOneShot(voiceOverForgotCodeCheckNotes);
              StartCoroutine(TutorialManager.DisplaySubs("If I am not mistaken the code was.... Uggh I don't remember, I better check my notes app.", 5.5f));
                dialogueSaid = true;

            }
           // DefaultCam.GetComponent<PlayerCameraController>().enabled = false;
            Safe_Guide_Text.SetActive(true);

        }
        if(SafeCam.activeInHierarchy == false)
        {
           // DefaultCam.GetComponent<PlayerCameraController>().enabled = true;
            Safe_Guide_Text.SetActive(false);
        }

        if (First_Digit_Amount > 99)
        {
            First_Digit_Text.text = "0";
            First_Digit_Amount = 0;
        }
        if(First_Digit_Amount < 0)
        {
            First_Digit_Text.text = "99";
            First_Digit_Amount = 99;
        }



        if (Second_Digit_Amount > 99)
        {   
            Second_Digit_Text.text = "0";
            Second_Digit_Amount = 0;
        }   
        if (Second_Digit_Amount < 0)
        {   
            Second_Digit_Text.text = "99";
            Second_Digit_Amount = 99;
        }



        if (Third_Digit_Amount > 99)
        {   
            Third_Digit_Text.text = "0";
            Third_Digit_Amount = 0;
        }   
        if (Third_Digit_Amount < 0)
        {   
            Third_Digit_Text.text = "99";
            Third_Digit_Amount = 99;
        }

        //--------------------Code Combination--------------------------//

        if(First_Digit_Amount == 2 && Second_Digit_Amount == 3 && Third_Digit_Amount == 4 && Safe_Gameobject.tag == "Safe")
        {
            UnlockedText.SetActive(true);
            Safe_Panel.SetActive(false);

            Safe_Gameobject.tag = "Untagged";

            StartCoroutine(UnlockedSafe());
        }
        //--------------------Code Combination--------------------------//

    }


    public void NextNumber1stD() 
    {
        RotateDial();

        playerAudio.clip = AudioClipForSafeDial;
        playerAudio.loop = false;
        playerAudio.Play();

        First_Digit_Amount += 1;
        First_Digit_Text.text = First_Digit_Amount.ToString();
    }

    public void NextNumber2stD()
    {

        RotateDial();

        playerAudio.clip = AudioClipForSafeDial;
        playerAudio.loop = false;
        playerAudio.Play();

        Second_Digit_Amount += 1;
        Second_Digit_Text.text = Second_Digit_Amount.ToString();
    }

    public void NextNumber3stD()
    {

        RotateDial();

        playerAudio.clip = AudioClipForSafeDial;
        playerAudio.loop = false;
        playerAudio.Play();

        Third_Digit_Amount += 1;
        Third_Digit_Text.text = Third_Digit_Amount.ToString();
    }






    public void PrevNumber1stD()
    {
        RotateDial();

        playerAudio.clip = AudioClipForSafeDial;
        playerAudio.loop = false;
        playerAudio.Play();

        First_Digit_Amount -= 1;
        First_Digit_Text.text = First_Digit_Amount.ToString();
    }
    public void PrevNumber2stD()
    {
        RotateDial();

        playerAudio.clip = AudioClipForSafeDial;
        playerAudio.loop = false;
        playerAudio.Play();

        Second_Digit_Amount -= 1;
        Second_Digit_Text.text = Second_Digit_Amount.ToString();
    }
    public void PrevNumber3stD()
    {
        RotateDial();

        playerAudio.clip = AudioClipForSafeDial;
        playerAudio.loop = false;
        playerAudio.Play();

        Third_Digit_Amount -= 1;
        Third_Digit_Text.text = Third_Digit_Amount.ToString();
    }

    public void RotateDial()
    {
        Safe_Dial.transform.Rotate(new Vector3(0, 0, 20));
    }

    

    IEnumerator UnlockedSafe()
    {
        safeDoor.Play(doorOpen, 0, 0.0f);

        playerAudio.clip = AudioSafeOpening;

        playerAudio.loop = false;
        playerAudio.Play();

        playerAudio.PlayOneShot(voiceOverUnlockedSafe);

        StartCoroutine(TutorialManager.DisplaySubs("Nice!!!!! the safe has been unlocked. Let me take a look at what's inside.", 4.5f));

        yield return new WaitForSeconds(2);

        flashLightRef.GetComponent<PlayerCon>().enabled = true;
        flashLightRef.GetComponent<FlashlightController>().enabled = true;
        SafeObjMeshCollider.convex = false;


        Cursor.lockState = CursorLockMode.Locked;

        PhoneManager.Instance.mouseShouldBeUseable = false;
        PhoneManager.Instance.phoneIsUseable = true;
        WatchManager.watchShouldBeUseable = true;


        Destroy(UnlockedText);

        SafeCam.SetActive(false);
        DefaultCam.SetActive(true);
        SafeInteractable.safeHasBeenCracked = true;
    }

}
