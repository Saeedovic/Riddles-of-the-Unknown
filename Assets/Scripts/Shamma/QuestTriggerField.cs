using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerField : MonoBehaviour
{
    public string questToComplete;

    private void OnTriggerEnter(Collider other)
    {
        var QuestManager = other.GetComponent<QuestManager>();
        if (QuestManager != null)
        {
            if (QuestManager.quests[QuestManager.currentQuestIndex] == questToComplete)
            {
                QuestManager.CompleteCurrentQuest();
            }
        }
    }
}
