using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 100f;
    public float staminaDecreaseRate = 10f;
    public float staminaIncreaseRate = 20f;

    public float currentStamina;
    private CharacterController controller;
    public Slider staminaBar;
    private PlayerCon playerCon;

    private void Start()
    {
        currentStamina = maxStamina;
        controller = GetComponent<CharacterController>();
        //staminaBar = FindObjectOfType<Slider>();
        playerCon = GetComponent<PlayerCon>();
        UpdateStaminaBar();
    }

    private void Update()
    {
        bool isRunning = playerCon.isRunning;

        if (isRunning && currentStamina > 0f)
        {
            currentStamina -= staminaDecreaseRate * Time.deltaTime;
            if (currentStamina <= 0f)
            {
                currentStamina = 0f;
                isRunning = false;
            }
            UpdateStaminaBar();
        }
        else if (!isRunning && currentStamina < maxStamina)
        {
            currentStamina += staminaIncreaseRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            UpdateStaminaBar();
        }

        if (isRunning && currentStamina > 0f)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            controller.Move(move * Time.deltaTime);

            currentStamina -= move.magnitude * staminaDecreaseRate * Time.deltaTime;

            if (currentStamina <= 0f)
            {
                currentStamina = 0f;
                isRunning = false;
            }

            UpdateStaminaBar();
        }

        Debug.Log("current stamina:" + currentStamina);
    }

    public void UpdateStaminaBar()
    {
        staminaBar.value = currentStamina;
    }
}