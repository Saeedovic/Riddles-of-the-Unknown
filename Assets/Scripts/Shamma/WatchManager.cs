using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class WatchManager : MonoBehaviour
{
    [SerializeField] GameObject watchObj;
    public AudioClip AudioForWatch;

    [SerializeField] Rig leftHandRig;
    [SerializeField] float rigWeightSmoothVelocity = 0.5f;

    public static bool watchIsOut { get; private set; }
    public static bool watchShouldBeUseable = true;

  //  public AudioSource playerAudio;
  //  public AudioClip AudioClipForWatch;

    public bool soundPlayed;


    void Start()
    {
        watchIsOut = true;
        SetWatchState(watchIsOut); // close watch.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && watchShouldBeUseable)
        {
            SetWatchState(watchIsOut);
        }
    }

    public void SetWatchState(bool watchActive)
    {
        // when watch is inactive, make it active, and vice versa.
        if (!watchActive)
        {
            //AudioSource.PlayClipAtPoint(AudioForWatch, transform.position);
            //watchObj.SetActive(true);

            StopCoroutine(SetHandForWatchOff());
            StartCoroutine(SetHandForWatchOn());
        }
        else
        {
           // AudioSource.PlayClipAtPoint(AudioForWatch, transform.position);
            //watchObj.SetActive(false);

            StopCoroutine(SetHandForWatchOn());
            StartCoroutine(SetHandForWatchOff());
        }

        watchIsOut = !watchIsOut; // flip the bool so we know we're in the other state
    }


    IEnumerator SetHandForWatchOn()
    {


        leftHandRig.weight = Mathf.MoveTowards(leftHandRig.weight, 1, rigWeightSmoothVelocity);

        if (leftHandRig.weight == 1)
        {
            yield break;
        }

        yield return null;
        StartCoroutine(SetHandForWatchOn());
    }

    IEnumerator SetHandForWatchOff()
    {


        leftHandRig.weight = Mathf.MoveTowards(leftHandRig.weight, 0, rigWeightSmoothVelocity);

        if (leftHandRig.weight == 0)
        {
            yield break;
        }

        yield return null;
        StartCoroutine(SetHandForWatchOff());
    }
}
