using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] float rayRange;
    [SerializeField] LayerMask interactablesLayer;
    //[SerializeField] Color highlightColor = Color.yellow;
    [SerializeField] Material highlightMaterial;

    public bool interactableAvailable { get; private set; }
    public bool interactionActive { get; private set; } // have these visible for other scripts to be able to
                                                        // do things depending on whether the player's in an interaction.

    public IInteractableObject currentObject { get; private set; }

    [HideInInspector] public XPManager xP;


    private void Start()
    {
        interactionActive = false;
        interactableAvailable = false;

        xP = GetComponent<XPManager>();
    }

    private void FixedUpdate()
    {
        Debug.Log(currentObject);

        if (!interactionActive)
        {
            interactableAvailable = CheckForInteractable();

            if (interactableAvailable 
                && Input.GetMouseButtonDown(0))
            {
                interactionActive = true;
                //interactableIsInRange = false;

                currentObject.Interact(this);

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
                currentObject = null;
            }
            return false;
        }
    }

    void OnObjectSelect(IInteractableObject newObj)
    {
        currentObject = newObj;
        currentObject.OnHighlight();

        // manually add outline material by creating a new material array and putting outline at the end.
        Renderer meshRenderer = currentObject.gameObject.GetComponent<Renderer>();
        if (meshRenderer != null)
        {
            Material[] matArray = new Material[meshRenderer.materials.Length + 1];

            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                matArray[i] = meshRenderer.materials[i];
            }

            matArray[matArray.Length - 1] = highlightMaterial;
            meshRenderer.materials = matArray;
        }
        
        /*Outline outline = currentObject.gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = highlightColor;
        outline.OutlineWidth = 5f;*/
    }

    void OnObjectDeselect()
    {
        MeshRenderer meshRenderer = currentObject.gameObject.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Material[] matArray = new Material[meshRenderer.materials.Length - 1];

            for (int i = 0; i < matArray.Length; i++)
            {
                matArray[i] = meshRenderer.materials[i];
            }

            meshRenderer.materials = matArray;

            currentObject.OnDeHighlight();
        }

        //Destroy(currentObject.gameObject.GetComponent<Outline>());
    }


}
