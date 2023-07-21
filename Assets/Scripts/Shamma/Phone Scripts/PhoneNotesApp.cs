using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PhoneNotesApp : PhoneAppScreen
{
    [SerializeField] int noteListLength;

    List<NoteContainer> notes;
    public List<NoteContainer> allNoteSlots { get { return notes; } }


    protected override void Start()
    {
        notes = new List<NoteContainer>();
        notes.Capacity = noteListLength;
    }

    public void AddNote(NoteContainer newNote, int noteSlot)
    {
        if (notes[noteSlot] == null)
        {
            notes[noteSlot] = newNote;
        }
    }

    
}