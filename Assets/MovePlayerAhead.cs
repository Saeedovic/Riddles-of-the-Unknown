using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerAhead : MonoBehaviour
{
    private void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-1,0,0) * Time.deltaTime;
    }
}
