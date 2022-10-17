using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    private float startTime = 0f;
    Text timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        startTime = Time.timeSinceLevelLoad;

        string minutes = ((int) startTime / 60).ToString();
        string seconds = (startTime % 60).ToString("f2");

        timer.text = minutes + ":" + seconds;
    }
}