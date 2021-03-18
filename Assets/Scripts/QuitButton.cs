using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    GameObject yourButton;

    private void Start()
    {
        yourButton = this.gameObject;
        Button button = yourButton.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        Application.Quit();
    }
}
