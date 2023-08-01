using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    TutorialManager tManager;
    public GameObject tutObjRef;

    QuestManager qManager;
    public GameObject questObjRef;


    // Start is called before the first frame update
    void Start()
    {
        tManager = tutObjRef.GetComponent<TutorialManager>();
        qManager = questObjRef.GetComponent<QuestManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Trigger End Game Cinematic!");
            tManager.popUpIndex = 33;

            qManager.CompleteCurrentQuest();
        }
    }
}
