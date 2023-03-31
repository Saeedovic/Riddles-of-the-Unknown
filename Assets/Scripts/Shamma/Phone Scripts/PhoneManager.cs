using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] GameObject phoneScreen;
    [SerializeField] List<GameObject> mainPhoneButtons;
    public static bool phoneIsOut { get; private set; }

    void Start()
    {
        phoneIsOut = true;
        SetPhoneState(phoneIsOut); // close phone.
        PhoneMainMenu.InitPhone(phoneScreen); // pass the given phone screen to PhoneMainMenu for it to load app components into its list

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainPhoneButtons[0]);
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
        // when phone is inactive, make it active, and vice versa.
        if (!phoneActive)
        {
            phoneScreen.SetActive(true);
        }
        else
        {
            phoneScreen.SetActive(false);
        }

        phoneIsOut = !phoneIsOut; // flip the bool so we know we're in the other state
    }
}
