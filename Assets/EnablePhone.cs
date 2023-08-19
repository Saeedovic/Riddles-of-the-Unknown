using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePhone : MonoBehaviour
{
    PhoneManager phoneManager;
    [SerializeField] GameObject PhoneManagerObjRef;

    // Start is called before the first frame update
    void Start()
    {
       
        phoneManager = PhoneManagerObjRef.GetComponent<PhoneManager>();


    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
           // phoneManager.SetPhoneState(false);
           // phoneManager.CheckPhoneIsOut = true;
            phoneManager.phoneIsUseable = true;
        }
    }
}
