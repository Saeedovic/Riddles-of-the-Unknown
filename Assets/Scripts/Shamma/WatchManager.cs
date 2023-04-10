using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchManager : MonoBehaviour
{
    [SerializeField] GameObject watchObj;
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

    void SetWatchState(bool watchActive)
    {
        // when phone is inactive, make it active, and vice versa.
        if (!watchActive)
        {
            watchObj.SetActive(true);
        }
        else
        {
            watchObj.SetActive(false);
        }

        watchIsOut = !watchIsOut; // flip the bool so we know we're in the other state
    }
}
