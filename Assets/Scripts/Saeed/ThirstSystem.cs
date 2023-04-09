using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirstSystem : MonoBehaviour
{

    public float maxThirst = 100f;
    public float decreaseAmount = 1f;
    public float refillAmount = 100f;
    //public float interactionDistance = 2f;
    //public LayerMask interactionLayer;

    public float currentThirst;
    public Slider thirstBar;

    void Start()
    {
        currentThirst = maxThirst;
        InvokeRepeating("DecreaseThirst", 5f, 5f);
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }*/

    void DecreaseThirst()
    {
        currentThirst -= decreaseAmount;
        UpdateThirstBar();
        Debug.Log("Current thirst level: " + currentThirst);
    }

    /*void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayer))
        {
            if (hit.collider.CompareTag("Water"))
            {
                RefillThirst();
                Destroy(hit.collider.gameObject);
            }
        }
    }*/

    public void RefillThirst()
    {
        currentThirst = Mathf.Min(currentThirst + refillAmount, maxThirst);
        UpdateThirstBar();
        Debug.Log("Current thirst level: " + currentThirst);
    }

    void UpdateThirstBar()
    {
        thirstBar.value = currentThirst;

    }
}