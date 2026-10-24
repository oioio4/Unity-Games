using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// A clock's face. In Winter: set the hands to a time you saw on the clock in an earlier
// season, click the face, and you're back in that season (Past.Travel). In that earlier
// season, while visiting, click it again to go forward to Winter.
public class TimeTravel : Interactable
{
    [Tooltip("Winter: the hands, read as \"hours:minutes\".")]
    [SerializeField] private CodeWheel hours;
    [SerializeField] private CodeWheel minutes;
    [SerializeField] private string[] times = new string[0];   // e.g. "9:05"
    [SerializeField] private string[] scenes = new string[0];  // e.g. "SpringLevel", one per time
    [Tooltip("Earlier seasons: where the clock goes forward to, e.g. \"WinterLevel\".")]
    [SerializeField] private string returnTo;
    [TextArea] [SerializeField] private string travelHint;

    private bool going;

    public override void InteractionEvent() {
        if (going) return;
        string target = null;
        bool back = string.IsNullOrEmpty(returnTo);
        if (!back) {
            if (Past.Visiting) target = returnTo;
        }
        else if (hours != null && minutes != null) {
            int i = System.Array.IndexOf(times, hours.Value + ":" + minutes.Value);
            if (i >= 0 && i < scenes.Length) target = scenes[i];
        }

        if (target == null) {
            Say(description);
            return;
        }
        going = true;
        StartCoroutine(Go(target, back));
    }

    private IEnumerator Go(string sceneName, bool back) {
        Say(travelHint);
        yield return RoomEnding.FadeOut(1.2f, 1f);
        Time.timeScale = 1f;
        if (back) Past.Travel(sceneName);
        else SceneManager.LoadScene(sceneName);
    }
}
