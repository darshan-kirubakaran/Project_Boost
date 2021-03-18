using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] float waitToLoad = 4f;
    GameObject WinLabel;
    GameObject LooseLabel;
    GameObject Revive;
    [SerializeField] float ReviveTime = 10;
    float StartTime = 0;

    // Start is called before the first frame update
    void Start()
    {
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
            Revive.transform.FindChild("Fill Bar").GetComponent<Image>().fillAmount = StartTime / ReviveTime;
            yield return new WaitForSeconds(1);
        }
        Revive.SetActive(false);
        LooseLabel.SetActive(true);
    }
}
