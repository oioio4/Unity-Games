using UnityEngine;
using UnityEngine.SceneManagement;

// For UI buttons: load a scene by name, e.g. the start menu's Play -> "LevelSelection".
public class SceneLink : MonoBehaviour
{
    public void Load(string sceneName) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
