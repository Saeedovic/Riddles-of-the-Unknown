using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTestSceneManager : MonoBehaviour
{
   public void PlayAgain()
    {
        SceneManager.LoadScene("Live_Playtest_Build");
    }

    public void Exit()
    {
        Application.Quit();
    }

 
}

