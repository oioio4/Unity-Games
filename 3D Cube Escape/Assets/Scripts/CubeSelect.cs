using UnityEngine;
using UnityEngine.SceneManagement;

// One of the season cubes on the level select. Click an unlocked cube to enter its room.
public class CubeSelect : MonoBehaviour
{
    [SerializeField] private string cubeSceneName;

    public string Season => GameManager.SeasonOf(cubeSceneName);
    public bool Unlocked => GameManager.IsUnlocked(Season);

    private void OnMouseOver() {
        if (!Input.GetMouseButtonDown(0)) return;

        if (!Unlocked) {
            HintText.Show("Not yet.");
        }
        else if (!Application.CanStreamedLevelBeLoaded(cubeSceneName)) {
            HintText.Show(Season + " is still being built.");
        }
        else {
            SceneManager.LoadScene(cubeSceneName);
        }
    }
}
