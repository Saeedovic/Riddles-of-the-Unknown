using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public AudioSource playerAudio;
    public AudioClip AudioClipForGameEnvironment;

    private bool soundPlayed;

    //public GameObject SafeCam;
    //public GameObject DefaultCam;

    //public GameObject SafeCanvas;

    //public Button closeButton;
    //private float range = 5;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        soundPlayed = false;
        // Cursor.lockState = CursorLockMode.Locked;

        flashLight.SetActive(false);
        flashLightIsOn = false;

        Cursor.lockState = CursorLockMode.Locked;
        //SafeCanvas.SetActive(false);
    }

     void Update()
    {
        // for adjusting mouse state in scenarios like the safe puzzle
        if (Input.GetMouseButtonDown(0) && !PhoneManager.Instance.mouseShouldBeUseable)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && PhoneManager.Instance.mouseShouldBeUseable)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (!soundPlayed)
        {
            playerAudio.PlayOneShot(AudioClipForGameEnvironment);
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



        /*Vector3 direction = Vector3.forward;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (hit.collider.tag == "Safe" && Input.GetMouseButtonDown(1))
            {

                Debug.Log("Safe is in Range!");
                DefaultCam.SetActive(false);
                SafeCam.SetActive(true);
                SafeCanvas.SetActive(true);

                closeButton.onClick.AddListener(ExitCrackingSafe);


            }
        }*/

    }



    /*public void ExitCrackingSafe()
    {
        SafeCam.SetActive(false);
        DefaultCam.SetActive(true);
        SafeCanvas.SetActive(false);

    }*/
}