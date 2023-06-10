using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameras : MonoBehaviour
{
    public Camera[] cams;
    private int camIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        camIndex = 0;

        for (int i = 1; i < cams.Length; i++) {
            cams[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            cams[camIndex].enabled = false;

            camIndex = (camIndex + 1) % cams.Length;

            cams[camIndex].enabled = true;
        }
    }

    public void SwapCamera(Camera cam){
         // performance tradeoff
         foreach(Camera c in cams){
                 c.enabled = false;
         }
         
         cam.enabled = true;
    }
}
