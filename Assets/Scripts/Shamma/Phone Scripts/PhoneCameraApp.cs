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
    [SerializeField] GameObject fullscreenUI;

    GameObject phoneObject;
    ScanAnimation ecopointScanner;
    Vector3 regularPhonePos;
    Vector3 cameraFullscreenPhonePos;

    public AudioClip AudioForSnapShot;

    public bool pictureTaken;
    bool enteredFullscreen;


    //protected override void Start() { hasFullscreenAsOption = false; }

    void Awake()
    {
        phoneObject = GetComponentInParent<PhoneManager>().gameObject;
        ecopointScanner = GetComponentInParent<ScanAnimation>();

        regularPhonePos = phoneObject.transform.position;
        cameraFullscreenPhonePos = new Vector3(regularPhonePos.x, regularPhonePos.y, regularPhonePos.z + 20); // just manually move phone out of the way.

        fullscreenUI.SetActive(false);
    }

    void Update()
    {
        if (PhoneManager.isFullscreen && !enteredFullscreen)
        {
            EnterFullscreenMode();
        }

        if (!PhoneManager.isFullscreen && enteredFullscreen)
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
        base.OnCloseApp();
        picTakingButton?.onClick.RemoveListener(TakeSnapshot);
    }

    void TakeSnapshot()
    {
        AudioSource.PlayClipAtPoint(AudioForSnapShot, transform.position);

        lastPicTaken.texture = toTexture2D(cameraDisplayTexture);

        pictureTaken = true;

        StartCoroutine(TakeSnapShotTimer());

    }

    // courtesy of stackoverflow. 
    // create a new texture of our rendertexture's size, then scan each of rendertexture's pixels and set our texture to them.
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

    void EnterFullscreenMode()
    {
        fullscreenUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(picTakingButtonInFullscreen.gameObject);

        picTakingButton.onClick.RemoveAllListeners();
        picTakingButtonInFullscreen.onClick.AddListener(TakeSnapshotFullscreen);

        ecopointButton.onClick.AddListener(ExecuteEcopoint);

        lastPicTakenInFullscreen = lastPicTaken;
    }

    void ExitFullScreenMode()
    {
        fullscreenUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(picTakingButton.gameObject);

        picTakingButtonInFullscreen.onClick.RemoveAllListeners();
        picTakingButton.onClick.AddListener(TakeSnapshot);

        ecopointButton.onClick.RemoveAllListeners();

        lastPicTaken = lastPicTakenInFullscreen;
    }

    void TakeSnapshotFullscreen()
    {
        AudioSource.PlayClipAtPoint(AudioForSnapShot, transform.position);

        lastPicTaken.texture = toTexture2D(cameraDisplayTexture);

        pictureTaken = true;

        StartCoroutine(TakeSnapShotTimer());
    }

    void ExecuteEcopoint()
    {
        ecopointScanner.InputHandle();
    }

}
