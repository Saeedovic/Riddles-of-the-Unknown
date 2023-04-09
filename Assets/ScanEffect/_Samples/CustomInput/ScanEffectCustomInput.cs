using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OD.Effect.HDRP;

[RequireComponent(typeof(ScanAnimation))]
public class ScanEffectCustomInput : MonoBehaviour {
    public KeyCode scanKey = KeyCode.V;

    ScanAnimation scanAnimation;

    void Start(){
        scanAnimation = GetComponent<ScanAnimation>();
        scanAnimation.customInput = true; // Make sure of this options is true
    }

    void Update(){
        //Scan Mode
        if(scanAnimation.mode == ScanAnimation.Mode.Scan){

            if(Input.GetKeyDown(scanKey))
                scanAnimation.StartScan(); // Just call this function to start the Scan
            
        } else {
        //Steath Vision Mode

            if(Input.GetKey(scanKey) && scanAnimation.CurrentState == ScanAnimation.CurState.End)
                scanAnimation.StartStealthVision(); // Call this function to start Steath Vision
            else if (Input.GetKey(scanKey) == false && scanAnimation.CurrentState == ScanAnimation.CurState.StartSteatlhVision)
                scanAnimation.EndStealthVision(); // Call this function to end Steath Vision
            
        }
    }
}
