using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class PhoneApp : MonoBehaviour, IPointerDownHandler
{
    protected GameObject appScreen;
    protected GameObject appButton;

    private void Awake()
    {
        appScreen = transform.GetChild(0).gameObject;
    }

    #region 
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("you clicked " + name + "!");

        if (!appScreen.activeInHierarchy)
        {
            PhoneMainMenu.onAppOpen(this);
        }
        return;
        

    }
    #endregion

    public void StartApp()
    {
        //menu = mainMenu;
        //menu.gameObject.SetActive(false);
        // pull up the relevant ui and other objs to activate.
        appScreen.SetActive(true);

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



    public void OnCloseButtonClicked()
    {
        PhoneMainMenu.onAppClose(this);
    }

    public void CloseApp()
    {
        // processes involved in closing.
        // send event to call RefreshPhone
        appScreen.SetActive(false);
        
    }

}
