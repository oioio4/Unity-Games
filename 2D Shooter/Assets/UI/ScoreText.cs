using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{

    public static int scoreNum = 0;
    Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float t = Time.time;
        int seconds = (int)(t % 60);
        int prev = 0;
        if (seconds == prev + 1) {
            scoreNum++;
            prev = seconds;
        }
        */
        score.text = "Score: " + scoreNum;
    }
}
