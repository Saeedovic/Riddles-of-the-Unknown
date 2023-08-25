using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TriggerMainMenu : MonoBehaviour
{
    private string MainMenu = "MainMenu";
    VideoPlayer video;

    void Start()
    {
        StartCoroutine(PlayCredits());
    }

    IEnumerator PlayCredits()
    {
        video = GetComponent<VideoPlayer>();
        video.enabled = true;

        video.Play();

        yield return new WaitForSecondsRealtime((float)video.length); // wait till clip's done

        SceneManager.LoadScene(MainMenu);
    }
}
