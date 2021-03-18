using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveButton : MonoBehaviour
{
    GameObject yourButton;
    LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        yourButton = this.gameObject;
        Button button = yourButton.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        levelController.ReviveButton();
    }
}
