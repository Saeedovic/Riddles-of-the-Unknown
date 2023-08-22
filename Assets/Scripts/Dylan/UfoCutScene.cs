using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoCutScene : PointOfInterest, IInteractableObject
{
    [SerializeField] bool isInteractable = true;

    public GameObject CutSceneCam;
    public GameObject playerObjRef;
    public GameObject mouseObjRef;
    public GameObject phoneObjRef;
    NoteContainer noteInfo;

    public bool voiceOverCompleted = false;

    [SerializeField] bool CutSceneEnabled;

    [SerializeField] private Animator Player;
    [SerializeField] private string TouchShip_Clip_Animation = "TouchShip_Clip_Revised";

    public MeshCollider UfoMesh;
    public Material SciiColour;

    public GameObject WaypointMarker;

    public GameObject camShakeRef;

    public GameObject UfoLight;
    



    public void Interact(PlayerInteractor user)
    {
        Debug.Log("hello! you just clicked me");

        SetScannabilityOff();

        StartCoroutine(TriggerEndGameCutScene());


    }

    public bool IsInteractable()
    {
        if (isInteractable)
            return true;
        else
            return false;
    }

    public void OnDeHighlight()
    {
        Debug.Log("hi, I'm now unhighlighted");
    }

    public void OnHighlight()
    {
        Debug.Log("hi, I'm highlighted");
        
    }

    public bool riverCheck()
    {
        return true;
    }

    IEnumerator TriggerEndGameCutScene()
    {
        WaypointMarker.SetActive(false);
        gameObject.layer = 0;
        UfoMesh.enabled = false;
        playerObjRef.transform.position = new Vector3(454.175995f, 114.067719f, 177.220596f);   //Set pos Of PLayer

        playerObjRef.transform.eulerAngles = new Vector3(playerObjRef.transform.eulerAngles.x, 254.634f, playerObjRef.transform.eulerAngles.z);  // Set Roatation of PLayer

        //Change Camera

        CutSceneCam.SetActive(true);

        //Disable movement & Camera Movement
        // noteInfo.DisableMovement();
        playerObjRef.GetComponent<PlayerCon>().enabled = false;
        mouseObjRef.GetComponent<PlayerCameraController>().enabled = false;


        //Trigger PickUp Animation
        Player.Play(TouchShip_Clip_Animation, 0, 0.0f);


        //Disable Phone Obj From Hand
        phoneObjRef.SetActive(false);
        yield return new WaitForSeconds(3.367f);
        UfoLight.SetActive(true);
        camShakeRef.GetComponent<CameraShake>().enabled = true;

        //Add Light Up Effect

        //Add Sound Of UFO Engine Starting

        //End Game




        yield return null;
    }



}
