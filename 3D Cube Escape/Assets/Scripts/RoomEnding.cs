using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Escaping the room: the screen glitches and fades to black, the next season unlocks,
// and you're back at the cube select. Start it with Escape(), e.g. from the last puzzle step.
public class RoomEnding : MonoBehaviour
{
    [SerializeField] private string season = "Spring";
    [SerializeField] private string nextScene = "LevelSelection";
    [SerializeField] private float glitchSeconds = 1.5f;
    [SerializeField] private float fadeSeconds = 2f;
    [TextArea] [SerializeField] private string message;

    private bool escaping;

    public bool Escaping => escaping;

    public void Escape() {
        if (escaping) return;
        escaping = true;
        StartCoroutine(Run());
    }

    private IEnumerator Run() {
        if (GameManager.instance != null) GameManager.instance.CompleteSeason(season);
        HintText.Show(message);
        yield return FadeOut(glitchSeconds, fadeSeconds);

        // the cube select needs the mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
    }

    // The camera tears up and goes dark (also the clock's time travel).
    public static IEnumerator FadeOut(float glitchSeconds, float fadeSeconds) {
        // the camera tears up...
        var glitches = FindObjectsByType<GlitchEffect>(FindObjectsSortMode.None);
        for (float t = 0f; t < glitchSeconds; t += Time.deltaTime) {
            foreach (var g in glitches) {
                g.intensity = g.flipIntensity = g.colorIntensity = Mathf.Lerp(0.1f, 1f, t / glitchSeconds);
            }
            yield return null;
        }

        // ...and goes dark
        var fade = BlackScreen();
        for (float t = 0f; t < fadeSeconds; t += Time.deltaTime) {
            fade.color = new Color(0f, 0f, 0f, t / fadeSeconds);
            yield return null;
        }
    }

    private static Image BlackScreen() {
        var root = new GameObject("Fade", typeof(Canvas));
        var canvas = root.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 200;
        var image = new GameObject("Black", typeof(Image)).GetComponent<Image>();
        image.transform.SetParent(root.transform, false);
        var rect = image.rectTransform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = rect.offsetMax = Vector2.zero;
        image.color = new Color(0f, 0f, 0f, 0f);
        return image;
    }
}
