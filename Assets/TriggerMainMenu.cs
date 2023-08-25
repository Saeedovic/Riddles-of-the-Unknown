using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerMainMenu : MonoBehaviour
{
    
    private string MainMenu = "MainMenu";

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CoFunc());
    }

    IEnumerator CoFunc()
    {
        yield return new WaitForSecondsRealtime(14);
        SceneManager.LoadScene(MainMenu);
    }
}
