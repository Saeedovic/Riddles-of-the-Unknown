using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PhoneAppButton : MonoBehaviour
{
    [SerializeField] protected GameObject appScreenObj; // the app that we'll be opening with this button
    protected PhoneAppScreen appScreen;
    protected Button appButton;

    private void Awake()
    {
        //appScreen = transform.GetChild(0).gameObject;
        if (appScreenObj == null)
        {
            Debug.LogWarning($"The screen variable on {this.name} is not set. \n" +
                $"Ensure that you have the corresponding screen to activate set in the App Screen field.");
        }
        else
        {
            appScreen = appScreenObj.GetComponent<PhoneAppScreen>();
        }

        // subscribe our function to the button component's OnClick wihout having to set it via inspector.
        appButton = GetComponent<Button>();
        appButton.onClick.AddListener(OnAppClicked); 
    }

    public void OnAppClicked()
    {
        Debug.Log("you clicked " + name + "!");

        if (!appScreenObj.activeInHierarchy)
        {
            PhoneMainMenu.onAppOpen(this);
        }
        return;
    }

    /*
    public void OnPointerDown(PointerEventData eventData)
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
        appScreenObj.SetActive(true);
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


    // call the onappclose delegate, which will call refreshphone, in turn calling this app's closeapp
    /*public void OnCloseButtonClicked()
    {
        PhoneMainMenu.onAppClose(this);
    }

    public void CloseApp()
    {
        // processes involved in closing.
        // send event to call RefreshPhone
        appScreenObj.SetActive(false);
        
    }*/

}
