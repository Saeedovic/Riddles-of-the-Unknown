using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeNoteInteractable : PointOfInterest, IInteractableObject
{
    [SerializeField] NoteContainer noteInfo;
    public NoteContainer publicNoteInfo { get { return noteInfo; } }

    [SerializeField] Image uiToDisplayNote;
    [SerializeField] TextMeshProUGUI textboxForNote;
    [SerializeField] PhoneNotesApp notesApp;
    static PhoneNotesApp _notesApp;
    public AudioSource playerAudio;
    public AudioClip noteVoiceOver;
    public bool voiceOverCompleted = false;

    bool FirstInteractionCompleted = false;

    public TutorialManager tManager;

    private void Start()
    {
        // CutSceneEnabled = false;

        if (_notesApp == null)
        {
            _notesApp = notesApp;
        }

        uiToDisplayNote.gameObject.SetActive(false);

        noteInfo.Intialize(uiToDisplayNote, textboxForNote);
        tManager.GetComponent<TutorialManager>();
    }


    public void Interact(PlayerInteractor user)
    {
        if (NoteContainer.playerCam == null)
        {
            NoteContainer.playerCam = user.GetComponentInChildren<PlayerCameraController>();
            NoteContainer.playerRef = user.GetComponent<PlayerCon>();
        }
        if (FirstInteractionCompleted == false)
        {
          StartCoroutine(TriggerNoteVoiceOver());
            FirstInteractionCompleted = true;
        }


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

    IEnumerator TriggerNoteVoiceOver()
    {

        PlayerAudioCaller.Instance.PlayAudio(noteVoiceOver, playerAudio);

        tManager.subtitleObj.SetActive(false);

        notesApp.AddNote(noteInfo);//, noteInfo.slotInNotesApp);

        noteInfo.DisplayNote();

        noteInfo.DisableMovement();

        yield return new WaitForSecondsRealtime(19.5f);

        voiceOverCompleted = true;

        if (voiceOverCompleted == true)
        {
            noteInfo.CloseNote();  //Disable UI

            noteInfo.EnableMovement();

            gameObject.SetActive(false);
        }

        yield return null;
    }
}