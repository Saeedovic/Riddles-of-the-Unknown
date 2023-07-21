using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaistPivot : MonoBehaviour
{
    [SerializeField] GameObject cameraObj;

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraObj.transform.rotation, Time.deltaTime);
    }
}
