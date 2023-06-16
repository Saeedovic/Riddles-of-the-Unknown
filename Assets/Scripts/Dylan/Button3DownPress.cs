using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button3DownPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    bool NextNumDown_3rd_Digit;

    Safe_System safe;
    [SerializeField] GameObject safeObj;


    void Awake()
    {
        safe = safeObj.GetComponent<Safe_System>();
    }


    void Update()
    {
        if (NextNumDown_3rd_Digit)
        {
            print("Button is Pressed");
            safe.NextNumber3stD();
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        NextNumDown_3rd_Digit = true;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        NextNumDown_3rd_Digit = false;

    }
}