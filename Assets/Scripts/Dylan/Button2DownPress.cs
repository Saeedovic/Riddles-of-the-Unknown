using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button2DownPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    bool NextNumDown_2nd_Digit;

    Safe_System safe;
    [SerializeField] GameObject safeObj;


    void Awake()
    {
        safe = safeObj.GetComponent<Safe_System>();
    }


    void Update()
    {
        if (NextNumDown_2nd_Digit)
        {
            print("Button is Pressed");
            safe.NextNumber2stD();
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        NextNumDown_2nd_Digit = true;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        NextNumDown_2nd_Digit = false;

    }
}