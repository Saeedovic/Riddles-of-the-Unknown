using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using OD.Effect.HDRP;

public class PhoneCameraApp : PhoneAppScreen
{
    [SerializeField] RenderTexture cameraDisplayTexture;
    [SerializeField] RawImage lastPicTaken;
    [SerializeField] RawImage lastPicTakenInFullscreen;

    [SerializeField] Button picTakingButton;
    [SerializeField] Button picTakingButtonInFullscreen;

    [SerializeField] Button ecopointButton;
    [SerializeField] Button backButtonInFullscreen;

    [SerializeField] GameObject fullscreenUI;

    [SerializeField] RectTransform uiCursor;
    GameObject objLastSelected;


    GameObject phoneObject;
    ScanAnimation ecopointScanner;
    Vector3 regularPhonePos;
    Vector3 cameraFullscreenPhonePos;

    public AudioClip AudioForSnapShot;

    public bool pictureTaken;
    public static bool enteredFullscreen { get; private set; }
    public bool ecopointScanned;

    [SerializeField] SkinnedMeshRenderer[] playerModel;
    [SerializeField] MeshRenderer phoneModel;

    public delegate void OnFullscreening();
    public static OnFullscreening onFullscreenEntered;
    public static OnFullscreening onFullscreenExited;


    //protected override void Start() { hasFullscreenAsOption = false; }

    void Awake()
    {
        phoneObject = GetComponentInParent<PhoneManager>().gameObject;
        ecopointScanner = GetComponentInParent<ScanAnimation>();

        regularPhonePos = phoneObject.transform.position;
        cameraFullscreenPhonePos = new Vector3(regularPhonePos.x, regularPhonePos.y, regularPhonePos.z + 20); // just manually move phone out of the way.

        enteredFullscreen = false;
        ecopointScanned = false;
        fullscreenUI.SetActive(false);
        //InitiateExitFullScreenMode();
    }

    protected void Update()
    {
        // update the camera's own cursor pos when selected game obj changes
        if (enteredFullscreen)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                EventSystem.current.SetSelectedGameObject(objLastSelected);
            }

            if (EventSystem.current.currentSelectedGameObject != null &&
                EventSystem.current.currentSelectedGameObject != objLastSelected)
            {
                UpdateCursorPosition();
            }
        }
    }

    void UpdateCursorPosition()
    {
        objLastSelected = EventSystem.current.currentSelectedGameObject;
        RectTransform selectedTransform = objLastSelected.GetComponent<RectTransform>();


        uiCursor.parent = selectedTransform;
        uiCursor.localPosition = selectedTransform.anchorMax;
    }



    public override void OnOpenApp()
    {
        base.OnOpenApp();

        picTakingButton.onClick.AddListener(TakeSnapshot);
        PhoneManager.onEnterFullscreen += InitiateEnterFullscreenMode;
        PhoneManager.onExitFullscreen += InitiateExitFullScreenMode;

        PhoneManager.Instance.cameraAppOpen = true;
    }

    public override void OnCloseApp()
    {
        if (enteredFullscreen)
        {
            InitiateExitFullScreenMode();
            PhoneManager.Instance.StartCoroutine(PhoneManager.Instance.ExitBlackoutTransition());
        }

        PhoneManager.onEnterFullscreen -= InitiateEnterFullscreenMode;
        PhoneManager.onExitFullscreen -= InitiateExitFullScreenMode;
        picTakingButton?.onClick.RemoveListener(TakeSnapshot);

        PhoneManager.Instance.cameraAppOpen = false; 

        base.OnCloseApp();
    }

    void TakeSnapshot()
    {
        AudioSource.PlayClipAtPoint(AudioForSnapShot, transform.position);

        lastPicTaken.texture = toTexture2D(cameraDisplayTexture);

        pictureTaken = true;

        StartCoroutine(TakeSnapShotTimer());
    }


    void TakeSnapshotFullscreen()
    {
        AudioSource.PlayClipAtPoint(AudioForSnapShot, transform.position);

        lastPicTakenInFullscreen.texture = toTexture2D(cameraDisplayTexture);

        pictureTaken = true;

        StartCoroutine(TakeSnapShotTimer());
    }

    // courtesy of stackoverflow. 
    // create a new texture of our rendertexture's size, then take each of the
    // rendertexture's pixels and set our texture to them, via texture2d funcs.
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);

        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        
        return tex;
    }

    IEnumerator TakeSnapShotTimer()
    {
        yield return new WaitForSeconds(2);

        pictureTaken = false;
    }

     IEnumerator EcoPointBoolCheck()
     {
        ecopointScanned = true;
        yield return new WaitForSeconds(2);
        ecopointScanned = false;

     }

    void ExecuteEcopoint()
    {
        ecopointScanner.InputHandle();
        StartCoroutine(EcoPointBoolCheck());
    }

    void InitiateEnterFullscreenMode()
    {
        // need to let animation play out before we start up fullscreen
        BlackoutEffect.Instance.StartCoroutine(BlackoutEffect.Instance.FadeInToBlack());
       // PhoneManager.Instance.phoneIsUseable = false;

        StartCoroutine(EnteringFullscreenTransition());
    }

    void InitiateExitFullScreenMode()
    {
        BlackoutEffect.Instance.StartCoroutine(BlackoutEffect.Instance.FadeInToBlack());
       // PhoneManager.Instance.phoneIsUseable = false;

        PhoneManager.Instance.StartCoroutine(ExitingFullscreenTransition());
        Debug.Log("one");
    }

    // start transition back out, wait for completion
    IEnumerator EnteringFullscreenTransition()
    {
        if (BlackoutEffect.Instance.screenIsBlack)
        {
            StartCoroutine(BlackoutEffect.Instance.FadeOutOfBlack());
            ProcessEnteringFullscreen();
            yield break;
        }

        yield return null;
        StartCoroutine(EnteringFullscreenTransition());
    }

    IEnumerator ExitingFullscreenTransition()
    {
        if (BlackoutEffect.Instance.screenIsBlack)
        {
            StartCoroutine(BlackoutEffect.Instance.FadeOutOfBlack());
            ProcessExitingFullscreen();
            Debug.Log("two B");
            yield break;
        }
        Debug.Log("two A");
        yield return null;
        PhoneManager.Instance.StartCoroutine(ExitingFullscreenTransition());
    }


    void ProcessEnteringFullscreen()
    {
        enteredFullscreen = true;
        fullscreenUI.SetActive(true);

        // set up all of the button configurations and their function calls
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ecopointButton.gameObject);

        picTakingButton.onClick.RemoveAllListeners();
        picTakingButtonInFullscreen.onClick.AddListener(TakeSnapshotFullscreen);

        ecopointButton.onClick.AddListener(ExecuteEcopoint);

        backButton.onClick.RemoveAllListeners();
        backButtonInFullscreen.onClick.AddListener(OnCloseApp);


        lastPicTakenInFullscreen.texture = lastPicTaken.texture;

        phoneObject.transform.position = cameraFullscreenPhonePos;

        for (int i = 0; i < playerModel.Length; i++)
        {
            playerModel[i].enabled = false;
        }
        phoneModel.enabled = false;

       // PhoneManager.Instance.phoneIsUseable = true;

        onFullscreenEntered?.Invoke();
    }


    public void ProcessExitingFullscreen()
    {
        enteredFullscreen = false;
        fullscreenUI.SetActive(false);


        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(picTakingButton.gameObject);

        picTakingButtonInFullscreen.onClick.RemoveAllListeners();
        picTakingButton.onClick.AddListener(TakeSnapshot);

        ecopointButton.onClick.RemoveAllListeners();

        backButtonInFullscreen.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(OnCloseApp);


        lastPicTaken.texture = lastPicTakenInFullscreen.texture;

        phoneObject.transform.position = PhoneManager.Instance.regularScreenPos.position;

        for (int i = 0; i < playerModel.Length; i++)
        {
            playerModel[i].enabled = true;
        }
        phoneModel.enabled = true;

        //PhoneManager.Instance.phoneIsUseable = true;
        Debug.Log("three");
        onFullscreenExited?.Invoke();
    }


}
