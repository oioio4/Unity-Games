using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationText : MonoBehaviour
{
    public PlayerCam pCam;
    private Text rotationText;

    private void Awake() {
        rotationText = GetComponent<Text>();
    }

    private void Update() {
        rotationText.text = "X: " + pCam.xRotation.ToString("F2") + " | " + "Y: " + pCam.yRotation.ToString("F2");
    }
}
