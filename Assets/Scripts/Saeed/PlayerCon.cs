using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCon : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 5f;

    public ThirstSystem thirstSystem;
    public StaminaSystem staminaSystem;

    public CharacterController characterController;

    private Vector3 moveDirection;
    private bool isJumping;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        isJumping = false;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool jump = Input.GetButtonDown("Jump");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        moveDirection = forward * vertical + right * horizontal;
        moveDirection *= movementSpeed;

        if (!characterController.isGrounded)
        {
            moveDirection.y -= Physics.gravity.magnitude * Time.deltaTime;
        }

        if (jump && characterController.isGrounded && !isJumping && staminaSystem.HasStamina(20f))
        {
            moveDirection.y = jumpForce;
            isJumping = true;
            staminaSystem.UseStamina(20f);
        }

        if (moveDirection.magnitude > 0 && Input.GetKey(KeyCode.LeftShift) && staminaSystem.HasStamina(10f))
        {
            moveDirection *= staminaSystem.sprintSpeedMultiplier;
            staminaSystem.UseStamina(10f);
        }

        characterController.Move(moveDirection * Time.deltaTime);

       // thirstSystem.DrinkWater(5f * Time.deltaTime);

        if (isJumping)
        {
          //  staminaSystem.RestoreStamina(2f * Time.deltaTime);
        }
        else
        {
           // staminaSystem.RestoreStamina(5f * Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (characterController.isGrounded && isJumping)
        {
            isJumping = false;
        }
    }
}