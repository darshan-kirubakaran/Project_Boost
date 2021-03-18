using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Rocket rocket;
    PauseMenu pauseMenu;
    GameObject optionsCanvas;
    Timer timer;


    private void Start()
    {
        optionsCanvas = GameObject.Find("Options Canvas");
        rocket = FindObjectOfType<Rocket>();
        timer = FindObjectOfType<Timer>();
        if(FindObjectOfType<PauseMenu>())
        {
            pauseMenu = FindObjectOfType<PauseMenu>();
        }

        if (SceneManager.GetActiveScene().name == "Game Over")
        {
            PlayerPrefsController.SetLives(3);
            PlayerPrefs.SetString("SavedLevel", "Level 1");
        }
        if(optionsCanvas = GameObject.Find("Options Canvas"))
        {
            optionsCanvas.SetActive(false);
        }
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
        timer.ResetTimer();
        if (pauseMenu != null)
        {
            pauseMenu.ChangeGameISPaused(false);
        }
        SceneManager.LoadScene("Main Menu");
    }
    
    public void BackForLevelsPage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Respawn()
    {
        timer.ResetTimer();
        rocket.canMove = true;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevel()
    {
        timer.ResetTimer();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadLevelsPage()
    {
        SceneManager.LoadScene("Levels Page");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
