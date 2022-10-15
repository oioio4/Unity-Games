using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{

    public static float scoreNum = 0f;
    public float pointsPerTime = 1f;
    Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreNum += pointsPerTime * Time.deltaTime;
        score.text = "Score: " + (int) scoreNum;
    }
}
