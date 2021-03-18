using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour
{
    [SerializeField] SceneAsset levelScene; 
    string levelNumber; 
    TextMeshProUGUI levelNumberText; 
    TextMeshProUGUI timeText;
    float msec;
    float sec;
    float min;

    GameObject yourButton;

    // Start is called before the first frame update
    void Start()
    {

        levelNumberText = this.transform.Find("Level Number Text").GetComponent<TextMeshProUGUI>();
        timeText = this.transform.Find("Time Text").GetComponent<TextMeshProUGUI>();

        yourButton = this.gameObject;
        levelNumber = levelScene.name.ToString().Substring(levelScene.name.ToString().Length - 2);
        levelNumberText.text = levelNumber;
        Button button = yourButton.GetComponent<Button>(); 
        button.onClick.AddListener(TaskOnClick);

        msec = PlayerPrefs.GetFloat(levelScene.name);
        sec = (int)(msec / 1000);
        min = (int)(sec / 60);
        sec = (int)sec % 60;
        msec = (int)msec % 1000;

        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
    }

    //public void SetDash()
    //{
      //  timeText.text = "-";
    //}

    private void TaskOnClick()
    {
        if (PlayerPrefsController.GetLives() <= 0)
        {
            Time.timeScale = 1f;
            PlayerPrefsController.SetLives(3);
            SceneManager.LoadScene(levelScene.name);
        }
        else
        {
            SceneManager.LoadScene(levelScene.name);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelScene.name);
    }

    
}
