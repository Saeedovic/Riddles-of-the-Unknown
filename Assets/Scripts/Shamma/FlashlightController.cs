using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] GameObject flashLight;
    bool flashLightIsOn = false;

    void Start()
    {
        flashLight.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && flashLightIsOn == false)
        {
            flashLight.SetActive(true);
            flashLightIsOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && flashLightIsOn == true)
        {
            flashLight.SetActive(false);
            flashLightIsOn = false;
        }
    }
}
