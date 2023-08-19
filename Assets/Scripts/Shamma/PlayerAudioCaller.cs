using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for executing player audio calls, ensuring player audio doesn't cut itself off.
public class PlayerAudioCaller : MonoBehaviour
{
    public static bool isPlaying {  get; private set; } 

    public static PlayerAudioCaller Instance;

    // singleton pattern
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // to be called by any scripts that trigger player dialogue.
    // here to make calling the func a bit smoother than always calling startcoroutine
    public void PlayAudio(AudioClip clip, AudioSource player)
    {
        StartCoroutine(PlayWhenReady(clip, player));
    }

    // keep waiting to play till the current audio clip has finished
    IEnumerator PlayWhenReady(AudioClip clip, AudioSource player)
    {
        if (!player.isPlaying)// && !isPlaying)
        {
            isPlaying = true;
            player.clip = clip;
            player.Play();

            yield return new WaitForSeconds(clip.length);

            isPlaying = false;
            yield break;
        }

        yield return null;
        StartCoroutine(PlayWhenReady(clip, player));
    }
}
