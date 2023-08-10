using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycler : MonoBehaviour
{
    public bool dayNightCycleActive = true;
    [SerializeField] float timeFactor = 1;

    public GameObject SunObj;
    public GameObject useTorchPopUp;

    bool torchTutorialDone = false;


    void Update()
    {
        transform.Rotate(0.01f * timeFactor, 0, 0);

        if(SunObj.transform.localRotation.eulerAngles.x > 170)
        {
            Debug.Log("Its night Time");
            StartCoroutine(UseTorch());
        }
    }

    IEnumerator UseTorch()
    {
        //Add Voice that says mmm Its Quite Dark, Good thing i Have a Torch
        yield return new WaitForSeconds(2);

        if(torchTutorialDone == false) 
        {
            useTorchPopUp.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                useTorchPopUp.SetActive(false);

                torchTutorialDone = true;

            }
        }
    }

}
