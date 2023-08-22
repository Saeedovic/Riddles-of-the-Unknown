using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycler : MonoBehaviour
{
    public bool dayCycleActive = true;
    [SerializeField] float timeFactor = 1;

    public GameObject SunObj;
    public static GameObject useTorchPopUpStatic;
    public GameObject useTorchPopUp;

    public static bool torchTutorialDoneStatic = false;
    public  bool torchTutorialDone = false;
 

    public FlashlightController flashlightController;


    private void Start()
    {
        useTorchPopUpStatic = useTorchPopUp;
        torchTutorialDoneStatic = torchTutorialDone;
    }

    void Update()
    {
       transform.Rotate(0.01f * timeFactor, 0, 0);
    }

    public static IEnumerator UseTorch()
    {
        yield return new WaitForSeconds(4);

        if(torchTutorialDoneStatic == false) 
        {
            useTorchPopUpStatic.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                useTorchPopUpStatic.SetActive(false);

                torchTutorialDoneStatic = true;

            }
        }
    }

}
