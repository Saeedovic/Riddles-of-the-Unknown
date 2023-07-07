using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;

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

    DisplayStats stats;




    [SerializeField] public  GameObject playerObjRef;
    [SerializeField] GameObject PhoneAppRef;
    [SerializeField] GameObject WaypointSystemRef;
    [SerializeField] GameObject WatchObjRef;
    [SerializeField] GameObject safeObjRef;
    [SerializeField] GameObject phoneObjRef;
    [SerializeField] GameObject inventoryObjRef;
    




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



    //---- Live Playtest ----- //






    public GameObject cameraApp;
    public GameObject inventoryApp;
    public GameObject statAllocationApp;

    public bool ActivateWayPoint;

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


        app.enteredFullscreen = false;

        collectableCounterObj.SetActive(false);

        hungTextPanel.SetActive(false);
        thristyTextPanel.SetActive(false);
        tiredTextPanel.SetActive(false);

        FoodScan.SetActive(false);
        WaterScan.SetActive(false);
        EcoScan.SetActive(false);

    }

    private void Start()
    {
        mainCam.SetActive(true);
        tutCam.SetActive(false);

        playerHunger = playerObjRef.GetComponent<HungerSystem>();

        
    }


    // Update is called once per frame
    void Update()
    {
        collectableCountText.text = collectableCount.ToString("0");


        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
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

        if (popUpIndex == 7) //Take a Picture - 3
        {

            interactor.enabled = false;
            mainCam.SetActive(false);
            tutCam.SetActive(true);

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
            if (cameraApp.activeInHierarchy == true && Input.GetKeyDown(KeyCode.P))
            {
                app.enteredFullscreen = true;
                popUpIndex++;
            }

        }

        if (popUpIndex == 9) //Take a Picture - 5
        {
            if (cameraApp.activeInHierarchy == true && app.ecopointScanned == true)
            {
                popUpIndex++;
            }
        }

        if (popUpIndex == 10) //Take a Picture - 6
        {
            if (cameraApp.activeInHierarchy == false)
            {
                popUpIndex++;
                collectableCounterObj.SetActive(true);

            }
        }



        // -------------------------------------------------------FOR LIVE PLAYTEST ------------------------------------------------------------------ //



        if (popUpIndex == 12) //IndexLocation = 5
        {
            

            if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
            {
                
                wpSystem.locationIndex++;

                ActivateWayPoint = false;

                app.ecopointScanned = false;


                //Text PopUp w Audio thats says - mmm,Im feeling Quite Hungry (Disappears after like 5 Seconds)

                source.PlayOneShot(hungryClip);

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
            interactionBox.SetActive(false);
            phoneObjRef.SetActive(false);

            mainCam.SetActive(false);
                tutCam.SetActive(true);
                phoneObjRef.SetActive(true);

            if (collectableCount == 2)
            {
                interactionBox.SetActive(false);
                FoodScan.SetActive(false);


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
        } 



        if(popUpIndex == 14)
        {
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
            

            if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
            {
                wpSystem.locationIndex++;

                app.ecopointScanned = false;

                ActivateWayPoint = false;

                source.PlayOneShot(thirstyClip);


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


        



        if (popUpIndex == 17) 
        {


                EcoScan.SetActive(false);

            if (Vector3.Distance(wpSystem.wayPoint[wpSystem.locationIndex].transform.position, playerObjRef.transform.position) <= 10)
            {
                wpSystem.locationIndex++;

                app.ecopointScanned = false;

                ActivateWayPoint = false;





                //Check if Stamina is <= 25 if it is then follow line below

                //Text PopUp w Audio thats says - Ah man, I wish I had better Stamina to Run longer (Disappears after like 5 Seconds)



                //Trigger Stat App Part of the Tutorial

                //when player upgrades using statpoint and clicks go back button

                //popUpIndex++


            }
            if (app.ecopointScanned == false)
            {
               // EcoScan.SetActive(true);

            }

            if (cameraApp.activeInHierarchy == true && app.ecopointScanned == true)
            {
                Debug.Log("EcoPoint Scanned");
                EcoScan.SetActive(false);
            }

            if (stats.currentStamina == 50)
            {
                //Play Audio of Feeling Tired

                source.PlayOneShot(tiredClip);

            }

        }

        if(popUpIndex == 18)
        {
            
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
            interactionBox.SetActive(false);

            if (xpManager.playerIncreasedHungerStat == true || xpManager.playerIncreasedStaminaStat == true || xpManager.playerIncreasedThristStat == true)
            {
                popUpIndex++;
            }
        }
        if (popUpIndex == 20)
        {
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

        if (popUpIndex == 23) //IndexLocation = 5
        {
            ActivateWayPoint = false;
            qManager.CompleteCurrentQuest();
        }

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


    public void HungryText()
    {
        hungTextPanel.SetActive(false);
        //FoodScan.SetActive(true);
    }

    public void ThristyText()
    {
        thristyTextPanel.SetActive(false);
        WaterScan.SetActive(true);

    }

    public void TiredText()
    {
       tiredTextPanel.SetActive(false);
        EcoScan.SetActive(true);
    }


}
