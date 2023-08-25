using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// activate caves sounds and deactivate nature sounds 
public class EnableTorchTrigger : MonoBehaviour
{

    public GameObject torchTriggerCave1;
    public GameObject torchTriggerCave2;

    public AudioSource natureAudioSource;
    
    public AudioClip natureAudioClip;

    public TriggerCaveAudio caveAudioRef;





    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
             torchTriggerCave1.SetActive(true);
             torchTriggerCave2.SetActive(true);

             caveAudioRef.caveAudio.Stop();
             Debug.Log("PLAY NATURE SOUND");
             natureAudioSource.volume = 1.0f;
             natureAudioSource.clip = natureAudioClip;
             natureAudioSource.loop = true;
             natureAudioSource.Play();


            }
        }
    }

