using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For any interactable objects to inherit from in order for the player to use them.
/// </summary>
public interface IInteractableObject
{
    public void Interact(GameObject user); // self explanatory. use the player passed in if needed
    public void OnHighlight();
    public void OnDeHighlight();

    public bool IsInteractable(); // add code here if you want conditions for the player to use this item,
                                  // otherwise just return true :P
}
