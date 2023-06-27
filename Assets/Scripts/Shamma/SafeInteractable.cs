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


    bool safePuzzleActive = false;
    public static bool safeHasBeenCracked = false;


    public void Start()
    {
        SafeCanvas.SetActive(false);
    }

    public void Interact(PlayerInteractor user)
    {
        Debug.Log("Safe is in Range!");
        DefaultCam.SetActive(false);
        SafeCam.SetActive(true);
        SafeCanvas.SetActive(true);

        closeButton.onClick.AddListener(ExitCrackingSafe);
        safePuzzleActive = true;
    }

    public void ExitCrackingSafe()
    {
        SafeCam.SetActive(false);
        DefaultCam.SetActive(true);
        SafeCanvas.SetActive(false);

        closeButton.onClick.AddListener(ExitCrackingSafe);

        if (!safeHasBeenCracked)
        {
            safePuzzleActive = false;
        }
    }

    public bool IsInteractable()
    {
        if (!safePuzzleActive && !safeHasBeenCracked)
            return true;
        else 
            return false;
    }

    public void OnDeHighlight() { }

    public void OnHighlight() { }
}
