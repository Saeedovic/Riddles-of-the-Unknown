using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// object class to store the saveable variables associated with the player.
public class SaveData
{
    // variables that represent vars of equivalent data types in player classes, to be saved
    SerializableVector3 currentCheckpoint;
    float currentHealth;
    // string currentObjective;
    // Inventory.Items currItems;

    public SaveData()
    {
        // set class' variables to the player's equivalent variables, to store them in saves
    }

    public SaveData(Vector3 checkpoint, float health)
    {
        // overload variant to save with a passed amount of items, health, position etc.
    }

    public void FinishLoad()
    {
        // called at the end of the load function. sets the variables to the ones loaded from file.
    }

}

[System.Serializable]
class SerializableVector3
{
    public float x;
    public float y;
    public float z;
}
