using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAppIntroTrigger : MonoBehaviour
{
    TutorialManager tManager;
    public GameObject tutObjRef;

    public AudioSource playerAudio;

    public AudioClip WishIWasMoreFit;

    PhoneManager phoneManager;
    [SerializeField] GameObject PhoneManagerObjRef;




    // Start is called before the first frame update
    void Start()
    {
        tManager = tutObjRef.GetComponent<TutorialManager>();
        phoneManager = PhoneManagerObjRef.GetComponent<PhoneManager>();


    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
           StartCoroutine(BetterStatsVoiceOver());
        }
    }


    IEnumerator BetterStatsVoiceOver()
    {
        //Add Voice that says Damn I've been walking a while, I wish i was more fit for this!
        playerAudio.clip = WishIWasMoreFit;

        playerAudio.loop = false;
        playerAudio.Play();

        StartCoroutine(TutorialManager.DisplaySubs("I wish I was more fit to walk these long paths.", 3.5f));


        yield return new WaitForSeconds(2);
       // phoneManager.SetPhoneState(true);

            tManager.popUpIndex = 48;
            this.gameObject.SetActive(false);

        
    }
}
