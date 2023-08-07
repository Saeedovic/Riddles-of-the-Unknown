using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAppIntroTrigger : MonoBehaviour
{
    TutorialManager tManager;
    public GameObject tutObjRef;

 


    // Start is called before the first frame update
    void Start()
    {
        tManager = tutObjRef.GetComponent<TutorialManager>();
      
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            tManager.popUpIndex = 48;
            this.gameObject.SetActive(false);
        }
    }
}
