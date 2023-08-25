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

    [SerializeField] Rig[] fingerRigs;
    [SerializeField] float rigWeightSmoothVelocity;

    private CharacterController controller;
    private Vector3 forceOfGravity;

    bool isWalkingForward = false;
    public bool isRunning = false;
    bool isCrouching = false;

    bool isWalkingLeft = false;
    bool isWalkingRight = false;
    bool isWalkingBack = false;

    

    Animator animator;

    [SerializeField] AudioSource walkingAudioSource;
    [SerializeField] AudioClip walkingAudio;
    [SerializeField] AudioClip runningAudio;

    public AudioSource playerAudio;

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
        
        // get input and convert to local vector

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(horizontal, 0, vertical);

        Vector3 move = transform.TransformDirection(moveInput);


        
        // run controls, animator bool setting 
        float currentSpeed = walkSpeed;

        currentSpeed = DetermineRunState(currentSpeed, horizontal, vertical);

        animator.SetBool("isWalkingForward", isWalkingForward);
        animator.SetBool("isWalkingLeft", isWalkingLeft);
        animator.SetBool("isWalkingRight", isWalkingRight);
        animator.SetBool("isWalkingBack", isWalkingBack);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isCrouching", isCrouching);
        //animator.SetFloat("moveSpeed", (move.x * move.z) * currentSpeed);


        PlayMovementSoundEffect();
        
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


    float DetermineRunState(float currentSpeed, float horizInput, float vertInput)
    {
        // not proud of the number of if elses here but we were a bit pressed for time in this project lol
        if (Input.GetKey(runKey) && (vertInput > 0 || horizInput > 0))
        {
            currentSpeed *= runMultiplier;
            isWalkingForward = false;
            isWalkingLeft = false;
            isWalkingRight = false;
            isWalkingBack = false;
            isRunning = true;
        }
        else if (horizInput > 0.5f)
        {
            isWalkingForward = false;
            isWalkingLeft = false;
            isWalkingRight = true;
            isWalkingBack = false;
            isRunning = false;
            //Debug.Log("one");
        }
        else if (-0.5f > horizInput)
        {
            isWalkingForward = false;
            isWalkingLeft = true;
            isWalkingRight = false;
            isWalkingBack = false;
            isRunning = false;
            //Debug.Log("two");
        }
        else if (vertInput > 0.5f)
        {
            isWalkingForward = true;
            isWalkingLeft = false;
            isWalkingRight = false;
            isWalkingBack = false;
            isRunning = false;
            //Debug.Log("three");
        }
        else if (-0.5f > vertInput)
        {
            isWalkingForward = false;
            isWalkingLeft = false;
            isWalkingRight = false;
            isWalkingBack = true;
            isRunning = false;
            //Debug.Log("four");
        }
        else
        {
            isWalkingForward = false;
            isWalkingLeft = false;
            isWalkingRight = false;
            isWalkingBack = false;
            isRunning = false;
        }


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



        if ((Mathf.Abs(horizInput) > 0.5f || Mathf.Abs(vertInput) > 0.5f) && PhoneManager.phoneIsOut)
            SetFingerRigState(fingerRigs, true);
        else
            SetFingerRigState(fingerRigs, false);
        

        //Debug.Log(horizInput + ", " + vertInput);
        return currentSpeed;
    }


    void PlayMovementSoundEffect()
    {
        // check if we should switch to walking or running clips
        if ((isWalkingForward || isWalkingBack
            || isWalkingLeft || isWalkingRight)
            && walkingAudioSource.clip != walkingAudio)
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
        if ((isWalkingForward || isWalkingBack
            || isWalkingLeft || isWalkingRight
            || isRunning) && !walkingAudioSource.isPlaying)
        {
            walkingAudioSource.Play();
        }

        if (!isWalkingForward && !isRunning
            && !isWalkingBack && !isWalkingLeft && !isWalkingRight)
        {
            walkingAudioSource.Stop();
        }
    }


    public void SetFingerRigState(Rig[] rig, bool activate)
    {
        for (int i = 0; i < rig.Length; i++)
        {
            if (activate)
                rig[i].weight = Mathf.MoveTowards(rig[i].weight, 1, rigWeightSmoothVelocity);
            else
                rig[i].weight = Mathf.MoveTowards(rig[i].weight, 0, rigWeightSmoothVelocity);
        }
    }
}