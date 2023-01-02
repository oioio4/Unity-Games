using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    float[] rotations = {0, 90, 180, 270};

    public float[] correctRotations;
    public bool correct = false;

    public float curRotation;

    private void Start() {
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        if (correctRotations.Length > 1) {
            if (transform.eulerAngles.z == correctRotations[0] || transform.eulerAngles.z == correctRotations[1]) {
                correct = true;
            }
        }
        else {
            if (transform.eulerAngles.z == correctRotations[0]) {
                correct = true;
            }
        }
    }

    private void Update() {
        curRotation = transform.eulerAngles.z;
    }

    public void RotatePipe() {
        transform.Rotate(new Vector3(0, 0, 90));
        transform.eulerAngles = new Vector3(0, 0, Mathf.Round(transform.eulerAngles.z));

        if (correctRotations.Length > 1) {
            if ((transform.eulerAngles.z == correctRotations[0] || transform.eulerAngles.z == correctRotations[1]) && !correct) {
                correct = true;
            }
            else if (correct) {
                correct = false;
            }
        }
        else {
            if (transform.eulerAngles.z == correctRotations[0] && !correct) {
                correct = true;
            }
            else if (correct) {
                correct = false;
            }
        }
    }
}
