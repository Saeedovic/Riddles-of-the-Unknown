using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    [SerializeField] public int popUpIndex;

    QuestManager qManager;

    XPManager xpManager;
    PhoneCameraApp app;
    WayPointSystem wpSystem;
    WatchManager watchManager;

    [SerializeField] GameObject playerObjRef;
    [SerializeField] GameObject PhoneAppRef;
    [SerializeField] GameObject WaypointSystemRef;
    [SerializeField] GameObject WatchObjRef;


    public GameObject cameraApp;
    public GameObject notesApp;
    public GameObject statAllocationApp;

    public bool TutorialSectionCompleted;

    public GameObject tutorialWayPointLocation;



    private void Awake()
    {
        qManager = playerObjRef.GetComponent<QuestManager>();
        xpManager = playerObjRef.GetComponent<XPManager>();
        app = PhoneAppRef.GetComponent<PhoneCameraApp>();
        wpSystem = WaypointSystemRef.GetComponent<WayPointSystem>();
        watchManager = WatchObjRef.GetComponent<WatchManager>();
    }


    // Update is called once per frame
    void Update()
    {

       for (int i = 0; i < popUps.Length; i++)
        {
            if(i == popUpIndex)
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
                qManager.CompleteCurrentQuest();
            }
        }

        if (popUpIndex == 1)   //Vaulting
        {
            if (Input.GetKey(KeyCode.Space))
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest();
            }
        }

        if (popUpIndex == 2)   //Crouching
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest();
            }
        }

        if (popUpIndex == 3) //Running
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest();
            }
        }

        if (popUpIndex == 4) //Watch
        {
            watchManager.SetWatchState(false);

            if (Input.GetKeyDown(KeyCode.J))
            {
                watchManager.SetWatchState(true);

                popUpIndex++;
                qManager.CompleteCurrentQuest();
            }
        }

        if (popUpIndex == 5) //Take A Picture
        {
            if (Input.GetKey(KeyCode.K))
            {
                popUpIndex++;  
            }
        }

        if (popUpIndex == 6) //Take a Picture - 2
        {
            if (cameraApp.activeInHierarchy == true)
            {
                popUpIndex++;  
            }
        }

        if (popUpIndex == 7) //Take a Picture - 3
        {
            if (app.pictureTaken == true)
            {
               popUpIndex++;    
            }
        }

        if (popUpIndex == 8) //Take a Picture - 4
        {
           if (cameraApp.activeInHierarchy == false)  
            {
                popUpIndex++;
            }
          
        }

        if (popUpIndex == 9) //Take a Picture - 5
        {
            if (notesApp.activeInHierarchy == true)
            {
                popUpIndex++;
            }
        }

        if (popUpIndex == 10) //Take a Picture - 6
        {
            if (notesApp.activeInHierarchy == false)
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest();

            }
        }
        if (popUpIndex == 11) //Upgrade Stat
        {
            if (statAllocationApp.activeInHierarchy == true)
            {
                popUpIndex++;
               

            }
        }
        if (popUpIndex == 12) //Upgrade Stat - 2
        {
            if (xpManager.playerIncreasedHungerStat == true || xpManager.playerIncreasedStaminaStat == true || xpManager.playerIncreasedThristStat == true)
            {
                popUpIndex++;
            }
        }
        if (popUpIndex == 13) //Upgrade Stat - 3
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
    }


    public void ContinueButton()
    {
      popUpIndex++;
        qManager.CompleteCurrentQuest();
    }

}
