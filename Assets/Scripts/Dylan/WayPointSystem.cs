using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WayPointSystem : MonoBehaviour
{
    public Image waypointMarker;
    public Transform[] wayPoint;

    public int locationIndex = 0;

    public TextMeshProUGUI DistanceFromWayPoint;

    public GameObject mapMarker;

    TutorialManager tManager;
    PlaytestTutorial playtestTutorial;
    [SerializeField] GameObject tutorialManagerObjRef;






    public bool fullMapActive = false;
    public GameObject fullMap;

    private void Awake()
    {
        tManager = tutorialManagerObjRef.GetComponent<TutorialManager>();
    
    }

        private void Start()
    {
        fullMap.SetActive(false);
    }

    void Update()
    {

        if(tManager.ActivateWayPoint == true)
        {
            waypointMarker.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.M) && fullMapActive == false)
             {
            fullMapActive = true;
            fullMap.SetActive(true);
             }

             else if (Input.GetKeyDown(KeyCode.M) && fullMapActive == true)
             {
            fullMapActive = false;
            fullMap.SetActive(false);
             }


             float minX = waypointMarker.GetPixelAdjustedRect().width / 2;
             float maxX = Screen.width - minX;
          
             float minY = waypointMarker.GetPixelAdjustedRect().height / 2;
             float maxY = Screen.width - minY;
          
             Vector2 pos = Camera.main.WorldToScreenPoint(wayPoint[locationIndex].transform.position);
          
          
          
             if (Vector3.Dot((wayPoint[locationIndex].transform.position - transform.position), transform.forward) < 0)
             {
                 //Target is Behind the Player
          
                 if(pos.x < Screen.width / 2)
                 {
                     pos.x = maxX;
                 }
                 else
                 {
                     pos.x = minX;
                 }
             }
          
             pos.x = Mathf.Clamp(pos.x, minX, maxX);
             pos.y = Mathf.Clamp(pos.y, minY, maxY);
          
             waypointMarker.transform.position = pos + Vector2.right;
          
             mapMarker.transform.position = wayPoint[locationIndex].transform.position;
          
             DistanceFromWayPoint.text = Vector3.Distance(wayPoint[locationIndex].transform.position, transform.position).ToString("0") + " m";
          
          
            

        } 


        if(tManager.ActivateWayPoint == false)

        {
            waypointMarker.gameObject.SetActive(false);
        }

    }

}
