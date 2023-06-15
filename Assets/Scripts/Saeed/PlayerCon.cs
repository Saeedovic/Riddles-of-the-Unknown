using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    public float speed = 5f;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 10f;
    public float gravity = -20f;
    //public float sensitivity = 100f;
    //public Transform cameraTransform;
    public KeyCode runKey = KeyCode.LeftShift;

    private CharacterController controller;
    private Vector3 playerVelocity;
    //private float mouseX = 0f;
    //private float mouseY = 0f;
    public bool isRunning = false;

    public GameObject flashLight;
    public bool flashLightIsOn;

    public AudioSource audio;
    public AudioClip AudioClipForGameEnvironment;

    private bool soundPlayed;
   


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        soundPlayed = false;
        // Cursor.lockState = CursorLockMode.Locked;

        flashLight.SetActive(false);
        flashLightIsOn = false;
    }

     void Update()
    {
        if (!soundPlayed)
        {
            audio.PlayOneShot(AudioClipForGameEnvironment);
            soundPlayed = true;
        }

        // AudioSource.PlayClipAtPoint(AudioForGameEnvironment, this.transform.position);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //mouseX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        //mouseY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        //mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        //cameraTransform.localRotation = Quaternion.Euler(mouseY, 0f, 0f);
        //transform.rotation = Quaternion.Euler(0f, mouseX, 0f);

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        float currentSpeed;
        if (isRunning)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }


        controller.Move(move * currentSpeed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        if (Input.GetKey(runKey))
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(runKey))
        {
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && flashLightIsOn == false)
        {
            flashLight.SetActive(true);
            flashLightIsOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && flashLightIsOn == true)
        {
            flashLight.SetActive(false);
            flashLightIsOn = false;
        }

    }
}