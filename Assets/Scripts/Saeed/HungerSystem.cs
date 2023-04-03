using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerSystem : MonoBehaviour
{

    public float maxHunger = 100f;
    public float decreaseAmount = 1f;
    public float refillAmount = 100f;
    public float interactionDistance = 2f;
    public LayerMask interactionLayer;

    public float currentHunger;
    public Slider hungerBar;

    void Start()
    {
        currentHunger = maxHunger;
        InvokeRepeating("DecreaseHunger", 5f, 5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void DecreaseHunger()
    {
        currentHunger -= decreaseAmount;
        UpdateHungerBar();
        Debug.Log("Current hunger level: " + currentHunger);
    }

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayer))
        {
            if (hit.collider.CompareTag("Food"))
            {
                RefillHunger();
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void RefillHunger()
    {
        currentHunger = Mathf.Min(currentHunger + refillAmount, maxHunger);
        UpdateHungerBar();
        Debug.Log("Current hunger level: " + currentHunger);
    }

    void UpdateHungerBar()
    {
        hungerBar.value = currentHunger;
    }
}