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
    [SerializeField] public bool cantBeClosed = false;
    //public int slotInNotesApp;

    public  Image uiToDisplayNote;
    public  TextMeshProUGUI textboxForNote;

    public static PlayerCameraController playerCam;
    public static PlayerCon playerRef;
    //public static PhoneNotesApp notesApp;
    float normalTimeScale;
    public static bool isInInteraction;

    public void Intialize(Image UItoDisplayNote, TextMeshProUGUI TextToDisplay)
    {
        this.uiToDisplayNote = UItoDisplayNote;
        this.textboxForNote = TextToDisplay;
    }


    public void DisableMovement()
    {
        playerCam.enabled = false;
        playerRef.enabled = false;
    }

    public void EnableMovement()
    {
        Time.timeScale = normalTimeScale;
        playerRef.enabled = true;
        playerCam.enabled = true;
    }

    public void DisplayNote()
    {
        //  playerCam.enabled = false;

        //IsCloseableStatic = IsCloseable;
        playerRef.GetComponent<FlashlightController>().enabled = false;
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

    public void CloseNote()
    {
        // unpause

        playerCam.enabled = true;
        playerRef.GetComponent<FlashlightController>().enabled = true;

        Time.timeScale = normalTimeScale;

        // stop displaying note
        uiToDisplayNote.gameObject.SetActive(false);
        isInInteraction = false;

        playerRef.StopCoroutine(WaitForContinue());
    }



    IEnumerator WaitForContinue()
    {
        yield return null;

        if (Input.GetKey(KeyCode.Return) && (cantBeClosed == false || PhoneMainMenu.appIsOpen))
        {
            CloseNote();

            //AudioSource.PlayClipAtPoint(endInteractionAudio, playerCam.transform.position);

            yield break;
        }
        //IsCloseable = true;  //i Added this line cause if i dotn i cant interact with the notes in the App!
        playerRef.StartCoroutine(WaitForContinue());
    }

}