using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class NoteContainer
{
    [SerializeField] Sprite noteDisplayTexture;
    [SerializeField] string noteText;
    [SerializeField] float textSize = 40f;
    public int slotInNotesApp;

    public static Image uiToDisplayNote;
    public static TextMeshProUGUI textboxForNote;

    public static PlayerCameraController playerCam;
    public static PlayerCon playerRef;
    //public static PhoneNotesApp notesApp;
    float normalTimeScale;
    public static bool isInInteraction { get; private set; }


    public void DisplayNote()
    {
        playerCam.enabled = false;

        normalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        uiToDisplayNote.gameObject.SetActive(true);
        SetUpNoteImage();

        isInInteraction = true;
        playerRef.StartCoroutine(WaitForContinue());
    }

    void SetUpNoteImage()
    {
        // may add code here to readjust image and textbox size to fit sprite size? depends if we need that
        uiToDisplayNote.sprite = noteDisplayTexture;
        textboxForNote.text = noteText;
        textboxForNote.fontSize = textSize;
    }

    IEnumerator WaitForContinue()
    {
        yield return null;

        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Return))
        {
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