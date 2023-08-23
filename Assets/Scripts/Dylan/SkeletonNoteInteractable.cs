using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkeletonNoteInteractable : PointOfInterest, IInteractableObject
{
    [SerializeField] NoteContainer noteInfo;

    [SerializeField] Image uiToDisplayNote;
    [SerializeField] TextMeshProUGUI textboxForNote;
    [SerializeField] PhoneNotesApp notesApp;
    static PhoneNotesApp _notesApp;
    //[SerializeField] Texture2D noteTexture;
    //[SerializeField] AudioClip endInteractionAudio;

    public AudioSource playerAudio;

    public AudioClip noteVoiceOver;

    public GameObject CutSceneCam;
    public GameObject playerObjRef;
    public GameObject phoneObjRef;
    public GameObject phoneFuncRef;



    public bool voiceOverCompleted = false;

    [SerializeField] bool CutSceneEnabled;

    [SerializeField] private Animator Player;
    [SerializeField] private Animator CameraAnimatorPlayer;

    [SerializeField] private string Pick_Up_Animation = "Pickup_Clip_Revised";
    [SerializeField] private string Idle_Animation = "Idle";
    [SerializeField] private string Camera_Animation = "Skeleton_CutScene";

    public MeshRenderer noteMesh;

    public PlayerCameraController playerCameraController;

    public GameObject GameUI;



    private void Start()
    {
        // CutSceneEnabled = false;

        


        if (_notesApp == null)
        {
            _notesApp = notesApp;
        }

        uiToDisplayNote.gameObject.SetActive(false);

        noteInfo.Intialize(uiToDisplayNote, textboxForNote);
    }


    public void Interact(PlayerInteractor user)
    {
        if (NoteContainer.playerCam == null)
        {
            NoteContainer.playerCam = user.GetComponentInChildren<PlayerCameraController>();
            NoteContainer.playerRef = user.GetComponent<PlayerCon>();
        }

        //Trigger CutScene

       StartCoroutine(TriggerCutScene());
        
        
    }


    public bool IsInteractable()
    {
        if (NoteContainer.isInInteraction)
            return false;
        else
            return true;
    }

    public void OnDeHighlight() { }

    public void OnHighlight() { }

    public bool riverCheck()
    {
        return true;
    }

    IEnumerator TriggerCutScene()
    {
        gameObject.layer = 0;
        // Deactivate Watch & Phone
        playerObjRef.GetComponent<WatchManager>().SetWatchState(true);
        //  phoneFuncRef.GetComponent<PhoneManager>().SetPhoneState(true);
       // gameObject.SetActive(false);
        GameUI.SetActive(false);
        playerObjRef.transform.position = new Vector3(502.289825f, 115.629646f, 184.796478f);

        playerObjRef.transform.eulerAngles = new Vector3(playerObjRef.transform.eulerAngles.x, 260, playerObjRef.transform.eulerAngles.z);

        //Change Camera
        CutSceneCam.SetActive(true);

        //Disable movement & Camera Movement
        noteInfo.DisableMovement();

        CameraAnimatorPlayer.Play(Camera_Animation, 0, 0.0f);

        //Trigger PickUp Animation
        Player.Play(Pick_Up_Animation, 0, 0.0f);
       
        //Disable Phone Obj From Hand
        phoneObjRef.SetActive(false);
        yield return new WaitForSeconds(3.0f);

        noteMesh.enabled = false;

        yield return new WaitForSeconds(3.367f); //Wait Until Animation Has been Completed

        CutSceneCam.SetActive(false);

        Player.Play(Idle_Animation, 0, 0.0f);
        //Trigger the below After PLayer STANDS UP!
         notesApp.AddNote(noteInfo);//, noteInfo.slotInNotesApp);

         noteInfo.DisplayNote();

        playerAudio.PlayOneShot(noteVoiceOver);
        //PlayerAudioCaller.Instance.PlayAudio(noteVoiceOver, playerAudio);
         yield return new WaitForSecondsRealtime(40.5f);
         voiceOverCompleted = true;
        
        if(voiceOverCompleted == true)
        {
            noteInfo.CloseNote();  //Disable UI

            noteInfo.EnableMovement();

            phoneObjRef.SetActive(true);
            GameUI.SetActive(true);
            gameObject.SetActive(false);  
        }

        yield return null;
    }
}
