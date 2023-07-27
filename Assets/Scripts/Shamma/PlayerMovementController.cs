using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    CharacterController playerController;
    public float moveIntensity = 1f;
    XPManager xP;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        xP = GetComponent<XPManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveDir = transform.TransformDirection(moveInput);
        playerController.Move(moveDir * moveIntensity);
    }
}
