using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpStrengthIndicator : MonoBehaviour
{

    public Text jumpStrengthText;
    private float jumpStrength;

    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        jumpStrength = 0;
    }

    // Update is called once per frame
    void Update()
    {
        jumpStrength = player.jumpStrength;
        jumpStrengthText.text = jumpStrength.ToString();
    }
}
