using UnityEngine;
using UnityEngine.SceneManagement;

// Going back in time. Winter's clock can be set to a time from an earlier season, and that
// room opens again, "visiting": a few things are there that you can only find that way.
// What you do in an earlier season is remembered (saved), and the later seasons change to
// match: a seed planted in 1964 is a cactus by 1981. See Echo and TimeTravel.
public class Past : MonoBehaviour
{
    // this room was reached through Winter's clock
    public static bool Visiting { get; private set; }

    private static bool travelling;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init() {
        Visiting = travelling = false;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Visiting = travelling;
        travelling = false;
    }

    // Load an earlier season's room as a visit from Winter.
    public static void Travel(string sceneName) {
        travelling = true;
        SceneManager.LoadScene(sceneName);
    }

    public static bool Remembers(string what) {
        return PlayerPrefs.GetInt("past_" + what, 0) == 1;
    }

    // From a puzzle step's onDone, e.g. Remember("seed") once the seed is planted.
    public void Remember(string what) {
        PlayerPrefs.SetInt("past_" + what, 1);
        PlayerPrefs.Save();
    }
}
