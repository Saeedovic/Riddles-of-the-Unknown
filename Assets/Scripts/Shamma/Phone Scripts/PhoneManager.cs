using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] GameObject phoneScreen;
    [SerializeField] List<GameObject> mainPhoneButtons; // used to set the input system up

    [SerializeField] Transform regularScreenPos;
    [SerializeField] Transform fullscreenScreenPos;
    bool isFullscreen = true;

    public static PhoneManager Instance;

    public GameObject playerUI;

    public AudioClip AudioForOpeningPhone;
    public static bool phoneIsOut { get; private set; }

    void Start()
    {
        Instance = this;
        PhoneMainMenu.InitPhone(phoneScreen); // pass the given phone screen to PhoneMainMenu for it to load app components into its list


        //SetFullscreen(isFullscreen);
        //savedScreenPos = GetComponent<RectTransform>();
        //regularScreenPos = transform.position;
        //regularScreenRot = transform.rotation;
        //regularScreenRot = transform.rotation;


        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainPhoneButtons[0]);


        phoneIsOut = true;
        SetPhoneState(phoneIsOut); // close phone.

        PhoneMainMenu.onAppOpen += ForceFullscreenOn;
        PhoneMainMenu.onAppClose += ForceFullscreenOff; // need to know if we should switch to fullscreen or no
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AudioSource.PlayClipAtPoint(AudioForOpeningPhone, transform.position);

            SetPhoneState(phoneIsOut);

            // make sure first app button is highlighted
            if (phoneIsOut && !PhoneMainMenu.appIsOpen)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(mainPhoneButtons[0]); 
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SetFullscreen(isFullscreen);
        }
    }


    void SetPhoneState(bool phoneActive)
    {
        // when phone is inactive, make it active, and vice versa.
        if (!phoneActive)
        {
            phoneScreen.SetActive(true);
            PhoneMainMenu.RefreshPhone();
        }
        else
        {
            phoneScreen.SetActive(false);
        }

        phoneIsOut = !phoneIsOut; // flip the bool so we know we're in the other state
    }



    void SetFullscreen(bool isFullscreen)
    {
        // check if we're in an app that shouldn't leave fullscreen, always set fullscreen true if so
        if (PhoneMainMenu.activeAppScreen != null) 
        {
            if (!PhoneMainMenu.activeAppScreen.hasFullscreenAsOption)
                isFullscreen = true;
        }

        // if false, make app not fullscreen. if true, make it fullscreen
        if (isFullscreen)
        {
            transform.position = fullscreenScreenPos.position;
            transform.rotation = fullscreenScreenPos.rotation;
            //Debug.Log("big");
            //this.isFullscreen = true;
        }
        else
        {
            transform.position = regularScreenPos.position;
            transform.rotation = regularScreenPos.rotation;
            //Debug.Log("small");
            //this.isFullscreen = false;
        }

        this.isFullscreen = !this.isFullscreen; // same thing as phone being active
    }

    void ForceFullscreenOn(PhoneAppButton app)
    {
        if (!PhoneMainMenu.activeAppScreen.hasFullscreenAsOption)
        {
            SetFullscreen(true);
        }
    }

    void ForceFullscreenOff()
    {
        SetFullscreen(false);
    }

}
