using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NextScene() {
        StartCoroutine(WaitToSwitch());
        FindObjectOfType<AudioManager>().Play("StartGame");
        FindObjectOfType<AudioManager>().Stop("OpeningTheme");
    }

    private IEnumerator WaitToSwitch() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
