using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneAppButton : MonoBehaviour
{
    [SerializeField] PhoneAppScreen appScreen;
    public PhoneAppScreen phoneAppScreen { get { return appScreen; } }
    Button appButton;

    public AudioClip AudioForOpeningPhoneApp;


    private void Awake()
    {
        if (appScreen == null)
        {
            Debug.LogError($"The screen variable on {this.name} is not set. \n" +
            $"Please ensure that you have the corresponding screen to activate set in the App Screen field.");
        }

        appButton = GetComponent<Button>();
        appButton.onClick.AddListener(OnAppClicked);
    }

    public void OnAppClicked()
    {
        //Debug.Log("you clicked " + name + "!");

        if (!appScreen.gameObject.activeInHierarchy)
        {
            AudioSource.PlayClipAtPoint(AudioForOpeningPhoneApp, transform.position);

            PhoneMainMenu.onAppOpen(this);
        }
        return;
    }

    /*
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("you clicked " + name + "!");

        if (!appScreen.activeInHierarchy)
        {
            PhoneMainMenu.onAppOpen(this);
        }
        return;


    }
    */

    public void StartApp()
    {
        //menu = mainMenu;
        //menu.gameObject.SetActive(false);
        // pull up the relevant ui and other objs to activate.
        appScreen.gameObject.SetActive(true);
        appScreen.OnOpenApp();

        /*
        // some test code
        if (!appScreen.activeInHierarchy && !PhoneMainMenu.appIsOpen)
        {
            appScreen.SetActive(true);
            //appButton.SetActive(false);
            PhoneMainMenu.appIsOpen = true;
        }
        else// if (appScreen.activeInHierarchy) 
        {
            appScreen.SetActive(false);
            //appButton.SetActive(true);
            PhoneMainMenu.appIsOpen = false;
        }*/
        //eventData.
    }

    /*
    public void OnCloseButtonClicked()
    {
        PhoneMainMenu.onAppClose(this);
    }

    public void CloseApp()
    {
        // processes involved in closing.
        // send event to call RefreshPhone
        appScreen.SetActive(false);

    }*/

}


    