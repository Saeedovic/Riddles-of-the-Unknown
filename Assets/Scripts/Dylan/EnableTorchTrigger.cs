using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTorchTrigger : MonoBehaviour
{

    public GameObject torchTriggerCave1;
    public GameObject torchTriggerCave2;


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            torchTriggerCave1.SetActive(true);
            torchTriggerCave2.SetActive(true);

        }
    }

}
