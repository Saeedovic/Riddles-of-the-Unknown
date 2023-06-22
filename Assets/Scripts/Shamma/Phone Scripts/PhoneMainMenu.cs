using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class PhoneMainMenu
{
    static List<PhoneAppButton> allAvailableAppButtons; // hold our app buttons in a list. the buttons reference all our app screens
    static List<PhoneGenericObject> allNonAppButtonObjs; // for things on the main menu that aren't app buttons. (minimap, timer)
    static public PhoneAppButton activeApp { get; private set; }
    public static PhoneAppScreen activeAppScreen { get; private set; }


    public static bool appIsOpen;

    public delegate void OnAppOpen(PhoneAppButton app); // event calls for our buttons to use
    public static OnAppOpen onAppOpen;
    public delegate void OnAppClose();
    public static OnAppClose onAppClose;


    // called from the phonemanager on the player.
    public static void InitPhone(GameObject phoneScreenObj)
    {
        // get the apps to switch between
        allAvailableAppButtons = new List<PhoneAppButton>(phoneScreenObj.GetComponentsInChildren<PhoneAppButton>());
        allNonAppButtonObjs = new List<PhoneGenericObject>(phoneScreenObj.GetComponentsInChildren<PhoneGenericObject>());

        // set our events up
        onAppOpen = new OnAppOpen(LoadApp);
        onAppClose = new OnAppClose(RefreshPhone);
    }


    // called when you bring up the phone or close an app.
    public static void RefreshPhone()// PhoneAppButton app)
    {
        // make sure any open app is closed!
        if (activeAppScreen != null)
        {
            //activeAppScreen.OnCloseApp();
            PhoneManager.Instance.playerUI.SetActive(true);
            activeAppScreen.gameObject.SetActive(false);
        }

        // reactivate buttons
        foreach (var appButton in allAvailableAppButtons)
        {
            appButton.gameObject.SetActive(true);
        }
        foreach (var phoneObj in allNonAppButtonObjs)
        {
            phoneObj.gameObject.SetActive(true);
        }

        // reset current selected object for ui navigation
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(allAvailableAppButtons[0].gameObject);

        activeAppScreen = null;
        appIsOpen = false;
    }


    // on app clicked: 
    // set all other apps inactive
    // set the current app active

    static void LoadApp(PhoneAppButton app)
    {
        // close main screen (turn off associated ui)
        // activate ui for the chosen app
        activeApp = app;
        app.StartApp();

        foreach (var appObj in allAvailableAppButtons)
        {
            appObj.gameObject.SetActive(false);
        }
        foreach (var phoneObj in allNonAppButtonObjs)
        {
            phoneObj.gameObject.SetActive(false);
        }

        //app.gameObject.SetActive(true);
        activeAppScreen = activeApp.phoneAppScreen;
        appIsOpen = true;
        //mainScreen.SetActive(false);
    }


    

}
