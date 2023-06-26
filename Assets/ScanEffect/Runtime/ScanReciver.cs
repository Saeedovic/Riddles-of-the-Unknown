using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OD.Effect.HDRP {
    public class ScanReciver : MonoBehaviour {
        public enum OffBy {ScanEnd, Time}

        [Tooltip("Event invoked when this gameobject is in scan radius")]
        public UnityEvent onInScan = new UnityEvent();
        [Tooltip("Event invoked when this gameobject is off of the scan effect")]
        public UnityEvent onOffScan = new UnityEvent();

        [Tooltip("ScanEnd: when this gameobject is not in scan radius. Time: by time out")]
        public OffBy offBy = OffBy.Time;
        [Tooltip("timeout to off")]
        public float timeToOff = 10;

        bool inScan = false;
        public bool InScan => inScan;

        IEnumerator offScan(){
            yield return new WaitForSeconds(timeToOff);
            onOffScan.Invoke();
        }

        void Update(){
            if(ScanEffect.InScanRadius(transform.position) && inScan == false){
                inScan = true;
                onInScan.Invoke();

                if(offBy == OffBy.Time) {
                    StopAllCoroutines();
                    StartCoroutine(offScan());
                }
            }

            if(ScanEffect.InScanRadius(transform.position) == false && inScan == true){
                inScan = false;

                if(offBy == OffBy.ScanEnd) onOffScan.Invoke();
            }
        }
    }
}
