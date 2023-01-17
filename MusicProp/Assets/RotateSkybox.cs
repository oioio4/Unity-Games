using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    public float rotateSpeed = 1.2f;

    private void Update() {
        RenderSettings.skybox.SetFloat("_Rotation", Time.deltaTime * rotateSpeed);
    }
}
