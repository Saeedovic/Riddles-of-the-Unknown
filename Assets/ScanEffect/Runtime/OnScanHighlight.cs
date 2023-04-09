using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OD.Effect.HDRP {
    [RequireComponent(typeof(ScanReciver))]
    public class OnScanHighlight : MonoBehaviour {
        public enum HighlightMode{AddMaterial, ChangeLayer}
        
        [Tooltip("AddMaterial: Add a highlight material in child mesh. ChangeLayer: change the layer of child mesh, useful to use with custom render pass.")]
        public HighlightMode mode;
        [Space]

        [Tooltip("Highlight Material")]
        public Material highlightMaterial;
        [Space]

        [Tooltip("Default Layer")]
        public int defaultLayer = 0;
        [Tooltip("Highlight Layer")]
        public int highlightLayer;

        void Start(){
            ScanReciver scanReciver = GetComponent<ScanReciver>();

            scanReciver.onInScan.AddListener(() => {
                foreach(Renderer m in GetComponentsInChildren<Renderer>()){
                    if(mode ==HighlightMode.ChangeLayer){
                        m.gameObject.layer = highlightLayer;
                    } else {
                        if(highlightMaterial != null){
                            List<Material> toadd = new List<Material>(m.materials);
                            toadd.Add(highlightMaterial);

                            m.materials = toadd.ToArray();
                        }
                    }
                }
            });

            scanReciver.onOffScan.AddListener(() => {
                foreach(Renderer m in GetComponentsInChildren<Renderer>()){
                    if(mode ==HighlightMode.ChangeLayer){
                        m.gameObject.layer = defaultLayer;
                    } else {
                        if(highlightMaterial != null){
                            List<Material> toRemove = new List<Material>(m.materials);
                            foreach(Material _m in toRemove){
                                if(_m.shader == highlightMaterial.shader){
                                    toRemove.Remove(_m);
                                    break;
                                }
                            }

                            m.materials = toRemove.ToArray();
                        }
                    }
                }
            });
        }
    }
}
