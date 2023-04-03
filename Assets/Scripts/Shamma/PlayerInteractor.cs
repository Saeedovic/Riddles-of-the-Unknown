using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] float rayRange;
    [SerializeField] LayerMask interactablesLayer;
    [SerializeField] Color highlightColor = Color.yellow;

    public bool interactableIsInRange { get; private set; }
    public bool interactionActive { get; private set; } // have these visible for other scripts to be able to
                                                        // do things depending on whether the player's in an interaction.

    IInteractableObject currentObject;

    private void Start()
    {
        interactionActive = false;
        interactableIsInRange = false;
    }

    private void FixedUpdate()
    {
        Debug.Log(currentObject);

        if (!interactionActive)
        {
            interactableIsInRange = CheckForInteractable();

            if (interactableIsInRange 
                && Input.GetMouseButtonDown(0))
            {
                interactionActive = true;
                //interactableIsInRange = false;

                currentObject.Interact(this.gameObject);

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

        
        Outline outline = currentObject.gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = highlightColor;
        outline.OutlineWidth = 5f;
    }

    void OnObjectDeselect()
    {
        currentObject.OnDeHighlight();

        Destroy(currentObject.gameObject.GetComponent<Outline>());
    }


}
