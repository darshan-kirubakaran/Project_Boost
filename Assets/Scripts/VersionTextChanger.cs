using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersionTextChanger : MonoBehaviour
{
    public TextMeshProUGUI versionText;

    // Start is called before the first frame update
    void Start()
    {
        versionText.text = "V " + Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
