using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonCycle : MonoBehaviour
{

    public bool NightCycleActive = true;
    [SerializeField] float timeFactor = 1;


    // Update is called once per frame
    void Update()
    {
        
       transform.Rotate(-0.01f * timeFactor, 0, 0);

    }
}
