using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycler : MonoBehaviour
{
    public bool dayNightCycleActive = true;
    [SerializeField] float timeFactor = 1;

    void Update()
    {
        transform.Rotate(0.01f * timeFactor, 0, 0);
    }
}
