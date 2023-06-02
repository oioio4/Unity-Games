using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTimer : MonoBehaviour
{
    public float timer = 20f;
    public string sceneName = "Main";

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 || Input.GetKeyDown("space")) {
            SceneManager.LoadScene(sceneName);
        }
    }
}
