using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseStateController : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
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
    }
}
