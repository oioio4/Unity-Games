using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnd = false;

    public float restartDelay = 0.05f;

    public void EndGame() {
        if (gameEnd == false) {
            gameEnd = true;
            SceneManager.LoadScene(2);
        }

    }
}
