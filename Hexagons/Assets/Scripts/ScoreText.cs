using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{

    public static float scoreNum = 0f;
    public static float highScoreNum = 0f;
    public Text score;
    public Text hiScore;
    // Start is called before the first frame update
    void Start()
    {
        scoreNum = 0f;
        highScoreNum = PlayerPrefs.GetFloat("hi-score");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreNum += Time.fixedDeltaTime;
        score.text = "" + ((int)scoreNum).ToString();
        if (scoreNum > highScoreNum) {
            highScoreNum = scoreNum;
        }
        hiScore.text = "" + ((int)highScoreNum).ToString();
        PlayerPrefs.SetFloat("hi-score", highScoreNum);
    }
}