using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// activate caves sounds and deactivate nature sounds 
public class EnableTorchTrigger : MonoBehaviour
{

    public GameObject torchTriggerCave1;
    public GameObject torchTriggerCave2;

    public AudioSource playerAudio;

    public AudioClip natureAudioClip;
   



    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            torchTriggerCave1.SetActive(true);
            torchTriggerCave2.SetActive(true);

            if (playerAudio.clip != natureAudioClip)
            {
                playerAudio.Stop();

                playerAudio.clip = natureAudioClip;
                playerAudio.loop = true;
                playerAudio.Play();
            }
        }
    }
}
