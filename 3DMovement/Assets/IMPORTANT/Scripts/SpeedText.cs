using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedText : MonoBehaviour
{
    public Rigidbody rb;
    private Text speedText;

    private void Awake() {
        speedText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = rb.velocity.magnitude.ToString("F2");
    }
}
