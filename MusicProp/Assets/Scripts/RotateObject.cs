using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 1.2f;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(new Vector3 (Time.deltaTime * rotationSpeed, 0, 0));
    }
}
