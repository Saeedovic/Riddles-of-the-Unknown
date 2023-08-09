using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Animations.Rigging;

public class PhoneManager : MonoBehaviour
{
    public static PhoneManager Instance;


    public  GameObject phoneScreen;
    [SerializeField] List<GameObject> mainPhoneButtons; // used to set the input system up

    [SerializeField] public Transform regularScreenPos;
    [SerializeField] Transform fullscreenScreenPos;


    public GameObject playerUI;
    public RectTransform uiCursor;
    GameObject objLastSelected;
    GameObject objCurrentlySelected;


    public AudioClip AudioForOpeningPhone;

    public bool mouseShouldBeUseable = false;
    public static bool isFullscreen { get; private set; }
    public static bool phoneIsOut { get; private set; }
    [HideInInspector] public bool phoneIsUseable;


    [SerializeField] Rig rightHandRig;
    [SerializeField] float rigWeightSmoothVelocity;

    public delegate void OnEnterFullscreen();
    public static OnEnterFullscreen onEnterFullscreen;
    public delegate void OnExitFullscreen();
    public static OnExitFullscreen onExitFullscreen;
    


    void Start()
    {
        Instance = this;
        PhoneMainMenu.InitPhone(phoneScreen); // pass the given phone screen to PhoneMainMenu for it to load app components into its list

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainPhoneButtons[0]);

        phoneIsUseable = true;
        isFullscreen = true; // start "true" so that the fullscreen func can set itself to true on first use
        phoneIsOut = true;
        SetPhoneState(phoneIsOut); // close phone.

        PhoneMainMenu.onAppOpen += ResetFullscreenVal;
        PhoneMainMenu.onAppClose += ForceFullscreenOff; // need to know if we should switch to fullscreen or no
    }


    void Update()
    {
        if (!mouseShouldBeUseable)
        {
            if (phoneIsOut)
            {
                // prevent phone buttons from being unfocused and requiring you close and reopen phone.
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
                {
                    EventSystem.current.SetSelectedGameObject(objLastSelected); 
                }
            }

            // open or close phone
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

            // control fullscreen state
            if (Input.GetKeyDown(KeyCode.P) && phoneIsOut)
            {
                SetFullscreen(isFullscreen);
            }

            // camera app fullscreen should handle its own cursor
            if (!PhoneCameraApp.enteredFullscreen)
            {
                // update cursor pos when selected game obj changes
                if (EventSystem.current.currentSelectedGameObject != null &&
                    EventSystem.current.currentSelectedGameObject != objLastSelected)
                {
                    UpdateCursorPosition();
                }
            }
        }
        //Debug.Log("weight: " + rightHandRig.weight);
    }


    void UpdateCursorPosition()
    {
        objLastSelected = EventSystem.current.currentSelectedGameObject;
        RectTransform selectedTransform = objLastSelected.GetComponent<RectTransform>();


        uiCursor.parent = selectedTransform;
        uiCursor.localPosition = selectedTransform.anchorMax;
    }


    public void SetPhoneState(bool phoneActive)
    {
        // when phone is inactive, make it active, and vice versa.
        if (!phoneActive)
        {
            phoneScreen.SetActive(true);
           // PhoneMainMenu.RefreshPhone();

            StopCoroutine(SetHandForPhoneOff());
            StartCoroutine(SetHandForPhoneOn());
        }
        else
        {
            if (PhoneMainMenu.activeAppScreen != null)
            {
                PhoneMainMenu.activeAppScreen.OnCloseApp();
            }
            ForceFullscreenOff();
            phoneScreen.SetActive(false);

            StopCoroutine(SetHandForPhoneOn());
            StartCoroutine(SetHandForPhoneOff());
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
                
                onEnterFullscreen?.Invoke();
            }
            else
            {
                transform.position = regularScreenPos.position;
                transform.rotation = regularScreenPos.rotation;

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


    IEnumerator SetHandForPhoneOn()
    {
        

        rightHandRig.weight = Mathf.MoveTowards(rightHandRig.weight, 1, rigWeightSmoothVelocity);

        if (rightHandRig.weight == 1)
        {
            yield break;
        }

        yield return null;
        StartCoroutine(SetHandForPhoneOn());
    }
    
    IEnumerator SetHandForPhoneOff()
    {
        

        rightHandRig.weight = Mathf.MoveTowards(rightHandRig.weight, 0, rigWeightSmoothVelocity);

        if (rightHandRig.weight == 0)
        {
            yield break;
        }

        yield return null;
        StartCoroutine(SetHandForPhoneOff());
    }
}
