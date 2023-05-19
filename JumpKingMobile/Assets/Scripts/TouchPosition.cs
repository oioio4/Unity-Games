using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPosition : MonoBehaviour
{

    public Text touchPosText;
    private Vector2 touchPos;

    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        touchPos = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        touchPos = player.touchPos;

        touchPosText.text = string.Format("({0},{1})", touchPos.x, touchPos.y);
    }
}

