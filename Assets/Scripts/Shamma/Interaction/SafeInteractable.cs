using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeInteractable : PointOfInterest, IInteractableObject
{
    public GameObject SafeCam;
    public GameObject DefaultCam;

    public GameObject SafeCanvas;

    public Button closeButton;

    public GameObject PlayerMeshRef;

    public GameObject PlayerObjRef;

    NoteContainer movement;


    bool safePuzzleActive = false;
    public static bool safeHasBeenCracked = false;


    public void Start()
    {
        closeButton.onClick.AddListener(ExitCrackingSafe);
        SafeCanvas.SetActive(false);
    }

    public void Interact(PlayerInteractor user)
    {
        Debug.Log("Safe is in Range!");
        DefaultCam.SetActive(false);
        SafeCam.SetActive(true);
        SafeCanvas.SetActive(true);

        PlayerObjRef.GetComponent<PlayerCon>().enabled = false;
        PlayerObjRef.GetComponent<FlashlightController>().enabled = false;

        PlayerMeshRef.SetActive(false);

        safePuzzleActive = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (PhoneManager.phoneIsOut)
        {
            PhoneManager.Instance.SetPhoneState(true);
        }

        PhoneManager.Instance.mouseShouldBeUseable = true;
        PhoneManager.Instance.phoneIsUseable = false;
        WatchManager.watchShouldBeUseable = false;
    }

    public void ExitCrackingSafe()
    {
        SafeCam.SetActive(false);
        DefaultCam.SetActive(true);
        SafeCanvas.SetActive(false);

        PlayerObjRef.GetComponent<PlayerCon>().enabled = true;
        

        PlayerMeshRef.SetActive(true);
        PlayerObjRef.GetComponent<FlashlightController>().enabled = true;


        safePuzzleActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;



        PhoneManager.Instance.mouseShouldBeUseable = false;
        PhoneManager.Instance.phoneIsUseable = true;
        WatchManager.watchShouldBeUseable = true;
    }

    public bool IsInteractable()
    {
        if (!safePuzzleActive && !safeHasBeenCracked)
            return true;
        else
            PlayerMeshRef.SetActive(true);

        return false;
    }

    public void OnDeHighlight() { }

    public void OnHighlight() { }

    public bool riverCheck()
    {
        return true;
    }
}
