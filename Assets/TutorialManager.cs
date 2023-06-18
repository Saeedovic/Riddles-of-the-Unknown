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

    [SerializeField] GameObject playerObjRef;
    [SerializeField] GameObject PhoneAppRef;
    [SerializeField] GameObject WaypointSystemRef;
        





    private void Awake()
    {
        qManager = playerObjRef.GetComponent<QuestManager>();
        xpManager = playerObjRef.GetComponent<XPManager>();
        app = PhoneAppRef.GetComponent<PhoneCameraApp>();
        wpSystem = WaypointSystemRef.GetComponent<WayPointSystem>();
    }


    // Update is called once per frame
    void Update()
    {
        qManager.currentQuestIndex = popUpIndex;

       for (int i = 0; i < popUps.Length; i++)
        {
            if(i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);

            }
        }

       if(popUpIndex == 0) // WASD MOVEMENT
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
            if (Input.GetKey(KeyCode.J))
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest();
            }
        }

        if (popUpIndex == 5) //Take a Picture
        {
            if (app.pictureTaken == true)
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest();
            }
        }

        if (popUpIndex == 6) // Upgrade 1 Stat
        {
            if (xpManager.playerIncreasedThristStat == true || xpManager.playerIncreasedStaminaStat == true || xpManager.playerIncreasedHungerStat == true)
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest();
   
            }
        }

        if (popUpIndex == 7) // Find Clue
        {
           /* if ()  
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest();

            }
           */
        }

        if (popUpIndex == 8) // Find the Shack
        {
            if (wpSystem.TutorialSectionCompleted == true)
            {
                popUpIndex++;
                qManager.CompleteCurrentQuest();

            }
        }
    }
}
