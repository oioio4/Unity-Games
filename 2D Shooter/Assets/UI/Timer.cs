using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    private float startTime;
    Text timer;
    // Start is called before the first frame update
    void Start()
    {
        startTime = 0f;
        timer = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;

        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timer.text = minutes + ":" + seconds;
    }
}