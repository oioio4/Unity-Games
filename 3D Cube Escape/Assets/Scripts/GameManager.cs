using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Lives across scenes. Tracks what has happened in the current room (puzzle flags)
// and which seasons are unlocked (saved between sessions).
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // In play order: escaping one room unlocks the next. Scenes are named "<Season>Level".
    public static readonly string[] Seasons = { "Spring", "Summer", "Fall", "Winter" };

    // Things that have happened in this room, e.g. "fireLit" or "birdFed".
    private readonly HashSet<string> flags = new HashSet<string>();

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy() {
        if (instance == this) SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // every room starts from scratch, even when you come back to it
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        flags.Clear();
    }

    // puzzle flags
    public bool HasFlag(string flag) {
        return !string.IsNullOrEmpty(flag) && flags.Contains(flag);
    }

    public void SetFlag(string flag) {
        if (!string.IsNullOrEmpty(flag)) flags.Add(flag);
    }

    // seasons
    public static string SeasonOf(string sceneName) {
        return sceneName.EndsWith("Level") ? sceneName.Substring(0, sceneName.Length - "Level".Length) : sceneName;
    }

    public static bool IsUnlocked(string season) {
        return season == Seasons[0] || PlayerPrefs.GetInt("unlocked_" + season, 0) == 1;
    }

    // Escaped a room: open up the next season and remember it.
    public void CompleteSeason(string season) {
        int i = System.Array.IndexOf(Seasons, season);
        if (i >= 0 && i + 1 < Seasons.Length) PlayerPrefs.SetInt("unlocked_" + Seasons[i + 1], 1);
        PlayerPrefs.SetInt("completed_" + season, 1);
        PlayerPrefs.Save();
    }

    public static bool IsCompleted(string season) {
        return PlayerPrefs.GetInt("completed_" + season, 0) == 1;
    }

    // scene loading stuff
    public void SceneLoad(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void SceneIncrement() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SceneDecrement() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
