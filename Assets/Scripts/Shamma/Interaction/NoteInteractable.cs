using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NoteContainer
{
    [SerializeField] Texture2D noteDisplayTexture;
    [SerializeField] int slotInNotesApp;

    public static Image uiToDisplayNote;

    public static PlayerCameraController playerCam;
    static PlayerCon playerRef;
    static PhoneNotesApp notesApp;
    float normalTimeScale;
    public bool isInInteraction { get; private set; }


    public void DisplayNote()
    {
        if (playerCam != null)
        {
            playerRef = playerCam.GetComponentInParent<PlayerCon>();
            notesApp = playerCam.GetComponentInParent<PhoneNotesApp>();
            playerCam.enabled = false;
        }

        normalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        uiToDisplayNote.gameObject.SetActive(true);
        SetUpNoteImage();

        isInInteraction = true;
        playerRef.StartCoroutine(WaitForContinue());
    }

    void SetUpNoteImage()
    {

    }

    IEnumerator WaitForContinue()
    {
        yield return null;

        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Return))
        {
            // add this note to the notes app 
            notesApp.AddNote(this, slotInNotesApp);

            // unpause
            playerCam.enabled = true;
            Time.timeScale = normalTimeScale;

            // stop displaying note
            uiToDisplayNote.gameObject.SetActive(false);
            isInInteraction = false;
            //AudioSource.PlayClipAtPoint(endInteractionAudio, playerCam.transform.position);

            yield break;
        }

        playerRef.StartCoroutine(WaitForContinue());
    }

}


public class NoteInteractable : PointOfInterest, IInteractableObject
{
    [SerializeField] NoteContainer noteInfo;

    [SerializeField] Image uiToDisplayNote;
    //[SerializeField] Texture2D noteTexture;
    [SerializeField] AudioClip endInteractionAudio;


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
            NoteContainer.playerCam.enabled = false;
        }

        noteInfo.DisplayNote();
    }


    public bool IsInteractable()
    {
        if (noteInfo.isInInteraction)
            return false;
        else
            return true;
    }

    public void OnDeHighlight() { }

    public void OnHighlight() { }
}
