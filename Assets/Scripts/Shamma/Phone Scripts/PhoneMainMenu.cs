using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class PhoneMainMenu
{
    static List<PhoneAppButton> allAvailableApps;
    static List<GameObject> appButtons;
    static public PhoneAppButton activeApp { get; private set; }
    static GameObject mainScreen;

    public static bool appIsOpen;

    public delegate void OnAppOpen(PhoneAppButton app);
    public static OnAppOpen onAppOpen;
    public delegate void OnAppClose();// PhoneAppButton app);
    public static OnAppClose onAppClose;

    public static void InitPhone(GameObject phoneScreenObj)
    {
        // get the apps to switch between
        mainScreen = phoneScreenObj;
        allAvailableApps = new List<PhoneAppButton>(phoneScreenObj.GetComponentsInChildren<PhoneAppButton>());

        // set our events up
        onAppOpen = new OnAppOpen(LoadApp);
        onAppClose = new OnAppClose(RefreshPhone);
    }

    // called when you bring up the phone or close an app.
    static void RefreshPhone()// PhoneAppButton app)
    {
        // ensure we're on the main screen:
        // close any potentially still active app
        // bring up the main phone menu



        //activeApp.CloseApp();

        /*if (activeApp != null)
        {
            activeApp.CloseApp();
            activeApp = null;
        }*/

        foreach (var appObj in allAvailableApps)
        {
            appObj.gameObject.SetActive(true);
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(activeApp.gameObject);

        appIsOpen = false;
    }

    // two events: app opening and app closing

    // on app clicked: 
    // set all other apps inactive
    // set the current app active

    static void LoadApp(PhoneAppButton app)
    {
        // close main screen (turn off associated ui)
        // activate ui for the chosen app
        activeApp = app;
        app.StartApp();

        foreach (var appObj in allAvailableApps)
        {
            appObj.gameObject.SetActive(false);
        }

        //app.gameObject.SetActive(true);

        appIsOpen = true;
        //mainScreen.SetActive(false);
    }

}
