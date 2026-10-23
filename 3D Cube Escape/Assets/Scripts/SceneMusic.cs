using UnityEngine;

// Plays this scene's theme on the AudioManager (which lives across scenes) and stops the others.
public class SceneMusic : MonoBehaviour
{
    private static readonly string[] Themes = { "MainMenu", "Spring", "Summer", "Fall", "Winter" };

    [SerializeField] private string theme = "Spring";

    private void Start() {
        var audio = AudioManager.instance;
        if (audio == null) return;
        foreach (string t in Themes) {
            if (t != theme) audio.Stop(t);
        }
        audio.Play(theme);
    }
}
