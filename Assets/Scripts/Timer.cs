using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time;
    TextMeshProUGUI timer;
    float msec;
    float sec;
    float min;

    public void Start()
    {
        timer = this.GetComponent<TextMeshProUGUI>();
        StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine("StopWatch");
    }

    public void StopTimer()
    {
        StopCoroutine("StopWatch");
    }

    public void ResetTimer()
    {
        time = 0;
        timer.text = "00:00:00";
    }

    public string ReturnTime()
    {
        return string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
    }

    public float Returnmsec()
    {
        float finalmsec = msec + (sec * 1000) + ((min * 60)* 60);
        return (int)finalmsec;
    }

    IEnumerator StopWatch()
    {
        while (true)
        {
            time += Time.deltaTime;
            msec = (int)((time - (int)time) * 100);
            sec = (int)(time % 60);
            min = (int)(time / 60 % 60);

            timer.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);

            yield return null;
        }
    }
}
