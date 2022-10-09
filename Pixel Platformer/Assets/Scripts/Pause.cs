using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!pause.activeSelf) {
                Time.timeScale = 0f;
                pause.SetActive(true);
                Cursor.visible = true;
            }
            else {
                resume();
            }
        }
    }

    public void quit() {
        Application.Quit();
    }

    public void resume() {
        Time.timeScale = 1f;
        pause.SetActive(false);
        Cursor.visible = false;
    }
}
