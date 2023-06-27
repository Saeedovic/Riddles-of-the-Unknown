using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneManager : MonoBehaviour
{
    public  GameObject phoneScreen;
    [SerializeField] List<GameObject> mainPhoneButtons; // used to set the input system up

    [SerializeField] public Transform regularScreenPos;
    [SerializeField] Transform fullscreenScreenPos;

    public static PhoneManager Instance;

    public GameObject playerUI;

    public AudioClip AudioForOpeningPhone;

    public static bool isFullscreen { get; private set; }
    public static bool phoneIsOut { get; private set; }

    public delegate void OnEnterFullscreen();
    public static OnEnterFullscreen onEnterFullscreen;
    public delegate void OnExitFullscreen();
    public static OnExitFullscreen onExitFullscreen;


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

        isFullscreen = true; // start "true" so that the fullscreen func can set itself to true on first use
        phoneIsOut = true;
        SetPhoneState(phoneIsOut); // close phone.

        // genuinely don't remember why i put these here
        PhoneMainMenu.onAppOpen += ResetFullscreenVal;
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

        if (EventSystem.current.currentSelectedGameObject == null && !PhoneMainMenu.appIsOpen && 
            (Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow)))
        {
            EventSystem.current.SetSelectedGameObject(mainPhoneButtons[0]);
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
            if (PhoneMainMenu.activeAppScreen != null)
            {
                PhoneMainMenu.activeAppScreen.OnCloseApp();
            }
            ForceFullscreenOff();
            phoneScreen.SetActive(false);
        }

        phoneIsOut = !phoneIsOut; // flip the bool so we know we're in the other state
    }



    void SetFullscreen(bool isFullscreen)
    {
        if (phoneIsOut)
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
             onEnterFullscreen?.Invoke();
        }
        else
        {
            transform.position = regularScreenPos.position;
            transform.rotation = regularScreenPos.rotation;
            //Debug.Log("small");
            //this.isFullscreen = false;
            onExitFullscreen?.Invoke();
        }

        PhoneManager.isFullscreen = !PhoneManager.isFullscreen; // same thing as phone being active
        }
    }

    public void ForceFullscreenOn()
    {
        if (!PhoneMainMenu.activeAppScreen.hasFullscreenAsOption)
        {
            SetFullscreen(true);
        }
    }

    public void ForceFullscreenOff()
    {
        SetFullscreen(false);
    }

    public void ResetFullscreenVal(PhoneAppButton app)
    {
        isFullscreen = true;
    }

}
