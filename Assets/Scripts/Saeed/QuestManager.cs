using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI questText;

    public AudioClip AudioForCompletingQuest;

    public AudioClip AudioForNextQuest;

    public List<string> quests = new List<string> { };
    public int currentQuestIndex = 0;

    public void Start()
    {
        UpdateQuestText();
    }

    public void CompleteCurrentQuest()
    {
        currentQuestIndex++;

        if (currentQuestIndex < quests.Count)
        {
            UpdateQuestText();
        }
        else
        {
            questText.text = "Quests complete!";
            AudioSource.PlayClipAtPoint(AudioForCompletingQuest, transform.position);
        }
    }

    public void UpdateQuestText()
    {

        AudioSource.PlayClipAtPoint(AudioForNextQuest, transform.position);

        questText.text = quests[currentQuestIndex];
    }
}