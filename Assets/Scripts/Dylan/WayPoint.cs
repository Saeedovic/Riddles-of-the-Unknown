using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Transform[] points;

    WayPointSystem wP;

    private void Awake()
    {
       
        wP = GetComponent<WayPointSystem>();
    }

    private void FixedUpdate()
    {
        points[1] = wP.wayPoint[wP.locationIndex];
    }


}
