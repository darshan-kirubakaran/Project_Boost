using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] float waitToLoad = 4f;
    [SerializeField] float levelLoadDelay = 2f;
    GameObject WinLabel;
    GameObject LooseLabel;
    GameObject Revive;
    [SerializeField] float ReviveTime = 10;
    float StartTime = 0;

    SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        WinLabel = GameObject.Find("Win Label");
        LooseLabel = GameObject.Find("Loose Label");
        Revive = GameObject.Find("Revive");

        StartTime = 0;

        WinLabel.SetActive(false);
        LooseLabel.SetActive(false);
        Revive.SetActive(false);
    }

    public void HandleWinCondition()
    {
        StartCoroutine(WinCondition());
    }

    public IEnumerator WinCondition()
    {
        WinLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waitToLoad);
        FindObjectOfType<SceneLoader>().LoadNextLevel();
    }

    public void HandleLooseCondition()
    {
        StartCoroutine(LooseCondition());
    }

    public IEnumerator LooseCondition()
    {
        Revive.SetActive(true);
        while (StartTime < ReviveTime)
        {
            StartTime += 1;
            GameObject.Find("Fill Bar").GetComponent<Image>().fillAmount = StartTime / ReviveTime;
            yield return new WaitForSeconds(1);
        }
        Revive.SetActive(false);
        LooseLabel.SetActive(true);
    }

    public void ReviveButton()
    {
        //WinLabel.SetActive(false);
        //LooseLabel.SetActive(false);
        //Revive.SetActive(false);

        PlayerPrefsController.SetLives(1);
        sceneLoader.Invoke("Respawn", 0);
    }
}
