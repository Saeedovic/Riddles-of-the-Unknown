using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchTrigger : MonoBehaviour
{

    public GameObject CrouchUIPopUp;

    private void Start()
    {
    
        CrouchUIPopUp.SetActive(false);

    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            CrouchUIPopUp.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            CrouchUIPopUp.SetActive(false);
        }
    }
}
