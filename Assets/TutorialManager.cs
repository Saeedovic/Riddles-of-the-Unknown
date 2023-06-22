using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

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
    Safe_System safe;



    [SerializeField] GameObject playerObjRef;
    [SerializeField] GameObject PhoneAppRef;
    [SerializeField] GameObject WaypointSystemRef;
    [SerializeField] GameObject WatchObjRef;
    [SerializeField] GameObject safeObjRef;
    [SerializeField] GameObject phoneObjRef;




    public GameObject cameraApp;
    public GameObject inventoryApp;
    public GameObject statAllocationApp;

    public bool TutorialSectionCompleted;

    public GameObject tutorialWayPointLocation;

    public GameObject tutorialCamera;
    public GameObject mainCamera;






    private void Awake()
    {
        qManager = playerObjRef.GetComponent<QuestManager>();
        xpManager = playerObjRef.GetComponent<XPManager>();
        app = PhoneAppRef.GetComponent<PhoneCameraApp>();
        wpSystem = WaypointSystemRef.GetComponent<WayPointSystem>();
        watchManager = WatchObjRef.GetComponent<WatchManager>();
        playerCont = playerObjRef.GetComponent<PlayerCon>();
        safe = safeObjRef.GetComponent<Safe_System>();

    }

    private void Start()
    {
        mainCamera.SetActive(true);
        tutorialCamera.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

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
            if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))      
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest(); //Go to Dave's Shack
            }
        }

        if (popUpIndex == 1)   //WayPoint
        {
            TutorialSectionCompleted = true;

            popUpIndex++; 
        }
        if (popUpIndex == 2 )
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                watchManager.SetWatchState(false);

                popUpIndex++;  //watch popup activated
            }
        }

        if (popUpIndex == 3)   //Watch is Active
        {
          Time.timeScale = 0;

            popUps[5].SetActive(true);

            mainCamera.SetActive(false);
            tutorialCamera.SetActive(true);

            if (Input.GetKeyDown(KeyCode.J))
            {
                mainCamera.SetActive(true);
                tutorialCamera.SetActive(false);

                Time.timeScale = 1;

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
                
                TutorialSectionCompleted = false;
                qManager.CompleteCurrentQuest();  //Investigate the Village

                popUpIndex = 6;

            }
        }
        

        if (popUpIndex == 6) //Take a Picture - 2
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
               popUps[6].SetActive(false);

              popUpIndex++;  

            }
            
        }

        if (popUpIndex == 7) //Take a Picture - 3
        {
            

            mainCamera.SetActive(false);
            tutorialCamera.SetActive(true);

            if (cameraApp.activeInHierarchy == true)  //Add condition to check if player is in radius of Village
            {
                mainCamera.SetActive(true);
                tutorialCamera.SetActive(false);

                popUpIndex++;

            }
        }

        if (popUpIndex == 8) //Take a Picture - 4
        {
           if (cameraApp.activeInHierarchy == true && Input.GetKeyDown(KeyCode.P)) 
            {
                popUpIndex++;
            }
          
        }

        if (popUpIndex == 9) //Take a Picture - 5
        {
            if (cameraApp.activeInHierarchy == true && Input.GetKeyDown(KeyCode.R))
            {
                popUpIndex++;
            }
        }

        if (popUpIndex == 10) //Take a Picture - 6
        {
            if (cameraApp.activeInHierarchy == false)
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest();   //Find a Safe!

            }
        }
        if (popUpIndex == 11) //Upgrade Stat
        {
            if (playerCont.SafeCam.activeInHierarchy == true)
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
            if (Input.GetKeyDown(KeyCode.E))  // Change to If Interacted with key
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
                
                popUps[8].SetActive(false);
                popUpIndex++;
            }
            
        }

        if (popUpIndex == 17) //Upgrade Stat - 3
        {

            popUps[9].SetActive(true);

            if (cameraApp.activeInHierarchy == true && Input.GetKeyDown(KeyCode.R))
            {
                
                popUpIndex++;
            }
        }

        if (popUpIndex == 18) //Interact with Food & Water
        {

            if (Input.GetKeyDown(KeyCode.F)) //if player interacts with food
            {


                Time.timeScale = 0;

                

                mainCamera.SetActive(false);
                tutorialCamera.SetActive(true);
                watchManager.SetWatchState(false);


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


    public void ContinueButton()
    {
      popUpIndex++;
        qManager.CompleteCurrentQuest();
    }

}
