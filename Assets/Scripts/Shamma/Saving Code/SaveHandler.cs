using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// class that handles use of SaveLoadFuncs for a set number of saves.
// TODO: how to have a dynamic number of saves rather than three hardcoded ones?
public class SaveHandler : MonoBehaviour
{

    public SaveData saveSlot1, saveSlot2, saveSlot3;


    // call save with the given data and directory.
    public void SaveToSlot(SaveData data, Vector3 checkpoint, float health, string filePath) // test version of save func that allows for assigning save values.
    {
        data = new SaveData(checkpoint, health);
        SsaveLoadFuncs.Save(data, filePath);
        Debug.Log("Saving with " + data);
    }


    public void SaveToSlot(SaveData data, string filePath) // version of save that takes the current data (used in final game).
    {
        data = new SaveData();
        SsaveLoadFuncs.Save(data, filePath);
    }

    public void LoadSlot(SaveData dataToWriteTo, string filePath)
    {
        SsaveLoadFuncs.Load(dataToWriteTo, filePath);
        //dataToWriteTo.FinishLoad(); // set the in-game variables to the given data. 
        //PlayerHealth.currentHealth = dataToWriteTo.currentHealth;
        Debug.Log("Loaded " + dataToWriteTo);
    }



    // functions for saving different data to different paths.
    public void SaveToSlot1() { SaveToSlot(saveSlot1, SsaveLoadFuncs.filePath1); Debug.Log("Saving to file 1."); }
    public void SaveToSlot2() { SaveToSlot(saveSlot2, SsaveLoadFuncs.filePath2); }
    public void SaveToSlot3() { SaveToSlot(saveSlot3, SsaveLoadFuncs.filePath3); }

    public void LoadSlot1() { LoadSlot(saveSlot1, SsaveLoadFuncs.filePath1); }
    public void LoadSlot2() { LoadSlot(saveSlot2, SsaveLoadFuncs.filePath2); }
    public void LoadSlot3() { LoadSlot(saveSlot3, SsaveLoadFuncs.filePath3); }


    /*public void ExecuteSave1()
    {
        saveSlot1 = new SaveData(save1Checkpoint, save1Health);
        SaveLoad.Save(saveSlot1, SaveLoad.filePath1);
    }
    public void LoadSlot1()
    {
        SaveLoad.Load(saveSlot1, SaveLoad.filePath1);
        saveSlot1.FinishLoad(); // set the in-game variables to the given data.
    }*/
}
