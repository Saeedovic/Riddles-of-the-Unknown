using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

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

    [SerializeField] private Animator MovePlayerAnimator;
    [SerializeField] private string MovePlayerAnimator_Container = "UfoPlayerMove";

    public MeshCollider UfoMesh;
    public Material SciiColour;

    public GameObject WaypointMarker;

    public GameObject camShakeRef;

    public GameObject UfoLight;

    public GameObject movePlayer;

    [SerializeField] private string CreditsScene = "Credits";

    public AudioSource playerAudioSource;
    public AudioSource alienShipAudioSource;
    public AudioClip alienShipAudioClip;

    public GameObject BlackScreenPanel;

    public GameObject GameUI;


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
        playerAudioSource.volume = 0;
        GameUI.SetActive(false);
        playerObjRef.GetComponent<PlayerCon>().enabled = false;
        mouseObjRef.GetComponent<PlayerCameraController>().enabled = false;
        gameObject.layer = 0;
        UfoMesh.enabled = false;

        //Disable Phone Obj From Hand
        phoneObjRef.SetActive(false);

        //Change Camera

        CutSceneCam.SetActive(true);

        playerObjRef.transform.position = new Vector3(455.347992f, 114.056999f, 177.804993f);   //Set pos Of PLayer
        playerObjRef.transform.eulerAngles = new Vector3(playerObjRef.transform.eulerAngles.x, 254.634f, playerObjRef.transform.eulerAngles.z);  // Set Roatation of PLayer

        //Trigger PickUp Animation
        Player.Play(TouchShip_Clip_Animation, 0, 0.0f);

        
        movePlayer.GetComponent<MovePlayerAhead>().enabled = true;

        yield return new WaitForSeconds(1f);

        movePlayer.GetComponent<MovePlayerAhead>().enabled = false;

        yield return new WaitForSeconds(2.367f);
        UfoLight.SetActive(true);
        camShakeRef.GetComponent<CameraShake>().enabled = true;

        alienShipAudioSource.PlayOneShot(alienShipAudioClip);  //Alien Ship Take Off Audio

        yield return new WaitForSeconds(9f);

        BlackScreenPanel.SetActive(true);

        yield return new WaitForSeconds(6f);

        SceneManager.LoadScene("Credits"); // go to the credits sequence. 
        yield return null;
    }



}
