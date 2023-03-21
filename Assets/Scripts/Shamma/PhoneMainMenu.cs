using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhoneMainMenu
{
    static List<PhoneApp> allAvailableApps;
    static PhoneApp activeApp;
    static GameObject mainScreen;

    public static bool appIsOpen;

    public delegate void OnAppOpen(PhoneApp app);
    public static OnAppOpen onAppOpen;
    public delegate void OnAppClose(PhoneApp app);
    public static OnAppClose onAppClose;

    public static void InitPhone(GameObject phoneScreenObj)
    {
        mainScreen = phoneScreenObj;
        allAvailableApps = new List<PhoneApp>(phoneScreenObj.GetComponentsInChildren<PhoneApp>());

        onAppOpen = new OnAppOpen(LoadApp);
        onAppClose = new OnAppClose(RefreshPhone);
    }

    // called when you bring up the phone or close an app.
    static void RefreshPhone(PhoneApp app)
    {
        // ensure we're on the main screen:
        // close any potentially still active app
        // bring up the main phone menu

        if (activeApp != null)
        {
            activeApp.CloseApp();
            activeApp = null;
        }

        foreach (var appObj in allAvailableApps)
        {
            appObj.gameObject.SetActive(true);
        }

        appIsOpen = false;
    }

    // two events: app opening and app closing

    // on app clicked: 
    // set all other apps inactive
    // set the current app active

    static void LoadApp(PhoneApp app)
    {
        // close main screen (turn off associated ui)
        // activate ui for the chosen app
        activeApp = app;

        foreach (var appObj in allAvailableApps)
        {
            appObj.gameObject.SetActive(false);
        }

        app.gameObject.SetActive(true);
        app.StartApp();

        appIsOpen = true;
        //mainScreen.SetActive(false);
    }

}
