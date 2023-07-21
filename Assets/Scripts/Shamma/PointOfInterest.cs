using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OD.Effect.HDRP;

// class for ecopoint objects to inherit from.
[RequireComponent(typeof(OnScanHighlight))]
public class PointOfInterest : MonoBehaviour
{
    OnScanHighlight onScanHighlight;
    ScanReciver scanReciver;

    private void Start()
    {
        onScanHighlight = GetComponent<OnScanHighlight>();
        scanReciver = GetComponent<ScanReciver>();
    }

    public void SetScannabilityOn()
    {
        onScanHighlight.enabled = true;
        scanReciver.enabled = true;
    }
    
    public void SetScannabilityOff()
    {
        onScanHighlight.enabled = false;
        scanReciver.enabled = false;
    }
    
}
