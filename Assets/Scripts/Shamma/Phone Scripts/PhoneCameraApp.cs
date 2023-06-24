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

    GameObject phoneObject;
    ScanAnimation ecopointScanner;
    Vector3 regularPhonePos;
    Vector3 cameraFullscreenPhonePos;

    public AudioClip AudioForSnapShot;

    public bool pictureTaken;
    public bool enteredFullscreen;
    public bool ecopointScanned;


    //protected override void Start() { hasFullscreenAsOption = false; }

    void Awake()
    {
        phoneObject = GetComponentInParent<PhoneManager>().gameObject;
        ecopointScanner = GetComponentInParent<ScanAnimation>();

        regularPhonePos = phoneObject.transform.position;
        cameraFullscreenPhonePos = new Vector3(regularPhonePos.x, regularPhonePos.y, regularPhonePos.z + 20); // just manually move phone out of the way.

        enteredFullscreen = true;
        ecopointScanned = false;
        fullscreenUI.SetActive(false);
        //ExitFullScreenMode();
    }

    void Update()
    {
        if (!PhoneManager.isFullscreen && !enteredFullscreen)
        {
            EnterFullscreenMode();
        }

        if (PhoneManager.isFullscreen && enteredFullscreen)
        {
            ExitFullScreenMode();
        }
    }

    public override void OnOpenApp()
    {
        base.OnOpenApp();
        picTakingButton.onClick.AddListener(TakeSnapshot);
    }

    public override void OnCloseApp()
    {
        if (enteredFullscreen)
        {
            ExitFullScreenMode();
        }

        picTakingButton?.onClick.RemoveListener(TakeSnapshot);
        base.OnCloseApp();
    }

    void TakeSnapshot()
    {
        AudioSource.PlayClipAtPoint(AudioForSnapShot, transform.position);

        lastPicTaken.texture = toTexture2D(cameraDisplayTexture);

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

    void EnterFullscreenMode()
    {
        enteredFullscreen = true;
        fullscreenUI.SetActive(true);

        // set up all of the button configurations and their function calls
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(picTakingButtonInFullscreen.gameObject);

        picTakingButton.onClick.RemoveAllListeners();
        picTakingButtonInFullscreen.onClick.AddListener(TakeSnapshotFullscreen);

        ecopointButton.onClick.AddListener(ExecuteEcopoint);

        backButton.onClick.RemoveAllListeners();
        backButtonInFullscreen.onClick.AddListener(OnCloseApp);


        lastPicTakenInFullscreen.texture = lastPicTaken.texture;

        phoneObject.transform.position = cameraFullscreenPhonePos;
    }

    void ExitFullScreenMode()
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
    }

    void TakeSnapshotFullscreen()
    {
        AudioSource.PlayClipAtPoint(AudioForSnapShot, transform.position);

        lastPicTakenInFullscreen.texture = toTexture2D(cameraDisplayTexture);

        pictureTaken = true;

        StartCoroutine(TakeSnapShotTimer());
    }

    void ExecuteEcopoint()
    {
        ecopointScanner.InputHandle();
        StartCoroutine(EcoPointBoolCheck());
    }

}
