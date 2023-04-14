using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirstSystem : MonoBehaviour
{

    public float maxThirst = 100f;
    public float decreaseAmount = 1f;
    public float refillAmount = 100f;
    public float interactionDistance = 2f;
    public LayerMask interactionLayer;
    private QuestManager questManager;

    public float currentThirst;
    public Slider thirstBar;
    public Slider secondThirstBar;
    public bool drankwater;

    public AudioClip AudioForDrinking;
    public AudioClip AudioForFillingwater;



    public void Start()
    {
        currentThirst = maxThirst;
        drankwater = false;
        questManager = GetComponent<QuestManager>();
        if (questManager == null)
        {
            Debug.Log("QuestManager not found!");
        }
        InvokeRepeating("DecreaseThirst", 5f, 5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))
        {
            Interact();
        }
    }

    void DecreaseThirst()
    {
        currentThirst -= decreaseAmount;
        UpdateThirstBar();
        Debug.Log("Current thirst level: " + currentThirst);
    }

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayer))
        {
            if (hit.collider.CompareTag("Water"))
            {
                RefillThirst();
                AudioSource.PlayClipAtPoint(AudioForDrinking, transform.position);

                Destroy(hit.collider.gameObject);
                questManager.CompleteCurrentQuest();
                drankwater = true;

            }
        }
    }

    public void RefillThirst()
    {
        currentThirst = Mathf.Min(currentThirst + refillAmount, maxThirst);

        UpdateThirstBar();
        AudioSource.PlayClipAtPoint(AudioForFillingwater, transform.position);
        Debug.Log("Current thirst level: " + currentThirst);
    }

    void UpdateThirstBar()
    {
        thirstBar.value = currentThirst;

        if (secondThirstBar != null)
        {
            secondThirstBar.value = currentThirst;
        }
    }
}