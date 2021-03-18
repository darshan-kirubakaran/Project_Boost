using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] Image[] hearts;
    [SerializeField] TextMeshProUGUI livesText;

    SceneLoader sceneLoader;
    LevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        levelController = FindObjectOfType<LevelController>();

        PlayerPrefs.SetString("SavedLevel", SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        // Update Lives Text
        livesText.text = PlayerPrefsController.GetLives().ToString();
    }

    public void DeacreaseLives()
    {
        PlayerPrefsController.SetLives(PlayerPrefsController.GetLives() - 1);

        if (PlayerPrefsController.GetLives() <= 0)
        {
            levelController.HandleLooseCondition();
        }
        else
        {
            sceneLoader.Invoke("Respawn", levelLoadDelay);
        }
    }

    public void AddToNumberOfGamesPlayed(int GameNumber)
    {
        PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + GameNumber);
    }
}
