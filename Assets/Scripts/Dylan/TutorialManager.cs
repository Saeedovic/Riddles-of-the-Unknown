using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.VirtualTexturing;

using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    [SerializeField] public int popUpIndex;

    QuestManager qManager;

    XPManager xpManager;
    PhoneCameraApp app;
    WayPointSystem wpSystem;
    WatchManager watchManager;
    PhoneManager phoneManager;
    PlayerCon playerCont;
    PlayerInteractor interactor;
    Safe_System safe;
    [SerializeField] InventoryHandler inventoryHandler;

    DisplayStats stats;




    [SerializeField] public GameObject playerObjRef;
    [SerializeField] GameObject PhoneAppRef;
    [SerializeField] GameObject WaypointSystemRef;
    [SerializeField] GameObject WatchObjRef;
    [SerializeField] GameObject PhoneManagerObjRef;

    [SerializeField] GameObject safeObjRef;
    [SerializeField] public GameObject phoneObjRef;
    [SerializeField] GameObject inventoryObjRef;

    [SerializeField] GameObject WaterBottleIntro;




    public Button camAppButton;
    public Button statAppButton;
    public Button inventoryAppButton;
    public Button noteAppButton;
    public Button settingAppButton;

    public Button camAppBackButton;
    public Button camAppFullScreenBackButton;

    public Button statAppBackButton;
    public Button inventoryAppBackButton;
    public Button noteAppBackButton;
    public Button settingAppBackButton;




    //---- Live Playtest ----- //

    public TextMeshProUGUI collectableCountText;
    public float collectableCount;

    public GameObject collectableCounterObj;



    public GameObject hungTextPanel;
    public GameObject thristyTextPanel;
    public GameObject tiredTextPanel;

    public GameObject FoodScan;
    public GameObject WaterScan;
    public GameObject EcoScan;

    public GameObject confirmBox;

    public GameObject interactionBox;

   // public AudioSource source;

    public AudioClip tiredClip;
    public AudioClip thirstyClip;
    public AudioClip hungryClip;



    public GameObject phoneControlsGUIText;
    public GameObject controlToAccessEcoPointText;
    public GameObject fullScreenCam;
    public GameObject onScreenInstructionUI;


   // PhoneManager pManager;


    public GameObject EcoPointScanText;
    public GameObject SafeCabinTriggerPoint;

    public GameObject NeedSafeNoteKeyCodeNote;
    SafeNoteInteractable safeNoteObjRef;
    KeyCodeNoteInteractable keyCodeNoteObjRef;

    public GameObject KeyCodeNote;

    public GameObject CabinDoorObj;
    public GameObject CaveDoorObj;

    public GameObject Cave2WallBlockObj;



    public bool WaterIntro = false;


    public Camera ThirdPersonCam;
    public GameObject playerWatch;

    public BoxCollider popUpCollider11;
    public BoxCollider popUpCollider12;
    public BoxCollider popUpCollider16;
    public BoxCollider popUpCollider18;

    public GameObject village1_Block_1;
    public GameObject village1_Block_2;








    //---- Live Playtest ----- //






    public GameObject cameraApp;
    public GameObject inventoryApp;
    public GameObject statAllocationApp;

    public GameObject notesApp;

    public bool ActivateWayPoint;

    bool noteTutorial = false;

    public bool statTutorial = false;

    public int originalPopUpIndex;

    //  public GameObject tutorialWayPointLocation;

    public GameObject tutCamWatch;
    public GameObject tutCamPhone;


    public GameObject mainCam;

    HungerSystem playerHunger;



    UnlockableDoor unlockableCabinDoor;
    UnlockableDoor unlockableCaveDoor;



    public GameObject CabinWKeyDoor;
    public GameObject CaveWKeyDoor;




    public AudioClip voiceOverForReachingVillage1;
    //  public AudioClip voiceOverForLeavingVillage1;
    // public AudioClip voiceOverForInventoryApp;
    public AudioClip voiceOverForFollowingPathToReachVillage;
    public AudioClip voiceOverAfterDrinkingWater;
    public AudioClip voiceOverIveGotABadFeeling;
    public AudioClip voiceOverAfterEatingFood;
    public AudioClip voiceOverNeedToFindKey;
    public AudioClip voiceOverSeeVillage2;

    public AudioClip voiceOverUghMyDamnPhone;

    public AudioClip voiceOverFoundKeyCode;
    public AudioClip voiceOverLetsSeeWhatsInside;


    public AudioSource playerAudio;
    public AudioSource phoneAudio;


    public AudioClip phone_Ringing;

    public AudioClip voiceOverSmartWatch;

    public AudioClip voiceOverFirstCutScene_1;
    public AudioClip voiceOverFirstCutScene_2;
    public AudioClip voiceOver1AfterFirstCutScene;
    public AudioClip voiceOver2AfterFirstCutScene;





    bool audio1HasPLayed = false;
    bool audio2HasPLayed = false;
    bool audio3HasPLayed = false;
    bool audio4HasPLayed = false;
    bool FoundKeyCodeNote = false;

    bool audioforFood = false;

    bool voiceOverFirstCutScene_1_Has_Played = false;

    bool FirstHalf = false;
    bool SecondHalf = false;

    public GameObject disablePhoneObjRef;




    [SerializeField] private Animator FirstCutScene;
    [SerializeField] private string FirstCutSceneContainer = "Intro_Cutscene";

    [SerializeField] private Animator Right_Bus_Door;
    [SerializeField] private string RightBusDoorContainer = "Right_Bus_Door_Closing";

    [SerializeField] private Animator Left_Bus_Door;
    [SerializeField] private string LeftBusDoorContainer = "Left_Bus_Door_Closing";

    public Camera FirstCutSceneCamera;

    public GameObject Quest_Canvas;


    public static GameObject staticSubtitleObj;
    public static TextMeshProUGUI staticSubTextBox;
    public bool subActive = true;
    public static TextMeshProUGUI staticSubButtonText;

    public GameObject subtitleObj;
    public TextMeshProUGUI subTextBox;
    public TextMeshProUGUI subButtonText;

    public PlayerCameraController playerCameraController;

    bool phoneIsOff = false;

    [SerializeField] bool CutSceneEnabled = false;

    public GameObject pauseMenuPanel;

    private void Awake()
    {
        staticSubtitleObj = subtitleObj;
        staticSubTextBox = subTextBox;
        staticSubButtonText = subButtonText;

        qManager = playerObjRef.GetComponent<QuestManager>();
        xpManager = playerObjRef.GetComponent<XPManager>();
        stats = playerObjRef.GetComponent<DisplayStats>();
        app = PhoneAppRef.GetComponent<PhoneCameraApp>();
        wpSystem = WaypointSystemRef.GetComponent<WayPointSystem>();
        watchManager = WatchObjRef.GetComponent<WatchManager>();
        phoneManager = PhoneManagerObjRef.GetComponent<PhoneManager>();
        playerCont = playerObjRef.GetComponent<PlayerCon>();
        interactor = playerObjRef.GetComponent<PlayerInteractor>();
        safe = safeObjRef.GetComponent<Safe_System>();
        inventoryHandler = GetComponent<InventoryHandler>();

        unlockableCabinDoor = CabinWKeyDoor.GetComponent<UnlockableDoor>();
        unlockableCaveDoor = CaveWKeyDoor.GetComponent<UnlockableDoor>();
        safeNoteObjRef = NeedSafeNoteKeyCodeNote.GetComponent<SafeNoteInteractable>();
        keyCodeNoteObjRef = KeyCodeNote.GetComponent<KeyCodeNoteInteractable>();

        //playerCameraController.GetComponent<PlayerCameraController>().enabled = false;
        pauseMenuPanel.SetActive(false);

        collectableCounterObj.SetActive(false);

        hungTextPanel.SetActive(false);
        thristyTextPanel.SetActive(false);
        tiredTextPanel.SetActive(false);

        FoodScan.SetActive(false);
        WaterScan.SetActive(false);
        EcoScan.SetActive(false);
        EcoPointScanText.SetActive(false);
        WaterBottleIntro.SetActive(false);

    }

    private void Start()
    {

        if (CutSceneEnabled == true)
        {
            FirstCutSceneCamera.enabled = true;     // NEEDS TO BE TRUE
            StartCoroutine(FirstCutsceneAnimation());    //NEEDS TO BE UN COMMENTED

        }

        if (CutSceneEnabled == false)
        {
            FirstCutSceneCamera.enabled = false ;
           // ExplosiveKey.hasBeenCollected = true;
        }

        mainCam.SetActive(true);
        tutCamWatch.SetActive(false);
        tutCamPhone.SetActive(false);

        playerHunger = playerObjRef.GetComponent<HungerSystem>();
        onScreenInstructionUI.SetActive(false);

        ThirdPersonCam.enabled = false;

        phoneManager.phoneIsUseable = false;

    }


    // Update is called once per frame
    void Update()
    {

        if (FirstCutSceneCamera.enabled == false)
        {
            Debug.Log(phoneAudio.clip);
            Debug.Log(playerAudio.clip);

            
            collectableCountText.text = collectableCount.ToString("0");


            if (PhoneManager.phoneIsOut == true || WatchManager.watchIsOut == true)   //Need to Check If Phone OR watch Is out (This needs to be redone..Third Person will work then.)
            {
                ThirdPersonCam.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.Tab) && ThirdPersonCam.enabled == false)
            {
                ThirdPersonCam.enabled = true;

            }
            else if (Input.GetKeyDown(KeyCode.Tab) && ThirdPersonCam.enabled == true)
            {
                ThirdPersonCam.enabled = false;
            }



            if (PhoneManager.phoneIsOut == false && cameraApp.activeInHierarchy == false && statAllocationApp.activeInHierarchy == false && inventoryApp.activeInHierarchy == false && notesApp.activeInHierarchy == false)  //iF PHONE IS ON
            {
                phoneControlsGUIText.SetActive(false);

                Debug.Log("phone text NOT active");


            }



            if (cameraApp.activeInHierarchy == true && fullScreenCam.activeInHierarchy != true)
            {
                phoneControlsGUIText.SetActive(false);
                controlToAccessEcoPointText.SetActive(true);
            }

            if (fullScreenCam.activeInHierarchy == true || cameraApp.activeInHierarchy != true || statAllocationApp.activeInHierarchy == true || inventoryApp.activeInHierarchy == true || notesApp.activeInHierarchy == true)
            {
                Debug.Log("phone text IS active");

                controlToAccessEcoPointText.SetActive(false);
                
                phoneControlsGUIText.SetActive(true);

                if(popUpIndex > 10)
                {
                    onScreenInstructionUI.SetActive(true);
                }
            }

            if (phoneObjRef.activeInHierarchy == true && cameraApp.activeInHierarchy == false && statAllocationApp.activeInHierarchy == false && inventoryApp.activeInHierarchy == false && notesApp.activeInHierarchy == false)
            {
                phoneControlsGUIText.SetActive(true);
            }
            else
            {
                phoneControlsGUIText.SetActive(false);

            }



            
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenuPanel.SetActive(true);
                Time.timeScale = 0;
            }

            if(pauseMenuPanel.activeInHierarchy == true && Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.None;

                PhoneManager.Instance.mouseShouldBeUseable = true;
            }else if(pauseMenuPanel.activeInHierarchy == false)
            {
                Cursor.lockState = CursorLockMode.Locked;

                PhoneManager.Instance.mouseShouldBeUseable = false;

            }


            for (int i = 0; i < popUps.Length; i++)
            {
                if (i == popUpIndex)
                {
                    popUps[i].SetActive(true);

                    originalPopUpIndex++;
                }
                else
                {
                    popUps[i].SetActive(false);

                }
            }



            if (popUpIndex == 0) // WASD MOVEMENT
            {
                phoneManager.phoneIsUseable = false;
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    popUpIndex++;
                    qManager.CompleteCurrentQuest(); //Go to Dave's Shack
                }
            }

            if (popUpIndex == 1)   //WayPoint
            {
                ActivateWayPoint = true;

                popUpIndex++;
            }
            if (popUpIndex == 2)
            {
                if (Input.GetKey(KeyCode.LeftShift))  // run
                {
                    // Walk and running
                    playerCont.enabled = false;


                    playerAudio.clip = voiceOverSmartWatch;

                    playerAudio.loop = false;
                    playerAudio.Play();

                    watchManager.SetWatchState(false);

                    popUpIndex++;  //watch popup activated
                }
            }

            if (popUpIndex == 3)   //Watch is Active
            {
                interactor.enabled = false;
                Time.timeScale = 0;

                popUps[5].SetActive(true);

                mainCam.SetActive(false);
                tutCamWatch.SetActive(true);

                if (WatchManager.watchIsOut == false)
                {
                    playerCont.enabled = true;

                    mainCam.SetActive(true);
                    tutCamWatch.SetActive(false);

                    Time.timeScale = 1;

                    interactor.enabled = true;
                    popUps[5].SetActive(false);
                   // watchManager.SetWatchState(true);

                    popUpIndex++;

                }
            }

            if (popUpIndex == 4) //Investigate the village
            {
                if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
                {
                    phoneManager.phoneIsUseable = true;

                  //  phoneAudio.clip = phone_Ringing;

                    phoneAudio.loop = true;
                    
                    //PlayerAudioCaller.Instance.PlayAudio(phone_Ringing, phoneAudio);
                    phoneAudio.Play();


                    // PlayerAudioCaller.Instance.PlayAudio(phone_Ringing, playerAudio);



                    popUps[6].SetActive(true);

                    wpSystem.locationIndex++;

                    ActivateWayPoint = false;
                    qManager.CompleteCurrentQuest();  //Investigate the Village

                    popUpIndex = 6;

                }
            }


            if (popUpIndex == 6) //Take a Picture - 2
            {


                if (!audio1HasPLayed)
                {
                     playerAudio.clip = voiceOverUghMyDamnPhone;
                     Debug.Log(playerAudio.clip);
                      Debug.Log(playerAudio.isPlaying);
                    playerAudio.Play();

                    StartCoroutine(DisplaySubs("Ugh!, My Damn Phone!", 1.5f));

                    audio1HasPLayed = true;

                }

                if (PhoneManager.Instance.CheckPhoneIsOut == true)  //
                {
                    //phoneAudio.Stop();
                    phoneAudio.enabled = false;

                    popUps[6].SetActive(false);

                    popUpIndex++;

                }

            }

            if (popUpIndex == 7) //Take a Picture - 3   //Make All Buttons Except Camera Button Uninteractable
            {

                noteAppButton.interactable = false;
                inventoryAppButton.interactable = false;
                statAppButton.interactable = false;
                settingAppButton.interactable = false;

                interactor.enabled = false;
                mainCam.SetActive(false);

                tutCamPhone.SetActive(true);


                phoneManager.firstHighlightedPhoneButton = camAppButton.gameObject;
               // phoneManager.SetPhoneState(false); // open Phone
                phoneManager.phoneIsUseable = false;



                if (PhoneMainMenu.appIsOpen == false)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(phoneManager.firstHighlightedPhoneButton);
                }


                /*
                if (Input.GetKeyDown(KeyCode.K))
                {
                    phoneObjRef.SetActive(true);
                    phoneManager.SetPhoneState(true); // close Phone

                }*/

                if (cameraApp.activeInHierarchy == true)  //Add condition to check if player is in radius of Village
                {
                    mainCam.SetActive(true);
                    tutCamPhone.SetActive(false);
                    interactor.enabled = true;

                    popUpIndex++;

                }
            }

            if (popUpIndex == 8) //Take a Picture - 4   
            {
                camAppBackButton.interactable = false;

                if (cameraApp.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Q))
                {
                  //  phoneManager.GetComponent<PhoneManager>().SetFullscreen(true); 
                    camAppFullScreenBackButton.interactable = false;
                    camAppBackButton.interactable = true;

                    popUpIndex++;
                }

                //if Statement to check if they close phone, they shouldnt be allwoed to


                //Make GoBack Button Uninteractable


            }

            if (popUpIndex == 9) //Take a Picture - 5
            {
                if (cameraApp.activeInHierarchy == true && app.ecopointScanned == true)
                {
                    camAppFullScreenBackButton.interactable = true;

                    popUpIndex++;
                }
            }

            if (popUpIndex == 10) //Take a Picture - 6
            {
                if (cameraApp.activeInHierarchy == false)
                {
                    phoneManager.phoneIsUseable = true;

                    popUpIndex++;
                    collectableCounterObj.SetActive(true);

                }
            }

            if (popUpIndex == 11)
            {
                //Add UI that says Look for Highlighted obj Around you

                popUps[43].SetActive(true);
                popUpCollider11.enabled = false;

            }



            // -------------------------------------------------------FOR LIVE PLAYTEST ------------------------------------------------------------------ //



            if (popUpIndex == 12) //IndexLocation = 5
            {
                popUps[43].SetActive(false);
                popUpCollider12.enabled = false;

                onScreenInstructionUI.SetActive(true);

                if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
                {

                    wpSystem.locationIndex++;

                    ActivateWayPoint = false;

                    app.ecopointScanned = false;


                    //Text PopUp w Audio thats says - mmm,Im feeling Quite Hungry (Disappears after like 5 Seconds)

                    //source.PlayOneShot(hungryClip);

                    //Text PopUp thats says - Do a Eco-Point Scan to look for food to consume (Deactivate this text after ecopoint is done)

                }

                if (app.ecopointScanned == false)
                {
                    Debug.Log("EcoPoint Scanned");
                    FoodScan.SetActive(true);
                }
                //player will look for item and consume it.

                //Once Consumed Phone will open And showcase how the Inventory App Works!


            }


            if (popUpIndex == 13)   //This Is the Start Of the inventory Tutorial
            {

                camAppButton.interactable = false;
                inventoryAppButton.interactable = true;

                phoneManager.firstHighlightedPhoneButton = inventoryAppButton.gameObject;
                phoneManager.phoneIsUseable = false;


                onScreenInstructionUI.SetActive(false);

                interactionBox.SetActive(false);
                //phoneObjRef.SetActive(false); //Close

                
                if(phoneIsOff == false)  //refreshing the phone 
                {
                    phoneManager.SetPhoneState(true); //close phone
                    phoneIsOff = true;
                }
                


                interactor.enabled = false;
                mainCam.SetActive(false);
                tutCamPhone.SetActive(true);
                //phoneObjRef.SetActive(true); //Open

                phoneManager.SetPhoneState(false); // open Phone

                if (PhoneMainMenu.appIsOpen == false)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(phoneManager.firstHighlightedPhoneButton);
                }



                if (collectableCount == 2)
                {
                   // playerAudio.clip = voiceOverAfterEatingFood;

                  //  playerAudio.loop = false;
                  //  playerAudio.Play();

                   

                    interactionBox.SetActive(false);
                    FoodScan.SetActive(false);

                    InventoryHandler.Tutorial = true;

                    if (phoneObjRef.activeInHierarchy == true)
                    {
                        popUps[13].SetActive(true);

                    }
                    if (inventoryApp.activeInHierarchy == true)
                    {


                        popUps[13].SetActive(false);
                        popUps[14].SetActive(true);

                        popUpIndex++;

                    }

                }
            }



            if (popUpIndex == 14)
            {
                if(audioforFood == false)
                {
                    PlayerAudioCaller.Instance.PlayAudio(voiceOverAfterEatingFood, playerAudio);
                }

                if (playerAudio.clip == voiceOverAfterEatingFood && playerAudio.isPlaying)
                {
                    StartCoroutine(DisplaySubs("I hope these foods don't get me sick.", 2.5f));
                    audioforFood = true;
                }

                inventoryAppBackButton.interactable = false;


                interactionBox.SetActive(false);

                if (confirmBox.activeInHierarchy == true)
                {
                    Debug.Log(popUpIndex);
                    popUps[14].SetActive(false);
                    popUps[15].SetActive(true);

                    popUpIndex++;

                }
            }

            if (popUpIndex == 15)
            {
                camAppButton.interactable = true;
                inventoryAppBackButton.interactable = true;

                phoneManager.firstHighlightedPhoneButton = camAppButton.gameObject;

                interactionBox.SetActive(false);

                popUps[14].SetActive(false);


                //Once Player clicks on Go Back they will be put back into the game loop
                if (inventoryApp.activeInHierarchy == false)
                {
                    mainCam.SetActive(true);
                    tutCamPhone.SetActive(false);

                    popUps[15].SetActive(false);

                    phoneManager.SetPhoneState(true); //Turn Phone Off

                    ActivateWayPoint = true;

                    popUpIndex++;

                }
            }

            if (popUpIndex == 16)  //This Is the End Of the inventory Tutorial
            {
                phoneManager.CheckPhoneIsOut = false;
                onScreenInstructionUI.SetActive(true);
                camAppButton.interactable = true;
                interactor.enabled = true;
                popUpCollider16.enabled = false;
                phoneManager.phoneIsUseable = true;

                if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
                {

                    InventoryHandler.Tutorial = false;
                    wpSystem.locationIndex++;

                    app.ecopointScanned = false;

                    ActivateWayPoint = false;

                    //source.PlayOneShot(thirstyClip);



                    //Text PopUp w Audio thats says - Jeez, Now I'm Feeling Quite Thristy (Disappears after like 5 Seconds)

                    //Text PopUp thats says - Do a Eco-Point Scan to look for Water to consume (Deactivate this text after ecopoint is done)

                    //player will look for item and consume it.


                }

                if (app.ecopointScanned == false)
                {
                    WaterScan.SetActive(true);

                }

                if (cameraApp.activeInHierarchy == true && app.ecopointScanned == true)
                {
                    Debug.Log("EcoPoint Scanned");
                    WaterScan.SetActive(false);
                }
            }





            if (popUpIndex == 17) //This is where we want the Game to Start From
            {

                qManager.CompleteCurrentQuest();
                collectableCounterObj.SetActive(false);
                onScreenInstructionUI.gameObject.transform.position -= new Vector3(173, 0, 0);
                popUpIndex++;

            }



            if (popUpIndex == 18)
            {
                WaterScan.SetActive(false);

                
                if (WaterIntro == false)
                {
                    
                        playerAudio.clip = voiceOverAfterDrinkingWater;

                        playerAudio.loop = false;
                        playerAudio.Play();

                       // PlayerAudioCaller.Instance.PlayAudio(voiceOverAfterDrinkingWater, playerAudio);

                    if (playerAudio.clip == voiceOverAfterDrinkingWater && playerAudio.isPlaying)
                    {
                        StartCoroutine(DisplaySubs("I feel much more Energetic after that!", 2.5f));
                        WaterBottleIntro.SetActive(true);

                        WaterIntro = true;
                    }
                }
                


                if (Input.GetKeyDown(KeyCode.Return))
                {
                    WaterBottleIntro.SetActive(false);
                    popUpCollider18.enabled = false;


                    InventoryHandler.Tutorial = true;
                }

                //check if player is in range <100 ..then deactivate waypoint
                if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 100)
                {
                    if (!audio2HasPLayed)
                    {
                       //  playerAudio.PlayOneShot(voiceOverForReachingVillage1);
                        PlayerAudioCaller.Instance.PlayAudio(voiceOverForReachingVillage1, playerAudio);
                    }

                    if (playerAudio.clip == voiceOverForReachingVillage1 && playerAudio.isPlaying)
                    {
                        StartCoroutine(DisplaySubs("Alright now. I am pretty sure Dave's other shack is here and I will need to look around.", 5.5f));
                        audio2HasPLayed = true;
                    }


                    ActivateWayPoint = false;

                    EcoPointScanText.SetActive(true);

                    if (Vector3.Distance(SafeCabinTriggerPoint.transform.position, playerObjRef.transform.position) < 1) //IF Player Is inside Cabin
                    {

                        qManager.currentQuestIndex = 4; //Goes to the Next Quest
                        qManager.UpdateQuestText();

                        EcoPointScanText.SetActive(false);


                    }
                    else if (Vector3.Distance(SafeCabinTriggerPoint.transform.position, playerObjRef.transform.position) > 5)
                    {
                        EcoPointScanText.SetActive(true);

                        qManager.currentQuestIndex = 3; //Goes to the Previous Quest
                        qManager.UpdateQuestText();

                    }

                    if (NeedSafeNoteKeyCodeNote.activeInHierarchy == false) //IF the Selected Note has been interacted with then...
                    {
                        EcoPointScanText.SetActive(false);

                      //  StartCoroutine(NeedToGoTo2ndVillage());

                        wpSystem.locationIndex++; //2nd Village
                        qManager.currentQuestIndex = 5; //Goes to the Next Quest
                        qManager.UpdateQuestText();

                        popUpIndex++;
                    }

                    Debug.Log(Vector3.Distance(SafeCabinTriggerPoint.transform.position, playerObjRef.transform.position));


                }
                //else if player range is >100 then Activate Waypoint!
                else if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) >= 100)
                {
                    ActivateWayPoint = true;

                    EcoPointScanText.SetActive(false);


                }
            }

            if (popUpIndex == 19)
            {
                if(safeNoteObjRef.publicNoteInfo.uiToDisplayNote.gameObject.activeInHierarchy == false && noteTutorial == false)
                {
                    noteAppButton.interactable = true;

                    popUpIndex = 44;
                }



                ActivateWayPoint = true;

                if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 100)
                {
                    ActivateWayPoint = false;
                    qManager.currentQuestIndex = 6; // This is Quest 6
                    qManager.UpdateQuestText();

                    if (!audio3HasPLayed)
                    {
                       //playerAudio.PlayOneShot(voiceOverSeeVillage2);
                        PlayerAudioCaller.Instance.PlayAudio(voiceOverSeeVillage2, playerAudio);
                    }

                    if (playerAudio.clip == voiceOverSeeVillage2 && playerAudio.isPlaying)
                    {
                        StartCoroutine(DisplaySubs("I see the other village. Lets head over there and investigate.", 3.5f));

                        audio3HasPLayed = true;

                    }

                    EcoPointScanText.SetActive(true);


                }
                //else if player range is >100 then Activate Waypoint!
                else if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) >= 100)
                {
                    ActivateWayPoint = true;

                    qManager.currentQuestIndex = 5; //Goes to the Next Quest
                    qManager.UpdateQuestText();

                    EcoPointScanText.SetActive(false);


                }

                if (keyCodeNoteObjRef.publicNoteInfo.uiToDisplayNote.gameObject.activeInHierarchy == true) //IF the Selected Note has been interacted with then...
                {
                    Debug.Log("Note Has Been Collected");

                    StartCoroutine(DisplaySubs("Yes!! Here are the codes to the safe now I can head back and crack that sucker open and see what's inside.", 7.5f));
                     
                    EcoPointScanText.SetActive(false);

                    wpSystem.locationIndex++; //Safe Location
                    qManager.currentQuestIndex = 7; // This is Quest 7
                    qManager.UpdateQuestText();

                    popUpIndex++;
                }

            }

            if (popUpIndex == 20)
            {
                ActivateWayPoint = true;
                

                if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
                {
                    ActivateWayPoint = false;


                    if (safeObjRef.tag == "Untagged") //IF the Selected Note has been interacted with then...
                    {
                        Debug.Log("Unlocked Safe");
                        qManager.currentQuestIndex = 8; // This is Quest 8
                        qManager.UpdateQuestText();

                        wpSystem.locationIndex = 6; //Key Location



                        if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 5)
                        {
                            ActivateWayPoint = false;

                        }
                        //else if player range is >100 then Activate Waypoint!
                        else if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) >= 10)
                        {
                            ActivateWayPoint = true;


                        }

                        if (KeyItemInteractable.hasBeenCollected)
                        {
                            village1_Block_2.SetActive(false);
                            popUpIndex = 21;
                        }



                    }


                }
                //else if player range is >100 then Activate Waypoint!
                else if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) >= 10)
                {
                    ActivateWayPoint = true;


                }


            }

            if (popUpIndex == 21)
            {

                qManager.currentQuestIndex = 9; // This is Quest 9
                qManager.UpdateQuestText();

                wpSystem.locationIndex++; //CabinOnRiverSide
                popUpIndex++;


            }

            if (popUpIndex == 22)
            {
                ActivateWayPoint = true;


                if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 100)
                {
                    ActivateWayPoint = false;

                    EcoPointScanText.SetActive(true);


                    //Check if Door is Opened
                    //If Door is Opened....Quest will Say Find Clues In Cabin
                    if (unlockableCabinDoor.DoorBoxCollider.enabled == false)
                    {
                        if (!audio4HasPLayed)
                        {

                            PlayerAudioCaller.Instance.PlayAudio(voiceOverLetsSeeWhatsInside, playerAudio);
                        }

                        if (playerAudio.clip == voiceOverLetsSeeWhatsInside && playerAudio.isPlaying)
                        {
                            StartCoroutine(DisplaySubs("Lets see what's inside here..", 1.5f));

                           audio4HasPLayed = true;
                        }

                        qManager.currentQuestIndex = 10; // This is Quest 9
                        qManager.UpdateQuestText();

                        wpSystem.locationIndex = 8; //Location - Cabin With Key


                        if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
                        {
                            ActivateWayPoint = false;


                        }
                        else if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) >= 10)
                        {
                            ActivateWayPoint = true;

                        }

                        if (CabinKey.hasBeenCollected)
                        {
                            StartCoroutine(DisplaySubs("Hmmmm.... Another key There has to be a use for this key somewhere. I better investigate..", 6f));

                            popUpIndex = 23;
                        }

                    }



                    //Ensure Player leaves the cabin with THE Second Key Collected

                    //Update the Waypoint to Cave 1  ...Quest will say find a use for key


                }
                //else if player range is >100 then Activate Waypoint!
                else if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) >= 100)
                {
                    ActivateWayPoint = true;

                    EcoPointScanText.SetActive(false);

                }
            }

            if (popUpIndex == 23)
            {
                EcoPointScanText.SetActive(false);

                qManager.currentQuestIndex = 11; // This is Quest 11
                qManager.UpdateQuestText();

                wpSystem.locationIndex = 9; //Cave
                popUpIndex++;


            }

            if (popUpIndex == 24)
            {
                ActivateWayPoint = true;

                if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 40)
                {
                    ActivateWayPoint = false;

                    qManager.currentQuestIndex = 12; // Quest will say Explore the Cave
                    qManager.UpdateQuestText();
                }
                //else if player range is >100 then Activate Waypoint!
                else if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) >= 40)
                {
                    ActivateWayPoint = true;

                    qManager.currentQuestIndex = 11; // This is Quest 11
                    qManager.UpdateQuestText();
                }

                if (unlockableCaveDoor.DoorBoxCollider.enabled == false)
                {
                    qManager.currentQuestIndex = 13; // This is Quest 9
                    qManager.UpdateQuestText();

                    wpSystem.locationIndex = 10; //Location - Cave Room


                    if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
                    {
                        ActivateWayPoint = false;


                    }
                    else if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) >= 10)
                    {
                        ActivateWayPoint = true;

                    }

                    if (ExplosiveKey.hasBeenCollected)
                    {
                        StartCoroutine(DisplaySubs("These sticks of dynamite might come in handy.Better save it for later.", 3.5f));

                        popUpIndex = 25;
                    }
                }
               

            }

            if (popUpIndex == 25)
            {

                qManager.currentQuestIndex = 14; // This is Quest 9
                qManager.UpdateQuestText();

                wpSystem.locationIndex = 11; //Cave2
                popUpIndex++;


            }

            if (popUpIndex == 26)
            {
                ActivateWayPoint = true;

                if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
                {
                    ActivateWayPoint = false;

                    qManager.currentQuestIndex = 15; // This is Quest 9
                    qManager.UpdateQuestText();

                }
                //else if player range is >100 then Activate Waypoint!
                else if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) >= 10)
                {
                    ActivateWayPoint = true;

                    qManager.currentQuestIndex = 14; // This is Quest 9
                    qManager.UpdateQuestText();
                }

                if (Cave2WallBlockObj.activeInHierarchy == false)
                {
                    qManager.currentQuestIndex = 16; // This is Quest 9
                    qManager.UpdateQuestText();

                    wpSystem.locationIndex = 12; //CaveExplore

                    if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 40)
                    {
                        ActivateWayPoint = false;
                    }

                }



            }

            if (popUpIndex == 27)
            {
                ActivateWayPoint = false;

            }




            //Notes

            if (popUpIndex == 44) //This is the Start of the Notes App Tutorial
            {
                phoneIsOff = false;
                interactor.enabled = false;

                mainCam.SetActive(false);
                tutCamPhone.SetActive(true);

                phoneManager.firstHighlightedPhoneButton = noteAppButton.gameObject;
                phoneManager.SetPhoneState(false);
                phoneManager.phoneIsUseable = false;

                if (PhoneMainMenu.appIsOpen == false)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(phoneManager.firstHighlightedPhoneButton);
                }



                if (notesApp.activeInHierarchy == true)
                {
                    popUpIndex = 45;
                }

            }

            if (popUpIndex == 45)
            {
                onScreenInstructionUI.SetActive(false);
                noteAppBackButton.interactable = false;

                if (NoteContainer.isInInteraction == true  || safeNoteObjRef.publicNoteInfo.uiToDisplayNote.enabled == false)
                {
                    popUpIndex = 46;
                }
            }

            if (popUpIndex == 46)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    popUpIndex = 47;
                }
            }
            if (popUpIndex == 47)
            {
                noteAppBackButton.interactable = true;

                if (notesApp.activeInHierarchy == false)   //This is the end of the Notes App Tutorial
                {
                    phoneManager.phoneIsUseable = true;

                    mainCam.SetActive(true);
                    tutCamPhone.SetActive(false);
                    interactor.enabled = true;

                    noteTutorial = true;

                    onScreenInstructionUI.SetActive(true);

                    village1_Block_1.SetActive(false);

                    phoneManager.SetPhoneState(true);
                    phoneManager.CheckPhoneIsOut = false;

                    popUpIndex = 19;
                }
            }




            if (popUpIndex == 48)  //This is the Start of the Stat App Tutorial
            {
                disablePhoneObjRef.SetActive(false);
                onScreenInstructionUI.SetActive(false);

                statAppButton.interactable = true;


                interactor.enabled = false;
                mainCam.SetActive(false);
                tutCamPhone.SetActive(true);
                
               
                phoneManager.phoneIsUseable = true;
                phoneManager.SetPhoneState(false);  //then set it to Active


                phoneManager.firstHighlightedPhoneButton = statAppButton.gameObject;
                phoneManager.phoneIsUseable = false;

                if (PhoneMainMenu.appIsOpen == false)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(phoneManager.firstHighlightedPhoneButton);
                }


                if (statAllocationApp.activeInHierarchy == true)
                {

                    popUpIndex++;
                }

            }

            if (popUpIndex == 49)
            {
                //statAppBackButton.interactable = false;

                if (xpManager.playerIncreasedHungerStat == true || xpManager.playerIncreasedStaminaStat == true || xpManager.playerIncreasedThristStat == true)
                {
                    statAppBackButton.interactable = true;
                    popUpIndex++;

                }
            }

            if (popUpIndex == 50)
            {
                playerAudio.clip = voiceOverForFollowingPathToReachVillage;

                if (statAllocationApp.activeInHierarchy == false)  //This is the end of the Stat App Tutorial
                {
                    phoneManager.phoneIsUseable = true;

                    mainCam.SetActive(true);
                    tutCamPhone.SetActive(false);

                    phoneManager.SetPhoneState(true); // Close Phone


                    statTutorial = true;
                    interactor.enabled = true;


                    //  playerAudio.PlayOneShot(voiceOverForFollowingPathToReachVillage);

                    playerAudio.PlayOneShot(voiceOverForFollowingPathToReachVillage);


                    if (playerAudio.clip == voiceOverForFollowingPathToReachVillage)
                    {
                    StartCoroutine(DisplaySubs("I better follow along this road path to reach the village.", 3.5f));

                    }


                    popUpIndex = 19;  //Set PopUpIndex To Old Index (Before Stat Tutorial Began)



                }
            }

        }


    }
        IEnumerator FirstCutsceneAnimation()
        {

           if (voiceOverFirstCutScene_1_Has_Played == false)
           {
               Quest_Canvas.SetActive(false);
               Right_Bus_Door.enabled = false;
               Left_Bus_Door.enabled = false;
               yield return new WaitForSeconds(0.2f);
              playerObjRef.GetComponent<PlayerCon>().enabled = false;
            playerCameraController.GetComponent<PlayerCameraController>().enabled = false;
            watchManager.GetComponent<WatchManager>().enabled = false;


            FirstCutScene.Play(FirstCutSceneContainer, 0, 0.0f);
             playerAudio.PlayOneShot(voiceOverFirstCutScene_1);
              // PlayerAudioCaller.Instance.PlayAudio(voiceOverFirstCutScene_1, playerAudio);
               subtitleObj.SetActive(true);

               subTextBox.text = "It Appears I lost consciousness on the bus, and Over-looked the bus stop in the village...";
               yield return new WaitForSeconds(5);
               //subTextBox.GetComponent<TextMeshPro>().text = "";

               voiceOverFirstCutScene_1_Has_Played = true;
           }
           
           if(voiceOverFirstCutScene_1_Has_Played == true)
           {
            playerAudio.PlayOneShot(voiceOverFirstCutScene_2);
            //PlayerAudioCaller.Instance.PlayAudio(voiceOverFirstCutScene_2, playerAudio);

            staticSubTextBox.text = "This journey was quite lengthy!";
             yield return new WaitForSeconds(2);
            staticSubTextBox.text = "Considering the Travel from the city to here spanned several days...";
             yield return new WaitForSeconds(4);
            staticSubTextBox.text = "As I had to reach this location by the means of a boat!";
             yield return new WaitForSeconds(3);
            staticSubtitleObj.SetActive(false);
            

            //yield return new WaitForSeconds(7);

            Right_Bus_Door.enabled = true;
            Left_Bus_Door.enabled = true;

            yield return new WaitForSeconds(3.5f);
            playerAudio.PlayOneShot(voiceOver1AfterFirstCutScene);
            //PlayerAudioCaller.Instance.PlayAudio(voiceOver1AfterFirstCutScene, playerAudio);

            staticSubtitleObj.SetActive(true);

            staticSubTextBox.text = "Ohh right!, I remember now...";
            yield return new WaitForSeconds(2.5f);
            playerAudio.PlayOneShot(voiceOver2AfterFirstCutScene);
            //PlayerAudioCaller.Instance.PlayAudio(voiceOver2AfterFirstCutScene, playerAudio);

            staticSubTextBox.text = "Someone gave me an anonymous tip in which Dave's Shack can hold the answer that I might need!";
            yield return new WaitForSeconds(7.5f);
            staticSubtitleObj.SetActive(false);
       
            Debug.Log("First Cut Scene Completed!");
            FirstCutSceneCamera.enabled = false;

            playerObjRef.GetComponent<PlayerCon>().enabled = true;
            watchManager.GetComponent<WatchManager>().enabled = true;

            playerCameraController.GetComponent<PlayerCameraController>().enabled = true;
            Quest_Canvas.SetActive(true);
           }
        }


    public void SwitchSubs()
    {
        if(subActive == false)
        {
            subtitleObj.SetActive(true);
            subActive = true;
            subButtonText.text = "Turn Subs OFF";
            return;
        }
        if (subActive == true)
        {
            subtitleObj.SetActive(false);
            subActive = false;
            subButtonText.text = "Turn Subs On";
            return;
        }
    }

    public static IEnumerator DisplaySubs(string message, float delay)
    {
        staticSubtitleObj.SetActive(true);  //Enables Subs Box
        staticSubTextBox.text = message;
        yield return new WaitForSeconds(delay);
        staticSubtitleObj.SetActive(false);

    }

}
