using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NoteInteractable : PointOfInterest, IInteractableObject
{
    [SerializeField] NoteContainer noteInfo;

    [SerializeField] Image uiToDisplayNote;
    //[SerializeField] PhoneNotesApp notesApp; // will uncomment once phonenotesapp is done
    //[SerializeField] Texture2D noteTexture;
    //[SerializeField] AudioClip endInteractionAudio;


    private void Start()
    {
        uiToDisplayNote.gameObject.SetActive(false);

        if (NoteContainer.uiToDisplayNote == null)
            NoteContainer.uiToDisplayNote = uiToDisplayNote;
    }


    public void Interact(PlayerInteractor user)
    {
        if (NoteContainer.playerCam == null)
        {
            NoteContainer.playerCam = user.GetComponentInChildren<PlayerCameraController>();
            NoteContainer.playerRef = user.GetComponent<PlayerCon>();
            //NoteContainer.notesApp = notesApp;
        }

        // add this note to the notes app 
        //NoteContainer.notesApp.AddNote(noteInfo, noteInfo.slotInNotesApp);

        noteInfo.DisplayNote();
        gameObject.SetActive(false);
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
}
