using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour
{
    public string thisSceneName;
    public string nextSceneName;
    public string menuSceneName;

    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(thisSceneName);
    }

    public void LevelMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
