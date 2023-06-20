using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchManager : MonoBehaviour
{
    [SerializeField] GameObject watchObj;
    public AudioClip AudioForWatch;

    public static bool watchIsOut { get; private set; }

    void Start()
    {
        watchIsOut = true;
        SetWatchState(watchIsOut); // close phone.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SetWatchState(watchIsOut);
        }
    }

    public void SetWatchState(bool watchActive)
    {
        // when phone is inactive, make it active, and vice versa.
        if (!watchActive)
        {
            AudioSource.PlayClipAtPoint(AudioForWatch, transform.position);
            watchObj.SetActive(true);
        }
        else
        {
            AudioSource.PlayClipAtPoint(AudioForWatch, transform.position);
            watchObj.SetActive(false);
        }

        watchIsOut = !watchIsOut; // flip the bool so we know we're in the other state
    }
}
