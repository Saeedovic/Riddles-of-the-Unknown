using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] float rayRange;
    [SerializeField] KeyCode interactKey = KeyCode.E;
    [SerializeField] LayerMask interactablesLayer;
    //[SerializeField] Color highlightColor = Color.yellow;
    [SerializeField] Material highlightMaterial;
    //public AudioClip AudioForGoBackButton;
    //public GameObject SettingsApp;
    //public GameObject GameControlScreen;

    //public Button GameControlScreen;


    public bool interactableAvailable { get; private set; }
    public bool interactionActive { get; private set; } // have these visible for other scripts to be able to
                                                        // do things depending on whether the player's in an interaction.

    public IInteractableObject currentObject { get; private set; }

    [HideInInspector] public XPManager xP;
    [HideInInspector] public InventoryHandler inventoryHandler;


    void Start()
    {
        interactionActive = false;
        interactableAvailable = false;

        xP = GetComponent<XPManager>();
        inventoryHandler = GetComponent<InventoryHandler>();
        //Button btn = GameControlScreen.GetComponent<Button>();

        //btn.onClick.AddListener(ActivateGameControlScreen);
    }

    private void Update()
    {
        //Debug.Log(currentObject);

        if (!interactionActive)
        {
            interactableAvailable = CheckForInteractable();

            if (interactableAvailable
                && Input.GetKeyDown(interactKey))
            {
                interactionActive = true;
                //interactableIsInRange = false;

                currentObject.Interact(this);
                OnObjectDeselect();

                interactionActive = false;
            }
        }
    }


    public bool CheckForInteractable()
    {
        RaycastHit hit;

        Ray checkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red);

        if (Physics.Raycast(checkRay, out hit, rayRange, interactablesLayer))
        {
            IInteractableObject newObject = hit.collider.GetComponent<IInteractableObject>();

            if (newObject == null)
            {
                newObject = hit.collider.GetComponentInParent<IInteractableObject>();
            }

            if (newObject == null)
            {
                newObject = hit.collider.GetComponentInChildren<IInteractableObject>();
            }

            if (newObject.IsInteractable())
            {
                // if we don't have an interactable in sight already, make it this one
                if (currentObject == null)
                {
                    OnObjectSelect(newObject);
                }

                // if we do have an interactable, switch the previous one for the new one
                if (newObject != currentObject)
                {
                    OnObjectDeselect();
                    OnObjectSelect(newObject);
                }

                return true;
            }
            return false;
        }
        else
        {
            if (currentObject != null)
            {
                OnObjectDeselect();
            }
            return false;
        }
    }

    void OnObjectSelect(IInteractableObject newObj)
    {
        currentObject = newObj;
        AddHighlightEffect();
    }

    void OnObjectDeselect()
    {
        RemoveHighlightEffect();
        currentObject = null;
    }

    
    void AddHighlightEffect()
    {
        currentObject.OnHighlight();

        // manually add outline material by creating a new material array and putting outline at the end.
        // do this for every object in the hierarchy.
        Renderer[] meshRenderers = currentObject.gameObject.GetComponentsInChildren<Renderer>();

        if (meshRenderers != null)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                Material[] matArray = new Material[meshRenderers[i].materials.Length + 1];

                for (int j = 0; j < meshRenderers[i].materials.Length; j++)
                {
                    matArray[j] = meshRenderers[i].materials[j];
                }

                matArray[matArray.Length - 1] = highlightMaterial;
                meshRenderers[i].materials = matArray;
            }

        }
    }

    void RemoveHighlightEffect()
    {
        Renderer[] meshRenderers = currentObject.gameObject.GetComponentsInChildren<Renderer>();

        if (meshRenderers != null)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                Material[] matArray = new Material[meshRenderers[i].materials.Length - 1];

                for (int j = 0; j < matArray.Length; j++)
                {
                    matArray[j] = meshRenderers[i].materials[j];
                }

                meshRenderers[i].materials = matArray;
            }

            currentObject.OnDeHighlight();
        }
    }


}
