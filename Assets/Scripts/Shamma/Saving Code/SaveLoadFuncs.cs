using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


// separate class from SaveHandler just to handle the actual save / load processes, and minimise references to BinaryFormatter.
// credit goes to myself, written this on a previous project: https://github.com/T3NSH11/The-Rad-Ninja
// TODO: switch default save depending on previous save.
public class SsaveLoadFuncs : MonoBehaviour
{
    static SaveData saveData; // object to hold the data we want saved

    static SaveData prevSave;
    static string prevSavePath;

    public static string filePath1 = Application.persistentDataPath + "/playersaveone.dat";
    public static string filePath2 = Application.persistentDataPath + "/playersavetwo.dat";
    public static string filePath3 = Application.persistentDataPath + "/playersavethree.dat"; // names for different file paths 


    // should be called when the player dies, or reaches a checkpoint
    public static void Save()
    {
        if (prevSavePath == null)
            prevSavePath = filePath1; // default to file 1.
                                      // actually now that i think about it, this would break if you reload the game,
                                      // which is the whole point of a save system. maybe you could keep the last save path in
                                      // a playerprefs file?
        
        using (FileStream stream = new FileStream(prevSavePath, FileMode.Create)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            saveData = new SaveData(); // make a file with current stats
            formatter.Serialize(stream, saveData);
        }
    }

    public static void Save(SaveData givenSave, string saveTo)
    {

        using (FileStream stream = new FileStream(saveTo, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            saveData = givenSave; // use the specified parameters for what the save data should look like
            formatter.Serialize(stream, saveData);
        }

        prevSave = givenSave;
        prevSavePath = saveTo;
    }


    public static void Load(SaveData givenSave, string loadFrom)
    {

        if (!File.Exists(loadFrom))
        {
            Debug.LogError("Save file couldn't be found!");
            return;
        }

        using (FileStream stream = new FileStream(loadFrom, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            givenSave = (SaveData)formatter.Deserialize(stream);
            givenSave.FinishLoad();  // sets the variables in game to the data that we've loaded from the file. 
        }
    }

}
