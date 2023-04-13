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

    protected virtual void Start()
    {
        hasFullscreenAsOption = true;
    }

    public virtual void OnOpenApp()
    {
        //PhoneMainMenu.onAppClose += OnCloseApp;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstHighlightedButton);

        PhoneManager.Instance.playerUI.SetActive(false);

        backButton.onClick.AddListener(OnCloseApp);
    }

    public virtual void OnCloseApp()
    {
        PhoneManager.Instance.playerUI.SetActive(true);


        PhoneMainMenu.onAppClose();
        gameObject.SetActive(false);
    }

}
