using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour
{
    public float speed = 5f;
    public float walkSpeed = 5f;
    public float runMultiplier = 2f;
    public float crouchDivider = 2f;
    public float jumpForce = 10f;
    public float gravityFactor = -20f;
    [SerializeField] float regularHeight = 2f;
    [SerializeField] float crouchHeight = 1f;
    
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;

    private CharacterController controller;
    private Vector3 forceOfGravity;

    bool isWalking = false;
    public bool isRunning = false;
    bool isCrouching = false;

    

    Animator animator;

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
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        soundPlayed = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

     void Update()
    {
        // other code
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
        }

        // get input and convert to local vector

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(horizontal, 0, vertical);

        Vector3 move = transform.TransformDirection(moveInput);

        {/*
            bool isWalking;

            if ((horizontal < 0.5f || horizontal > 0.5f) || (vertical < 0.5f || vertical > 0.5f))
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            animator.SetBool("isWalking", isWalking);*/
        }

        
        // run controls, animator bool setting 
        float currentSpeed = walkSpeed;

        if (Input.GetKey(runKey) && vertical > 0)
        {
            currentSpeed *= runMultiplier;
            isWalking = false;
            isRunning = true;
        }
        else if (Mathf.Abs(horizontal) > 0.5f || Mathf.Abs(vertical) > 0.5f)
        {
            isWalking = true;
            isRunning = false;
        }
        else
        {
            isWalking = false;
            isRunning = false;
        }

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        //animator.SetFloat("moveSpeed", (move.x * move.z) * currentSpeed);

        if (Input.GetKey(crouchKey) && controller.isGrounded && !isRunning)
        {
            currentSpeed /= crouchDivider;
            controller.height = crouchHeight;
            isCrouching = true;
        }
        else
        {
            controller.height = regularHeight;
            isCrouching = false;
        }

        {
        /*
        if (currentSpeed > 8 && isWalking)
        {
            animator.SetBool("startRunning", true);
        }
        if (currentSpeed < 8 && !isWalking)
        {
            animator.SetBool("startRunning", false);
        }
        */
        }

        // apply movement vector to char
        controller.Move(move * currentSpeed * Time.deltaTime);


        // apply gravity force
        forceOfGravity.y += gravityFactor * Time.deltaTime;
        controller.Move(forceOfGravity * Time.deltaTime);

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            forceOfGravity.y = Mathf.Sqrt(jumpForce * -2f * gravityFactor);
        }

     }


}