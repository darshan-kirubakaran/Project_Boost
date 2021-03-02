using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleten : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        //SetUpSingelton();    
    }

    private void SetUpSingelton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
