using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button1DownPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    bool NextNumDown_1st_Digit;
 
    Safe_System safe;
    [SerializeField] GameObject safeObj;


    void Awake()
    {
        safe = safeObj.GetComponent<Safe_System>();
    }

    
    void Update()
    {
        if (NextNumDown_1st_Digit)
        {
            print("Button is Pressed");
            safe.NextNumber1stD();
        }
    }

     void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        NextNumDown_1st_Digit = true;
    }

     void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        NextNumDown_1st_Digit = false;

    }
}
