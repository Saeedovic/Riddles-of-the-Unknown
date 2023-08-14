using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProgressBarrier : MonoBehaviour
{
    [SerializeField] Vector3 directionToPush;
    [SerializeField] float turnSpeed = 1;
    [SerializeField] float turnForce = 1;
    [SerializeField] float maxAngleTillMovement = 1;
    [SerializeField] float maxDistTillTarget = 1;
    [SerializeField] float maxMovementSpeed = 1;

    GameObject player;
    PlayerCon controls;
    PlayerCameraController camera;

    bool turningDone;
    bool pushingDone;

    private void Start()
    {
        // set up direction if left null
        if (directionToPush == Vector3.zero) 
        {
            directionToPush = transform.forward;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            controls = player.GetComponent<PlayerCon>();
            camera = player.GetComponentInChildren<PlayerCameraController>();

            controls.enabled = false;
            camera.enabled = false;

            turningDone = false;
            pushingDone = false;
            Debug.Log("now starting push");
            StartCoroutine(TurnPlayer());
        }
    }

    IEnumerator TurnPlayer()
    {
        player.transform.forward = Vector3.RotateTowards(player.transform.position, directionToPush, turnForce, turnSpeed * Time.deltaTime);
        Debug.Log("turn");

        if (Vector3.Angle(player.transform.forward, directionToPush) <= maxAngleTillMovement)
        {
            turningDone = true;
            StartCoroutine(PushPlayer());
            yield break;
        }
        
        yield return null;
        StartCoroutine(TurnPlayer());
    }

    IEnumerator PushPlayer()
    {

        player.transform.position = Vector3.MoveTowards(player.transform.position, directionToPush, maxMovementSpeed);
        Debug.Log("push");

        if (Vector3.Distance(player.transform.position, directionToPush) >= maxDistTillTarget && turningDone)
        {
            pushingDone = true;
            Debug.Log("done the pushh");

            controls.enabled = true;
            camera.enabled = true;

            yield break;
        }

        yield return null;
        StartCoroutine(PushPlayer());
    }
}
