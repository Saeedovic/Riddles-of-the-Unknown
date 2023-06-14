using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{
    public Camera fullMapCam;

    public bool fullMapActive = false;
    public GameObject miniMap;
    public GameObject fullMap;


    private void Start()
    {
        miniMap.SetActive(true);
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.G))
        {
            fullMapActive = true;
            miniMap.SetActive(false);
            fullMap.SetActive(true);
        }

        if (Input.GetKey(KeyCode.U) && fullMapActive == true)
        {
            fullMapActive = false;
            miniMap.SetActive(true);
            fullMap.SetActive(false);
        }
    }
}
