using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class PhoneAppScreen : MonoBehaviour
{
    [SerializeField] protected GameObject firstHighlightedButton;
    [SerializeField] protected Button backButton;

    public virtual void OnOpenApp()
    {
        //PhoneMainMenu.onAppClose += OnCloseApp;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstHighlightedButton);

        backButton.onClick.AddListener(OnCloseApp);
    }

    public virtual void OnCloseApp()
    {
        PhoneMainMenu.onAppClose();
        gameObject.SetActive(false);
    }

}
