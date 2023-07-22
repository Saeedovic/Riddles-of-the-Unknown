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

    // i tried to just set up an array for notes in start but for some reason couldn't initialise it properly.
    // count remained at 0 or array remained null even though other code in this func worked.
    // so, for now it's a list we add to, and we open notes at the corresponding number to the button in the app
    /*
    protected override void Start()
    {
        
        

        // set up button with corresponding note slot to display
        for (int i = 0; i < allButtonsForCheckingNotes.Capacity; i++)
        {
            allButtonsForCheckingNotes[i].onClick.AddListener(delegate { DisplayNote(i); });
            //allButtonsForCheckingNotes[i].interactable = false;
        }

    }
    void Awake()
    {
            for (int i = 0; i < allButtonsForCheckingNotes.Count; i++)
            {
                notes.Add(null);
            }
    }*/

    // set corresponding slot with given note. 
    // requires that you have a specific slot number for each note in unity! 
    // at least that's how it's supposed to work, it's just a list right now
    public void AddNote(NoteContainer newNote, int noteSlot)
    {
        notes.Add(newNote);
        // set the ui button to the note image here.
    }

    // i have no idea why this isn't being called anymore, it used to work i swear
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
            Debug.Log("you haven't picked this note up yet");
        }
    }
}