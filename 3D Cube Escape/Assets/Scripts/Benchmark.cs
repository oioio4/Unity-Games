using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

// Frame-time benchmark: turns the player once round on the spot and logs frame times and draw
// stats. Nothing happens unless asked: start a build with "-benchmark SpringLevel,WinterLevel"
// (it plays each room, logs "[Benchmark] ...", then quits), open the web build with
// "?benchmark=SpringLevel" on its URL (results go to the browser console), or call
// Benchmark.Run() in Play mode.
public class Benchmark : MonoBehaviour
{
    public static string LastResult;

    private float seconds = 12f;
    private static Queue<string> pendingScenes;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void FromCommandLine() {
        string list = null;
        string[] args = System.Environment.GetCommandLineArgs();
        int i = System.Array.IndexOf(args, "-benchmark");
        if (i >= 0 && i + 1 < args.Length) list = args[i + 1];
        string url = Application.absoluteURL ?? "";
        int q = url.IndexOf("benchmark=");
        if (q >= 0) list = url.Substring(q + "benchmark=".Length).Split('&', '#')[0];
        if (string.IsNullOrEmpty(list)) return;
        pendingScenes = new Queue<string>(list.Split(','));
        SceneManager.sceneLoaded += (scene, mode) => { if (pendingScenes != null && scene.name.EndsWith("Level")) Run(); };
        SceneManager.LoadScene(pendingScenes.Dequeue());
    }

    public static void Run(float seconds = 12f) {
        var go = new GameObject("Benchmark");
        go.AddComponent<Benchmark>().seconds = seconds;
    }

    private IEnumerator Start() {
        // the first seconds after the room starts: the worst frame is any stutter as it settles
        float settleWorst = 0f;
        for (float t = 0f; t < 3f; t += Time.unscaledDeltaTime) {
            yield return null;
            settleWorst = Mathf.Max(settleWorst, Time.unscaledDeltaTime * 1000f);
        }

        var player = GameObject.Find("Player");
        foreach (var b in player.GetComponents<MonoBehaviour>()) {
            if (b.GetType().Name == "FPSController") b.enabled = false; // it would hold the view still
        }
        var batches = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Batches Count");
        var setPass = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
        var tris = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
        var casters = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Shadow Casters Count");

        var frames = new List<float>();
        long sumBatches = 0, sumSetPass = 0, sumTris = 0, sumCasters = 0;
        float startYaw = player.transform.eulerAngles.y;
        for (float t = 0f; t < seconds; t += Time.unscaledDeltaTime) {
            player.transform.rotation = Quaternion.Euler(0f, startYaw + 360f * t / seconds, 0f);
            yield return null;
            frames.Add(Time.unscaledDeltaTime * 1000f);
            sumBatches += batches.LastValue; sumSetPass += setPass.LastValue; sumTris += tris.LastValue; sumCasters += casters.LastValue;
        }
        batches.Dispose(); setPass.Dispose(); tris.Dispose(); casters.Dispose();

        frames.Sort();
        float avg = 0f;
        foreach (float f in frames) avg += f;
        avg /= frames.Count;
        int n = frames.Count;
        LastResult = string.Format("{0}: {1} frames, avg {2:F1} ms ({3:F0} fps), 95th pct {4:F1} ms, worst {5:F1} ms (worst while settling {10:F0} ms); per frame: {6} batches, {7} setpass, {8}k tris, {9} shadow casters",
            SceneManager.GetActiveScene().name, n, avg, 1000f / avg, frames[(int)(n * 0.95f)], frames[n - 1],
            sumBatches / n, sumSetPass / n, sumTris / n / 1000, sumCasters / n, settleWorst);
        Debug.Log("[Benchmark] " + LastResult);

        if (pendingScenes != null) {
            if (pendingScenes.Count > 0) SceneManager.LoadScene(pendingScenes.Dequeue());
            else Application.Quit();
        }
        Destroy(gameObject);
    }
}
