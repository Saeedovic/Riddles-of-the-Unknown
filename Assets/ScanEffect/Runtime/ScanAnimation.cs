using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace OD.Effect.HDRP {
    public class ScanAnimation : MonoBehaviour {
        public enum Mode {Scan, StealthVision}

        [Tooltip("Use this transform position as scan source")]
        public bool useThisAsScanSource = true;
        [Tooltip("Custom Scan Source")]
        public Transform customScanSource;
        [Space]
        
        [Tooltip("Scan Radius Move Speed")]
        public float scanSpeed = 180;
        public float cooldownTime = 7f;
        float initialCooldownTime = 0f;
        bool cooldownActive = false;

        [Tooltip("Enable this to use custom inputs. Call these function in your custom inputs: StartScan() | StartStealthVision(), EndStealthVision()")]
        public bool customInput = false;
        [Tooltip("Key used to start scan animation. StealthVision Mode need to hold the input key")]
        public KeyCode inputKey = KeyCode.LeftControl;
        [Tooltip("Animation Mode")]
        public Mode mode;

        [Header("Scan Mode")]
        [Tooltip("Distance to stop effect animtion")]
        public float scanModeEndDistance = 300;

        [Header("Stealth Vision Mode")]
        [Tooltip("Max distance of scan radius")]
        public float svModeEndDistance = 100;
        [Tooltip("Transition Time")]
        public float transitionTime = 0.25f;

        [Tooltip("Transition Curve")]
        public AnimationCurve transitionCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        

        [Tooltip("In Stealth Vision Volume")]
        public Volume inStealthVisionVolume;        

        [Tooltip("Use Transition Volume")]
        public bool useTransitionVolume = false;
        [Tooltip("Transition Curve")]
        public AnimationCurve volumeTransitionCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(.5f, 1), new Keyframe(1, 0));

        [Tooltip("Transition Stealth Vision Volume")]
        public Volume transitionStealthVisionVolume;

        public enum CurState{End, StarScan, StartSteatlhVision} 
        CurState curState;

        public CurState CurrentState => curState;


        private void Start()
        {
            initialCooldownTime = cooldownTime;
        }

        void Update(){

            cooldownTime += Time.deltaTime;

            if (cooldownTime > initialCooldownTime)
            {
                cooldownTime = initialCooldownTime;
                cooldownActive = false;
            }

            if (!cooldownActive)
            {
                InputHandle();
            }

            HandleScanAnim();
        }

        public void StartScan(){
            curState = CurState.StarScan;
            ScanEffect.scanRadius = 0;
        }

        void HandleScanAnim(){
            if(curState != CurState.StarScan) return;

            ScanEffect.scanPos = transform.position;

            ScanEffect.scanRadius += scanSpeed * Time.deltaTime;
            if(ScanEffect.scanRadius > scanModeEndDistance){
                curState = CurState.End;
                ScanEffect.scanRadius = 0;
            }
        }

        public void StartStealthVision(){
            if(curState == CurState.StartSteatlhVision) return;

            curState = CurState.StartSteatlhVision;
            StopAllCoroutines();

            if(useTransitionVolume == true)
                StartCoroutine(HandleStealthVision());
            else 
                StartCoroutine(HandleStealthVision2());
        }

        public void EndStealthVision(){
            curState = CurState.End;
        }

        IEnumerator HandleStealthVision(){
            ScanEffect.scanRadius = 0;

            if(transitionStealthVisionVolume != null)
                transitionStealthVisionVolume.gameObject.SetActive(true);
            if(inStealthVisionVolume != null)
                inStealthVisionVolume.gameObject.SetActive(false);

            float t = 0;
            while(t < transitionTime){
                t += Time.deltaTime;
                float nt = Mathf.InverseLerp(0, transitionTime, t);
                if(transitionStealthVisionVolume != null)
                    transitionStealthVisionVolume.weight = volumeTransitionCurve.Evaluate(nt);
                yield return null;
            }
            if(transitionStealthVisionVolume != null)
                transitionStealthVisionVolume.gameObject.SetActive(false);
            if(inStealthVisionVolume != null)
                inStealthVisionVolume.gameObject.SetActive(true);

            while(curState == CurState.StartSteatlhVision){
                if(useThisAsScanSource){
                    ScanEffect.scanPos = transform.position;
                } else if(customScanSource != null){
                    ScanEffect.scanPos = customScanSource.position;
                }


                if(ScanEffect.scanRadius < svModeEndDistance){
                    ScanEffect.scanRadius += scanSpeed * Time.deltaTime;
                }
                yield return null;
            }

            ScanEffect.scanRadius = 0;

            if(transitionStealthVisionVolume != null)
                transitionStealthVisionVolume.gameObject.SetActive(true);
            if(inStealthVisionVolume != null)
                inStealthVisionVolume.gameObject.SetActive(false);

            t = 0;
            while(t < transitionTime){
                t += Time.deltaTime;
                float nt = Mathf.InverseLerp(0, transitionTime, t);
                if(transitionStealthVisionVolume != null)
                    transitionStealthVisionVolume.weight = volumeTransitionCurve.Evaluate(nt);
                yield return null;
            }
            if(transitionStealthVisionVolume != null)
                transitionStealthVisionVolume.gameObject.SetActive(false);
            if(inStealthVisionVolume != null)
                inStealthVisionVolume.gameObject.SetActive(false);

        }

        IEnumerator HandleStealthVision2(){
            ScanEffect.scanRadius = 0;

            if(inStealthVisionVolume != null)
                inStealthVisionVolume.gameObject.SetActive(true);

            float t = 0;
            while(t < transitionTime){
                t += Time.deltaTime;
                float nt = Mathf.InverseLerp(0, transitionTime, t);
                if(inStealthVisionVolume != null)
                    inStealthVisionVolume.weight = transitionCurve.Evaluate(nt);
                yield return null;
            }

            while(curState == CurState.StartSteatlhVision){
                if(useThisAsScanSource){
                    ScanEffect.scanPos = transform.position;
                } else if(customScanSource != null){
                    ScanEffect.scanPos = customScanSource.position;
                }

                if(ScanEffect.scanRadius < svModeEndDistance){
                    ScanEffect.scanRadius += scanSpeed * Time.deltaTime;
                }
                yield return null;
            }

            ScanEffect.scanRadius = 0;
            t = 0;
            while(t < transitionTime){
                t += Time.deltaTime;
                float nt = Mathf.InverseLerp(0, transitionTime, t);
                if(inStealthVisionVolume != null)
                    inStealthVisionVolume.weight = transitionCurve.Evaluate(1 - nt);
                yield return null;
            }

            if(inStealthVisionVolume != null)
                inStealthVisionVolume.gameObject.SetActive(false);

        }

        void InputHandle(){
            if(customInput == true) return;

            if(mode == Mode.Scan){
                if(Input.GetKeyDown(inputKey)){
                    StartScan();
                    cooldownTime = 0f;
                    cooldownActive = true;
                }
            } else {
                if(Input.GetKey(inputKey) && curState == CurState.End){
                    StartStealthVision();
                } else if (Input.GetKey(inputKey) == false && curState == CurState.StartSteatlhVision){
                    EndStealthVision();
                }
            }
        }   
    }
}