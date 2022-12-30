using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool puzzle1 = false;
    public bool puzzle2 = false;
    public bool puzzle3 = false;
    public bool puzzle4 = false;

    private int pusheens = 0;
    public Text counterText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = pusheens + "/4";
    }
}
