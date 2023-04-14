using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PhoneSettingsApp : PhoneAppScreen
{
    public GameObject MainSettingsApp;
    public GameObject GameControlScreen;
    public GameObject gameControlsBackButton;
    public AudioClip AudioForGoBackButton;


    public override void OnOpenApp()
    {
        MainSettingsApp.SetActive(true);
        base.OnOpenApp();
    }

    // for button in main settings to call to switch to the game controls screen.
    public void ActivateGameControlScreen()
    {
        AudioSource.PlayClipAtPoint(AudioForGoBackButton, transform.position);

        GameControlScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameControlsBackButton);

        MainSettingsApp.SetActive(false);
    }

    public void BackButton()
    {
        AudioSource.PlayClipAtPoint(AudioForGoBackButton, transform.position);
    }

    // for back button in the game controls screen to call to switch back to main settings.
    public void BackButtonGameControls()
    {
        AudioSource.PlayClipAtPoint(AudioForGoBackButton, transform.position);

        MainSettingsApp.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstHighlightedButton); // make sure button is selected!

        GameControlScreen.SetActive(false);
    }
}
