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


    private void Start()
    {
        if (_notesApp == null)
        {
            _notesApp = notesApp;
        }

        uiToDisplayNote.gameObject.SetActive(false);

        if (NoteContainer.uiToDisplayNote == null)
        {
            NoteContainer.uiToDisplayNote = uiToDisplayNote;
        }
        if (NoteContainer.textboxForNote == null)
        {
            NoteContainer.textboxForNote = textboxForNote;
        }
    }


    public void Interact(PlayerInteractor user)
    {
        if (NoteContainer.playerCam == null)
        {
            NoteContainer.playerCam = user.GetComponentInChildren<PlayerCameraController>();
            NoteContainer.playerRef = user.GetComponent<PlayerCon>();
        }

        // add this note to the notes app 

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













        //Trigger the below After PLayer STANDS UP!
        notesApp.AddNote(noteInfo);//, noteInfo.slotInNotesApp);

        noteInfo.DisplayNote();
        //
        gameObject.SetActive(false);


        yield return null;
    }
}
