using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerCaveAudio : MonoBehaviour
{
    public UseTorch TorchScript;
    public AudioClip caveAudioClip;
    public AudioSource caveAudio;
    public EnableTorchTrigger natureAudioRef;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            TorchScript.enabled = false;

            natureAudioRef.natureAudioSource.volume = 0;

            caveAudio.clip = caveAudioClip;
            caveAudio.loop = true;
            caveAudio.Play();

          
        }
    }
}
