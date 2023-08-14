using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;

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
    [SerializeField] float crouchAdjustSpeed = 1f;
    
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;

    private CharacterController controller;
    private Vector3 forceOfGravity;

    bool isWalking = false;
    public bool isRunning = false;
    bool isCrouching = false;

    bool movingLeft = false;
    bool movingRight = false;
    bool movingBack = false;

    

    Animator animator;

    [SerializeField] AudioSource walkingAudioSource;
    [SerializeField] AudioClip walkingAudio;
    [SerializeField] AudioClip runningAudio;

    public AudioSource playerAudio;
    public AudioClip AudioClipForGameEnvironment;

    public bool soundPlayed;

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
    }

     void Update()
    {
        // other code
        {
            if (!soundPlayed)
            {
                playerAudio.clip = AudioClipForGameEnvironment;

               playerAudio.Play();

              
                soundPlayed = true;
            }
        }

        // get input and convert to local vector

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(horizontal, 0, vertical);

        Vector3 move = transform.TransformDirection(moveInput);


        
        // run controls, animator bool setting 
        float currentSpeed = walkSpeed;

        DetermineRunState(currentSpeed, horizontal, vertical);

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        //animator.SetFloat("moveSpeed", (move.x * move.z) * currentSpeed);



        PlayMovementSoundEffect();
        

        if (Input.GetKey(crouchKey) && controller.isGrounded && !isRunning)
        {
            currentSpeed /= crouchDivider;
            controller.height = Mathf.Lerp(controller.height, crouchHeight, Time.deltaTime * crouchAdjustSpeed);
            isCrouching = true;
        }
        else
        {
            controller.height = Mathf.Lerp(controller.height, regularHeight, Time.deltaTime * crouchAdjustSpeed);
            isCrouching = false;
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


    void DetermineRunState(float currentSpeed, float horizInput, float vertInput)
    {
        if (Input.GetKey(runKey) && vertInput > 0)
        {
            currentSpeed *= runMultiplier;
            isWalking = false;
            isRunning = true;
        }
        else if (Mathf.Abs(horizInput) > 0.5f || Mathf.Abs(vertInput) > 0.5f)
        {
            isWalking = true;
            isRunning = false;
        }
        else
        {
            isWalking = false;
            isRunning = false;
        }
    }


    void PlayMovementSoundEffect()
    {
        // check if we should switch to walking or running clips
        if (isWalking && walkingAudioSource.clip != walkingAudio)
        {
            walkingAudioSource.Stop();
            walkingAudioSource.clip = walkingAudio;
        }
        if (isRunning && walkingAudioSource.clip != runningAudio)
        {
            walkingAudioSource.Stop();
            walkingAudioSource.clip = runningAudio;
        }

        // play the clip when we're moving and it isn't already playing
        if ((isWalking || isRunning) && !walkingAudioSource.isPlaying)
        {
            walkingAudioSource.Play();
        }

        if (!isWalking && !isRunning)
        {
            walkingAudioSource.Stop();
        }
    }


    public void SetFingerRigState(Rig rig)
    {

    }
}