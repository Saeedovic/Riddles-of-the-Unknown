using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteractable : PointOfInterest, IInteractableObject
{

    [SerializeField] Image uiToDisplayNote;
    [SerializeField] string questToComplete;
    //[SerializeField] Texture2D noteTexture;
    [SerializeField] AudioClip endInteractionAudio;

    PlayerCameraController playerCam;
    float normalTimeScale;
    bool isInInteraction = false;


    private void Start()
    {
        uiToDisplayNote.gameObject.SetActive(false);
    }

    // placeholder for demo, working on a more scalable version.
    public void Interact(PlayerInteractor user)
    {
        // pause everything
        playerCam = user.GetComponentInChildren<PlayerCameraController>();
        playerCam.enabled = false;

        normalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        // display note
        uiToDisplayNote.gameObject.SetActive(true);
        isInInteraction = true;
        StartCoroutine(WaitForContinue());
    }

    IEnumerator WaitForContinue()
    {
        yield return null;

        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Return))
        {
            // unpause
            playerCam.enabled = true;
            Time.timeScale = normalTimeScale;

            // stop displaying note
            uiToDisplayNote.gameObject.SetActive(false);
            isInInteraction = false;
            //AudioSource.PlayClipAtPoint(endInteractionAudio, playerCam.transform.position);

            var questManager = playerCam.gameObject.GetComponentInParent<QuestManager>();
            if (questManager != null)
            {
                if (questManager.quests[questManager.currentQuestIndex] == questToComplete)
                {
                    questManager.CompleteCurrentQuest();
                }
            }

            yield break;
        }

        StartCoroutine(WaitForContinue());
    }


    public bool IsInteractable()
    {
        if (isInInteraction)
            return false;
        else
            return true;
    }

    public void OnDeHighlight() { }

    public void OnHighlight() { }
}
