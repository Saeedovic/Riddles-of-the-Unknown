using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] PlayerInteractor interactor;
    [SerializeField] GameObject promptBox;
    [SerializeField] TextMeshProUGUI promptText;


    void Update()
    {
        if (interactor.interactableAvailable && interactor.currentObject != null)
        {
            promptBox.SetActive(true);
            promptText.text = "Press E to use " + interactor.currentObject.gameObject.name;
        }
        else
        {
            promptBox.SetActive(false);
            promptText.text = "";
        }
    }
}
