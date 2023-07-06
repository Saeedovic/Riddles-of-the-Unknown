using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PlaytestTutorial : MonoBehaviour
{

    public GameObject[] popUps;
    [SerializeField] public int popUpIndex;
    QuestManager qManager;
    InventoryHandler inventoryHandler;

    [SerializeField] GameObject playerObjRef;
    [SerializeField] GameObject PhoneAppRef;
    [SerializeField] GameObject WaypointSystemRef;
    [SerializeField] GameObject WatchObjRef;
    [SerializeField] GameObject phoneObjRef;

    XPManager xpManager;
    PhoneCameraApp app;
    WayPointSystem wpSystem;
    WatchManager watchManager;
    PlayerCon playerCont;
    PlayerInteractor interactor;

    public GameObject cameraApp;


    public bool TutorialSectionCompleted;

    public GameObject tutorialCamera;
    public GameObject mainCamera;





    // Start is called before the first frame update
    void Start()
    {
        qManager = playerObjRef.GetComponent<QuestManager>();
        xpManager = playerObjRef.GetComponent<XPManager>();
        app = PhoneAppRef.GetComponent<PhoneCameraApp>();
        wpSystem = WaypointSystemRef.GetComponent<WayPointSystem>();
        watchManager = WatchObjRef.GetComponent<WatchManager>();
        playerCont = playerObjRef.GetComponent<PlayerCon>();
        interactor = playerObjRef.GetComponent<PlayerInteractor>();

        app.enteredFullscreen = false;

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
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
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

            mainCamera.SetActive(false);
            tutorialCamera.SetActive(true);

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
            if (Input.GetKeyDown(KeyCode.K))
            {
                popUps[6].SetActive(false);

                popUpIndex++;

            }

        }

        if (popUpIndex == 7) //Take a Picture - 3
        {

            interactor.enabled = false;
            mainCamera.SetActive(false);
            tutorialCamera.SetActive(true);

            if (cameraApp.activeInHierarchy == true)  //Add condition to check if player is in radius of Village
            {
                mainCamera.SetActive(true);
                tutorialCamera.SetActive(false);
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



    }
}
