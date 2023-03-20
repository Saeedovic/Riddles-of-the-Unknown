using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] GameObject phoneScreen;
    bool phoneIsOut = false;

    void Start()
    {
        SetPhoneState(phoneIsOut);
        PhoneMainMenu.InitPhone(phoneScreen);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetPhoneState(phoneIsOut);
        }
    }

    void SetPhoneState(bool phoneActive)
    {
        if (phoneActive)
        {
            phoneScreen.SetActive(true);
        }
        else
        {
            phoneScreen.SetActive(false);
        }

        phoneIsOut = !phoneIsOut;
    }
}
