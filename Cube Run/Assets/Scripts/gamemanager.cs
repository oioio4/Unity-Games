using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    bool gameEnd = false;

    public float restartDelay = 1f;

    public GameObject completeUI;

    public void completeLevel() {
        completeUI.SetActive(true);
    }
    public void EndGame() {
        if (gameEnd == false) {
            gameEnd = true;
            Invoke("Restart", restartDelay);
        }

    }
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
