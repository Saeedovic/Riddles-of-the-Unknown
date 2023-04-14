using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PhoneCameraApp : PhoneAppScreen
{
    [SerializeField] RenderTexture cameraDisplayTexture;
    [SerializeField] RawImage lastPicTaken;
    [SerializeField] Button picTakingButton;
    public AudioClip AudioForSnapShot;



    protected override void Start() { hasFullscreenAsOption = false; }

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

}
