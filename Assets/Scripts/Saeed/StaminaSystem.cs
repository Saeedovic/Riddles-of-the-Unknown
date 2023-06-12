using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
   // public float maxStamina = 100f;
    public float staminaDecreaseRate = 10f;
    public float staminaIncreaseRate = 20f;

   // public float currentStamina;
    private CharacterController controller;
    public Slider staminaBar;
    public Slider secondStaminaBar;
    private PlayerCon playerCon;

   public DisplayStats stats;


    public void Start()
    {
        stats.currentStamina = stats.maxStamina;
        controller = GetComponent<CharacterController>();
        //staminaBar = FindObjectOfType<Slider>();
        playerCon = GetComponent<PlayerCon>();
        UpdateStaminaBar();
    }

    private void Update()
    {
        bool isRunning = playerCon.isRunning;

        if (isRunning && stats.currentStamina > 0f)
        {
            stats.currentStamina -= staminaDecreaseRate * Time.deltaTime;
            if (stats.currentStamina <= 0f)
            {
                stats.currentStamina = 0f;
                isRunning = false;
            }
            UpdateStaminaBar();
        }
        else if (!isRunning && stats.currentStamina < stats.maxStamina)
        {
            stats.currentStamina += staminaIncreaseRate * Time.deltaTime;
            if (stats.currentStamina > stats.maxStamina)
            {
                stats.currentStamina = stats.maxStamina;
            }
            UpdateStaminaBar();
        }

        if (isRunning && stats.currentStamina > 0f)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            controller.Move(move * Time.deltaTime);

            stats.currentStamina -= move.magnitude * staminaDecreaseRate * Time.deltaTime;

            if (stats.currentStamina <= 0f)
            {
                stats.currentStamina = 0f;
                isRunning = false;
            }

            UpdateStaminaBar();
        }

        //Debug.Log("current stamina:" + currentStamina);
    }

    public void UpdateStaminaBar()
    {
        staminaBar.value = stats.currentStamina;

        // also update second bar if we have one. (used for the watch)
        if (secondStaminaBar != null)
        {
            secondStaminaBar.value = stats.currentStamina;
        }
    }
}