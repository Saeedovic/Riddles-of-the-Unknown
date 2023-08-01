using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class PhoneAppScreen : MonoBehaviour
{
    [SerializeField] protected GameObject firstHighlightedButton;
    [SerializeField] protected Button backButton;

    PhoneManager phoneManager;
    public bool hasFullscreenAsOption { get; protected set; }

    public virtual void Start()
    {
        hasFullscreenAsOption = true;
    }

    public virtual void OnOpenApp()
    {
        backButton.onClick.RemoveAllListeners();

        //PhoneMainMenu.onAppClose += OnCloseApp;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstHighlightedButton); // make sure new UI screen is selected properly

        PhoneManager.Instance.playerUI.SetActive(false); // switch off stats so they don't cover the app screen

        backButton.onClick.AddListener(OnCloseApp); // make it so back button will automatically close, only setup needed is putting a var in the editor field.
    }

    public virtual void OnCloseApp()
    {
        PhoneManager.Instance.playerUI.SetActive(true); // reactivate stats

        //backButton?.onClick.RemoveListener(OnCloseApp);

        PhoneMainMenu.onAppClose(); // notify that this app is now closed
        gameObject.SetActive(false);
    }

}
