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
    PlayerCon playerCont;
    PlayerInteractor interactor;
    Safe_System safe;
    [SerializeField]  InventoryHandler inventoryHandler;

    DisplayStats stats;




    [SerializeField] public  GameObject playerObjRef;
    [SerializeField] GameObject PhoneAppRef;
    [SerializeField] GameObject WaypointSystemRef;
    [SerializeField] GameObject WatchObjRef;
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

    public AudioSource source;

    public AudioClip tiredClip;
    public AudioClip thirstyClip;
    public AudioClip hungryClip;


    public GameObject phoneControlsGUIText;
    public GameObject controlToAccessEcoPointText;
    public GameObject fullScreenCam;
    public GameObject onScreenInstructionUI;


    PhoneManager pManager;


    public GameObject EcoPointScanText;
    public GameObject SafeCabinTriggerPoint;

    public GameObject NeedSafeNoteKeyCodeNote;
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

    public GameObject tutCam;
    public GameObject mainCam;

    HungerSystem playerHunger;




    private void Awake()
    {
        qManager = playerObjRef.GetComponent<QuestManager>();
        xpManager = playerObjRef.GetComponent<XPManager>();
        stats = playerObjRef.GetComponent<DisplayStats>();
        app = PhoneAppRef.GetComponent<PhoneCameraApp>();
        wpSystem = WaypointSystemRef.GetComponent<WayPointSystem>();
        watchManager = WatchObjRef.GetComponent<WatchManager>();
        playerCont = playerObjRef.GetComponent<PlayerCon>();
        interactor = playerObjRef.GetComponent<PlayerInteractor>();
        safe = safeObjRef.GetComponent<Safe_System>();
        inventoryHandler = GetComponent<InventoryHandler>();


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
        mainCam.SetActive(true);
        tutCam.SetActive(false);

        playerHunger = playerObjRef.GetComponent<HungerSystem>();
        onScreenInstructionUI.SetActive(false);

        ThirdPersonCam.enabled = true;

        originalPopUpIndex = 1;

    }


    // Update is called once per frame
    void Update()
    {
        collectableCountText.text = collectableCount.ToString("0");


        if(phoneObjRef.activeInHierarchy == true || playerWatch.activeInHierarchy == true)
        {
            ThirdPersonCam.enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.U) && ThirdPersonCam.enabled == false)
        {
            ThirdPersonCam.enabled = true;

        }else if(Input.GetKeyDown(KeyCode.U) && ThirdPersonCam.enabled == true)
        {
            ThirdPersonCam.enabled = false;
        }



        if (phoneObjRef.activeInHierarchy == true && cameraApp.activeInHierarchy == false && statAllocationApp.activeInHierarchy == false && inventoryApp.activeInHierarchy == false)
        {
            phoneControlsGUIText.SetActive(true);
        }
        else
        {
            phoneControlsGUIText.SetActive(false);
        }
        if(cameraApp.activeInHierarchy == true && fullScreenCam.activeInHierarchy != true)
        {
            phoneControlsGUIText.SetActive(false);
            controlToAccessEcoPointText.SetActive(true);
        }
        if (fullScreenCam.activeInHierarchy == true || cameraApp.activeInHierarchy != true || statAllocationApp.activeInHierarchy == true || inventoryApp.activeInHierarchy == true)
        {
            controlToAccessEcoPointText.SetActive(false);
        }

        //Stat Point Check

        if (xpManager.level == 2 && statTutorial == false && popUpIndex == 16)
        {
            //Save  Current PopUpIndex (Before Stat Tutorial Begins)
            
           
            popUpIndex = 48;
        }

        //Stat Point Check


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
            if (Input.GetKey(KeyCode.LeftShift))
            {
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
            tutCam.SetActive(true);

            if (Input.GetKeyDown(KeyCode.J))
            {
                mainCam.SetActive(true);
                tutCam.SetActive(false);

                Time.timeScale = 1;

                interactor.enabled = true;
                popUps[5].SetActive(false);
                watchManager.SetWatchState(true);

                popUpIndex++;

            }
        }

        if (popUpIndex == 4) //Investigate the village
        {
            if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
            {
                popUps[6].SetActive(true);

                wpSystem.locationIndex++;

                ActivateWayPoint = false;
                qManager.CompleteCurrentQuest();  //Investigate the Village

                popUpIndex = 6;

            }
        }


        if (popUpIndex == 6) //Take a Picture - 2
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
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
            tutCam.SetActive(true);


            if(Input.GetKeyDown(KeyCode.K)) 
            {
                phoneObjRef.SetActive(true);
            }

            if (cameraApp.activeInHierarchy == true)  //Add condition to check if player is in radius of Village
            {
                mainCam.SetActive(true);
                tutCam.SetActive(false);
                interactor.enabled = true;

                popUpIndex++;

            }
        }

        if (popUpIndex == 8) //Take a Picture - 4   
        {

            camAppBackButton.interactable = false;

            if (cameraApp.activeInHierarchy == true && Input.GetKeyDown(KeyCode.P))
            {
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

            //if Statement to check if they close phone, they shouldnt be allwoed to
            


            //Make GoBack Button Uninteractable


        }

        if (popUpIndex == 10) //Take a Picture - 6
        {
            if (cameraApp.activeInHierarchy == false)
            {

                popUpIndex++;
                collectableCounterObj.SetActive(true);

            }
        }

        if(popUpIndex == 11)
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


        if (popUpIndex == 13)
        {

            inventoryAppButton.interactable = true;

            onScreenInstructionUI.SetActive(false);

            interactionBox.SetActive(false);
            phoneObjRef.SetActive(false);

            interactor.enabled = false;
            mainCam.SetActive(false);
                tutCam.SetActive(true);
                phoneObjRef.SetActive(true);

            if (collectableCount == 2)
            {
                interactionBox.SetActive(false);
                FoodScan.SetActive(false);

                InventoryHandler.Tutorial = true;

                if (phoneObjRef.activeInHierarchy == true)
                {
                    popUps[13].SetActive(true);
         
                }
                if (inventoryApp.activeInHierarchy == true )
                {
                    

                    popUps[13].SetActive(false);
                    popUps[14].SetActive(true);

                    popUpIndex++;

                }
        
            }

            if(cameraApp.activeInHierarchy == true )
            {
                cameraApp.SetActive(false);
                inventoryApp.SetActive(true);
            }

        } 



        if(popUpIndex == 14)
        {
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

        if(popUpIndex == 15)
        {
            inventoryAppBackButton.interactable = true;

            interactionBox.SetActive(false);

            popUps[14].SetActive(false);


            //Once Player clicks on Go Back they will be put back into the game loop
            if (inventoryApp.activeInHierarchy == false)
            {
                mainCam.SetActive(true);
                tutCam.SetActive(false);

                popUps[15].SetActive(false);

                phoneObjRef.SetActive(false);

                ActivateWayPoint = true;

                popUpIndex++;

            }
        }

        if(popUpIndex == 16)
        {
            onScreenInstructionUI.SetActive(true);
            camAppButton.interactable = true;
            interactor.enabled = true;
            popUpCollider16.enabled = false;


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
                WaterBottleIntro.SetActive(true);

                WaterIntro = true;
                
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
            if(NoteContainer.uiToDisplayNote.gameObject.activeInHierarchy == false && noteTutorial == false)
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

            if (KeyCodeNote.activeInHierarchy == false) //IF the Selected Note has been interacted with then...
            {
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
                if (CabinDoorObj.activeInHierarchy == false)
                {
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

                    if (KeyItemInteractable.hasBeenCollected)
                    {
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

            if (CaveDoorObj.activeInHierarchy == false)
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

                if (KeyItemInteractable.hasBeenCollected)
                {
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

        if (popUpIndex == 44)
        {

            interactor.enabled = false;
            phoneObjRef.SetActive(false);
            mainCam.SetActive(false);
            tutCam.SetActive(true);


            phoneObjRef.SetActive(true);

            if(notesApp.activeInHierarchy == true)
            {
                popUpIndex = 45;
            }

        }

        if(popUpIndex == 45)
        {
            onScreenInstructionUI.SetActive(false);
            noteAppBackButton.interactable = false;

            if(NoteContainer.uiToDisplayNote.gameObject.activeInHierarchy == true) 
            {
                popUpIndex = 46;
            }
        }

        if (popUpIndex == 46)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                popUpIndex = 47;
            }
        }
        if (popUpIndex == 47)
        {
            noteAppBackButton.interactable = true;

            if (notesApp.activeInHierarchy == false)
            {

                mainCam.SetActive(true);
                tutCam.SetActive(false);
                interactor.enabled = true;

                noteTutorial = true;

                onScreenInstructionUI.SetActive(true);
                popUpIndex = 19;
            }
        }




        if (popUpIndex == 48)
        {
            interactor.enabled = false;


            statAppButton.interactable = true;
            camAppButton.interactable = true;

            onScreenInstructionUI.SetActive(false);

            phoneObjRef.SetActive(false);

            mainCam.SetActive(false);
            tutCam.SetActive(true);
            phoneObjRef.SetActive(true);

            if (statAllocationApp.activeInHierarchy == true)
            {

                popUpIndex++;
            }

        }

        if(popUpIndex == 49)
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
            if (statAllocationApp.activeInHierarchy == false)
            {
                mainCam.SetActive(true);
                tutCam.SetActive(false);

                phoneObjRef.SetActive(false);

                statTutorial = true;
                interactor.enabled = true;

                popUpIndex = 19;  //Set PopUpIndex To Old Index (Before Stat Tutorial Began)
             


            }
        }



        //Notes












        //For Later

        /*

        if (popUpIndex == 18)
        {
            statAppButton.interactable = true;
            camAppButton.interactable = true;
            inventoryAppButton.interactable = true;


            onScreenInstructionUI.SetActive(false);

            inventoryAppButton.interactable = false;

            interactionBox.SetActive(false);

            phoneObjRef.SetActive(false);

            mainCam.SetActive(false);
            tutCam.SetActive(true);
            phoneObjRef.SetActive(true);

            if (collectableCount == 4) //when they interact with collectable
                {

                interactionBox.SetActive(false);


                if(statAllocationApp.activeInHierarchy == true)
                {
                    interactionBox.SetActive(false);

                    popUpIndex++;
                }

            }



        }
        if (popUpIndex == 19)
        {
            statAppBackButton.interactable = false;
            interactionBox.SetActive(false);

            if (xpManager.playerIncreasedHungerStat == true || xpManager.playerIncreasedStaminaStat == true || xpManager.playerIncreasedThristStat == true)
            {
                popUpIndex++;
            }
        }
        if (popUpIndex == 20)
        {
            statAppBackButton.interactable = true;


            interactionBox.SetActive(false);

            if (statAllocationApp.activeInHierarchy == false)
            {


                mainCam.SetActive(true);
                tutCam.SetActive(false);

                phoneObjRef.SetActive(false);

                app.ecopointScanned = true;

                popUpIndex++;


            }
        }

        if (popUpIndex == 21 || popUpIndex == 22) //IndexLocation = 1
        {
            inventoryAppButton.interactable = true;
            onScreenInstructionUI.SetActive(true);

            if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
            {
                wpSystem.locationIndex++;

                ActivateWayPoint = false;

                app.ecopointScanned = false;

            }

            if (app.ecopointScanned == false)
            {
                EcoScan.SetActive(true);

            }

            if (cameraApp.activeInHierarchy == true && app.ecopointScanned == true)
            {
                Debug.Log("EcoPoint Scanned");
                EcoScan.SetActive(false);
            }
        }
        */




        // -------------------------------------------------------FOR LIVE PLAYTEST ------------------------------------------------------------------ //





        // -------------------------------------------------------FOR NORMAL GAME ------------------------------------------------------------------ //

        /*
        if (popUpIndex == 11) //Upgrade Stat
        {
            if (safe.SafeCam.activeInHierarchy == true)
            {

                popUpIndex++;
                qManager.CompleteCurrentQuest();  //Crack the Safe

            }
        }
        if (popUpIndex == 12) //Upgrade Stat - 2
        {
            if (safe.UnlockedText.activeInHierarchy == true)
            {

                popUpIndex++;
                qManager.CompleteCurrentQuest();  //Grab Key From Safe

            }
        }

        if (popUpIndex == 13) //Take Key From Safe
        {
                if (KeyItemInteractable.hasBeenCollected)  // Change to If Interacted with key
                {
                    popUpIndex++;
                    qManager.CompleteCurrentQuest();  //Grab Key From Safe
                }
        }

        if (popUpIndex == 14) //Upgrade Stat - 3
        {

            if (Input.GetKeyDown(KeyCode.Return))  // Change to If Interacted with key
            {
                popUpIndex++;


            }


        }
        if (popUpIndex == 15) //Upgrade Stat - 3
        {
            if (cameraApp.activeInHierarchy == true)  // If Player Interacted with Food or drank Water
            {


                popUps[6].SetActive(false);
                popUpIndex++;


            }

        }

        if (popUpIndex == 16) //Upgrade Stat - 3
        {

            popUps[8].SetActive(true);


            if (cameraApp.activeInHierarchy == true && Input.GetKeyDown(KeyCode.P))
            {
                app.enteredFullscreen = true;

                popUps[8].SetActive(false);
                popUpIndex++;
            }

        }

        if (popUpIndex == 17) //Upgrade Stat - 3
        {

            popUps[9].SetActive(true);

            if (cameraApp.activeInHierarchy == true && app.ecopointScanned == true)
            {

                popUpIndex++;
            }
        }

        if (popUpIndex == 18) //Interact with Food & Water
        {

            if (playerHunger.ate) //if player interacts with food
            {


                Time.timeScale = 0;



                mainCamera.SetActive(false);
                tutorialCamera.SetActive(true);
                watchManager.SetWatchState(false);
                interactor.enabled = false;

                popUpIndex++;


            }

        }

        if (popUpIndex == 19) 
        {
            popUps[5].SetActive(true);

            if (Input.GetKeyDown(KeyCode.J))
            {

                mainCamera.SetActive(true);
                tutorialCamera.SetActive(false);

                Time.timeScale = 1;

                interactor.enabled = true;
                popUps[5].SetActive(false);
                watchManager.SetWatchState(true);

                popUpIndex++;


            }

        }

        if (popUpIndex == 20)
        {
            popUps[20].SetActive(true);

            mainCamera.SetActive(false);
            tutorialCamera.SetActive(true);

            phoneObjRef.SetActive(true);

            if (inventoryApp.activeInHierarchy == true)
            {
                popUpIndex++;
            }
        }

        if (popUpIndex == 21)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                popUpIndex++;

            }
        }
        if(popUpIndex == 22)
        {
            if (inventoryApp.activeInHierarchy == false)
            {
                mainCamera.SetActive(true);
                tutorialCamera.SetActive(false);

                popUpIndex++;
            }
        }

        if(popUpIndex == 23)
        {
            wpSystem.locationIndex = 1;
            TutorialSectionCompleted = true;

            if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
           {
                Debug.Log("Reached Lake");
                TutorialSectionCompleted = false;
                qManager.CompleteCurrentQuest();

                popUpIndex++;


            }

        }

        */

        // -------------------------------------------------------FOR NORMAL GAME ------------------------------------------------------------------ //




        /*  if (popUpIndex == 15) //Upgrade Stat - 2
          {
              if (xpManager.playerIncreasedHungerStat == true || xpManager.playerIncreasedStaminaStat == true || xpManager.playerIncreasedThristStat == true)
              {
                  popUpIndex++;
              }
          }

          */




        /*  if (popUpIndex == 13) //Upgrade Stat - 3
          {
              if (statAllocationApp.activeInHierarchy == false)
              {          
                  popUpIndex++;
                  qManager.CompleteCurrentQuest();

              }
          }
          if (popUpIndex == 14) //Waypoints
          {
              TutorialSectionCompleted = true;

              if (Vector3.Distance(tutorialWayPointLocation.transform.position, playerObjRef.transform.position) <= 10)
              {
                  TutorialSectionCompleted = false;
                  popUpIndex++;

              }
          }
          if (popUpIndex == 15) //Waypoints - 2
          {
              if (Input.GetKey(KeyCode.E))
              {
                  popUpIndex++;
                  qManager.CompleteCurrentQuest();

              }
          }
          if (popUpIndex == 16) //Use EcoPoint to Find a Clue
          {
              if (cameraApp.activeInHierarchy == true)
              {
                  popUpIndex++;
              }
          }
          if (popUpIndex == 17) //Use EcoPoint to Find a Clue - 2
          {
              if (cameraApp.activeInHierarchy == true && Input.GetKey(KeyCode.P))
              {
                  popUpIndex++;  

              }
          }
          if (popUpIndex == 18) //Use EcoPoint to Find a Clue - 3
          {
              if (Input.GetKey(KeyCode.R))
              {
                  popUpIndex++;
                  qManager.CompleteCurrentQuest();

              }
          }
          if (popUpIndex == 19) //Use EcoPoint to Find a Clue - 4
          {

          }
          if (popUpIndex == 20) //Use EcoPoint to Find a Clue - 5
          {

          }
          if (popUpIndex == 21) //End oF Tutorial
          {


              if (Input.GetKey(KeyCode.Return))
              {
                  popUpIndex++;
              }
          }
        */
    }

}
