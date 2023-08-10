using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseTorch : MonoBehaviour
{
    FlashlightController flashCont;

    DayNightCycler TorchUI;

    public GameObject TorchPrefab;
    public GameObject sun;


    private void Start()
    {
        flashCont = TorchPrefab.GetComponent<FlashlightController>();
        TorchUI = sun.GetComponent<DayNightCycler>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && flashCont.flashLightIsOn == false)
        {
            TorchUI.useTorchPopUp.SetActive(true);
            StartCoroutine(TorchIsOff());
            
        }

        if (collider.gameObject.tag == "Player" && flashCont.flashLightIsOn == true)
        {
            TorchUI.useTorchPopUp.SetActive(false);
            StartCoroutine(TorchIsOn());

        }
    }

    IEnumerator TorchIsOn()
    {
        //Add Voice that says Glad I got my FlashLight Along!

        

        yield return new WaitForSeconds(2);


    }

    IEnumerator TorchIsOff()
    {
        //Add Voice that says Probably best to use my Torch right now..


        yield return new WaitForSeconds(2);


    }
}