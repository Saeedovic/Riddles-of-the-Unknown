using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public float sprintSpeedMultiplier = 2f;
    public float staminaReductionRate = 10f;
    public float staminaRegenerationRate = 10f;
    public Slider staminaSlider;

    private bool isSprinting = false;

    private void Start()
    {
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = currentStamina;
    }

    private void Update()
    {
        if (isSprinting && currentStamina > 0f)
        {
            float sprintReductionRate = staminaReductionRate * sprintSpeedMultiplier * Time.deltaTime;
            currentStamina -= sprintReductionRate;
        }
        else
        {
            float reductionRate = staminaReductionRate * Time.deltaTime;
            currentStamina += reductionRate;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        if (currentStamina < maxStamina && !isSprinting)
        {
            float regeneration = staminaRegenerationRate * Time.deltaTime;
            currentStamina += regeneration;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }

        staminaSlider.value = currentStamina;
    }

    public void StartSprinting()
    {
        isSprinting = true;
    }

    public void StopSprinting()
    {
        isSprinting = false;
    }

    public bool HasStamina(float amount)
    {
        return currentStamina >= amount;
    }

    public void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        staminaSlider.value = currentStamina;
    }

    public float GetCurrentStamina()
    {
        return currentStamina;
    }
}