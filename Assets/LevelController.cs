using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] float waitToLoad = 4f;
    [SerializeField] GameObject WinLabel;

    // Start is called before the first frame update
    void Start()
    {
        WinLabel.SetActive(false);
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
}
