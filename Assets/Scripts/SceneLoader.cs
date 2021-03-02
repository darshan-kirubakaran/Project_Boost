using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Rocket rocket;
    PauseMenu pauseMenu;
    GameObject optionsCanvas;


    private void Start()
    {
        optionsCanvas = GameObject.Find("Options Canvas");
        rocket = FindObjectOfType<Rocket>();
        if(FindObjectOfType<PauseMenu>())
        {
            pauseMenu = FindObjectOfType<PauseMenu>();
        }

        if (SceneManager.GetActiveScene().name == "Game Over")
        {
            PlayerPrefsController.SetLives(3);
            PlayerPrefs.SetString("SavedLevel", "Level 1");
        }
        optionsCanvas.SetActive(false);
    }

    public void Play()
    {
        Time.timeScale = 1f;
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("SavedLevel")) == true)
        {
            print("Level set");
            PlayerPrefs.SetString("SavedLevel", "Level 1");
        }

        if(PlayerPrefsController.GetLives() <= 0)
        {
            PlayFromBegining();
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("SavedLevel"));
        }
    }

    public void PlayFromBegining()
    {
        Time.timeScale = 1f;
        PlayerPrefsController.SetLives(3);
        SceneManager.LoadScene("Level 1");
    }

    public void Options()
    {
        optionsCanvas.SetActive(true);
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        if(pauseMenu != null)
        {
            pauseMenu.ChangeGameISPaused(false);
        }
        SceneManager.LoadScene("Main Menu");
    }

    public void Respawn()
    {
        rocket.canMove = true;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
