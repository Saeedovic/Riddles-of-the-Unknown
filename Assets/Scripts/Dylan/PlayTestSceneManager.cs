using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayTestSceneManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public GameObject optionMenuPanel;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
               resolutions[i].height == Screen.height &&
               resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene("Live_Playtest_Build");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_GameLoop");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
       Screen.fullScreen = isFullScreen;
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void CloseOptionMenu()
    {
        optionMenuPanel.SetActive(false);
    }

    public void OpenOptionMenu()
    {
        optionMenuPanel.SetActive(true);
    }

}

