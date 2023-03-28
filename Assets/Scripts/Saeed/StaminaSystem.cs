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
    private bool isMoving;
    private CharacterController controller;
    public Slider staminaBar;

    private void Start()
    {
        currentStamina = maxStamina;
        controller = GetComponent<CharacterController>();
        staminaBar = FindObjectOfType<Slider>();
       // UpdateStaminaBar();
    }

    private void Update()
    {
        isMoving = (Mathf.Abs(Input.GetAxis("Horizontal")) > 0f || Mathf.Abs(Input.GetAxis("Vertical")) > 0f);

        if (isMoving && currentStamina > 0f)
        {
            currentStamina -= staminaDecreaseRate * Time.deltaTime;
            if (currentStamina <= 0f)
            {
                currentStamina = 0f;
                isMoving = false;
            }
            UpdateStaminaBar();
        }
        else if (!isMoving && currentStamina < maxStamina)
        {       
            currentStamina += staminaIncreaseRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }

          
            UpdateStaminaBar();
        }

        
        if (isMoving && currentStamina > 0f)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            controller.Move(move * Time.deltaTime);

           
            currentStamina -= move.magnitude * staminaDecreaseRate * Time.deltaTime;

            if (currentStamina <= 0f)
            {
                currentStamina = 0f;
                isMoving = false;
            }

         
            UpdateStaminaBar();
        }
        Debug.Log("current stamina:" + currentStamina);
    }

    public void UpdateStaminaBar()
    {
        staminaBar.value = currentStamina / maxStamina;
    }
}