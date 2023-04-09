using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFpsController : MonoBehaviour {
    public Transform camHolder;
    public float speed = 6.0f;
    public float lookSpeed = 3;

    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;
    float rotX = 0.0f;
    float rotY = 0.0f;

    void Start () {
        controller = GetComponent<CharacterController> ();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update () {
        Vector3 forward = transform.forward * Input.GetAxisRaw ("Vertical");
        Vector3 right = transform.right * Input.GetAxisRaw ("Horizontal");
        Vector3 move = (forward + right).normalized;

        if (controller.isGrounded) {
            moveDirection = new Vector3 (move.x, 0, move.z);
            moveDirection *= speed;
            if (Input.GetButton ("Jump")) {
                moveDirection.y = 8.0f;
            }
        }

        moveDirection.y -= 20.0f * Time.deltaTime;
        controller.Move (moveDirection * Time.deltaTime);
        MouseLook ();
    }

    void MouseLook(){
        rotX += -Input.GetAxis ("Mouse Y");
        rotY += Input.GetAxis ("Mouse X");

        rotX = Mathf.Clamp(rotX, -50, 50f);
        transform.eulerAngles = new Vector2(0,rotY) * lookSpeed;

        camHolder.localRotation = Quaternion.Euler(rotX * lookSpeed, 0, 0);
    }
}