using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            paused = !paused;
        }

        if (paused) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Pause();
    }

    private void Pause() {
        if (paused) {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void Resume() {
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Quit() { 
        Application.Quit();
    }
}
