using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour
{

    public Text highScore;
    public static float highscore;
    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "Hi-Score: " + PlayerPrefs.GetInt("HighScore", (int) highscore).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float n = ScoreText.scoreNum;
        if (n > PlayerPrefs.GetInt("HighScore", (int) highscore)) {
            highscore = n;
            highScore.text = "Hi-Score: " + ((int)n).ToString();
        }

    }
}