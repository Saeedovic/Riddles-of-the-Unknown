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
    private QuestManager questManager;
    public bool ate;
    public float currentHunger;
    public Slider hungerBar;
    public Slider secondHungerBar;

    public void Start()
    {
        currentHunger = maxHunger;
        questManager = GetComponent<QuestManager>();
        InvokeRepeating("DecreaseHunger", 5f, 5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!ate)
            {
                Interact();
            }
            else // the player can eat after the quest is completed 
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayer))
                {
                    if (hit.collider.CompareTag("Food"))
                    {
                        RefillHunger();
                        Destroy(hit.collider.gameObject);
                        questManager.CompleteCurrentQuest();
                        ate = true;

                    }
                }
            }

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
                if (questManager != null && questManager.quests[questManager.currentQuestIndex] == "Find some food")
                {
                    RefillHunger();
                    Destroy(hit.collider.gameObject);
                    questManager.CompleteCurrentQuest();
                    ate = true;
                }
            }
        }
    }

    public void RefillHunger()
    {
        currentHunger = Mathf.Min(currentHunger + refillAmount, maxHunger);
        UpdateHungerBar();
        Debug.Log("Current hunger level: " + currentHunger);
    }

    void UpdateHungerBar()
    {
        hungerBar.value = currentHunger;

        if (secondHungerBar != null)
        {
            secondHungerBar.value = currentHunger;
        }
    }
}