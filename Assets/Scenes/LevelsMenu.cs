using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    public int worldNumber = 1;
    public int maxWorldNumber = 3;
    public int lastBeatenLevel = 2;
    public TextMeshProUGUI worldText;
    public GameObject nextWorldButton;
    public GameObject previousWorldButton;

    public bool loadProgress = true;

    public List<GameObject> levelButtons;

    public void Start()
    {
        if (loadProgress)
        {
            maxWorldNumber = PlayerPrefs.GetInt("world", 1);
            lastBeatenLevel = PlayerPrefs.GetInt("level", 0);
        }
        
        if (lastBeatenLevel == 9)
        {
            lastBeatenLevel = 0;
            maxWorldNumber++;
        }

        LoadMenu();
    }

    public void LoadMenu()
    {
        worldText.text = "WORLD " + worldNumber.ToString();
        if (worldNumber == 1)
        {
            previousWorldButton.SetActive(false);
        }
        if (worldNumber == maxWorldNumber)
        {
            nextWorldButton.SetActive(false);
            for(int i = 0; i < levelButtons.Count; i++)
            {
                if (i <= lastBeatenLevel)
                {
                    levelButtons[i].SetActive(true);
                }
                else
                {
                    levelButtons[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].SetActive(true);
            }
        }
    }

    public void PlayLevel(int level)
    {
        string sceneName = worldNumber.ToString() +  "-" + level.ToString();
        SceneManager.LoadScene(sceneName);
    }

    public void NextWorld()
    {
        worldNumber++;
        previousWorldButton.SetActive(true);
        LoadMenu();
    }

    public void PreviousWorld() 
    {
        worldNumber--;
        nextWorldButton.SetActive(true);
        LoadMenu();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
