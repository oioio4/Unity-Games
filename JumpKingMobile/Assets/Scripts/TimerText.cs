using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    private Text timerText;

    private float t;

    private void Awake() {
        timerText = GetComponent<Text>();
        
        t = 0;
    }

    private void Update() {
        t += Time.deltaTime;

        float minutes = Mathf.FloorToInt(t / 60) % 60;
        float seconds = Mathf.FloorToInt(t % 60);
        float milliseconds = Mathf.FloorToInt(t * 1000f) % 1000;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
