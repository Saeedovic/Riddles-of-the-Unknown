using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PhoneNotesApp : PhoneAppScreen
{
    List<NoteContainer> notes = new List<NoteContainer>();
    public List<NoteContainer> allNoteSlots { get { return notes; } }
    public List<Button> allButtonsForCheckingNotes = new List<Button>();
    public Sprite noteCollectedIcon;

    bool firstNoteAdded = false;

    public AudioSource playerAudio;

    public AudioClip voiceOverFirstNotePicked;

    // i tried to just set up an array for notes in start but for some reason couldn't initialise it properly.
    // count remained at 0 or array remained null even though other code in this func worked.
    // so, for now it's a list we add to, and we open notes at the corresponding number to the button in the app

    public override void Start()
    {
        // set up buttons with the corresponding note slot value to call displaynote with.
        for (int i = 0; i < allButtonsForCheckingNotes.Capacity; i++)
        {
            // need i as its own var, for some reason
            int j = i;
            allButtonsForCheckingNotes[i].onClick.RemoveAllListeners();
            allButtonsForCheckingNotes[i].onClick.AddListener(delegate { DisplayNote(j); });

            notes.Add(null); // add a slot for every button

            //Debug.Log("button " + allButtonsForCheckingNotes[i]);
        }

        base.Start();

    }

    // add given note to the next available slot space.
    public void AddNote(NoteContainer newNote)//, int noteSlot)
    {
        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i] == null)
            {
              /*  if (!firstNoteAdded)
                {
                    
                    PlayerAudioCaller.Instance.PlayAudio(voiceOverFirstNotePicked, playerAudio);

                    StartCoroutine(TutorialManager.DisplaySubs("Ah my first note let's read its content.", 2.5f));

                    firstNoteAdded = true;
                }
              */
                notes[i] = newNote;
                PhoneManager.onClosePhone += notes[i].CloseNote; // sub closing notes to closing the phone

                allButtonsForCheckingNotes[i].image.sprite = noteCollectedIcon;
                // set the ui button to the note image here.

                PhoneMainMenu.onAppClose += notes[i].CloseNote;

                Debug.Log("you got a new note!");
                return;
            }
        }

        Debug.LogError("no space found for note. you may want to increase slot count.");
    }

    // display note via the button setup
    public void DisplayNote(int noteSlot)
    {
        
        if (notes[noteSlot] != null)
        {
            if (!NoteContainer.isInInteraction)
            {
                notes[noteSlot].DisplayNote();
            }
        }
        else
        {
            Debug.Log("you haven't picked this note up yet - slot " + noteSlot);
        }
    }
}