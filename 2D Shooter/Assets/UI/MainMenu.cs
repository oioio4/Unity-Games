using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Begin() {
        Time.timeScale = 1f;
        PauseMenu.GamePaused = false;
        SceneManager.LoadScene(1);
    }
}
