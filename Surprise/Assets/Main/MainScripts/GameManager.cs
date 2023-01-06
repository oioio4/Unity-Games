using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Interactable puzzle1;
    public Interactable puzzle2;
    public Interactable puzzle3;
    public Interactable puzzle4;
    public Interactable puzzle5;

    [SerializeField] private int pusheens = 0;
    [SerializeField] private int cubes = 0;
    private CubeBar cubeBar;
    public Text counterText;
    public GameObject counterAnimation;
    public GameObject pauseMenu;
    [SerializeField] private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        cubeBar = FindObjectOfType<CubeBar>();
        FindObjectOfType<AudioManager>().Play("Theme1");
        pauseMenu.SetActive(false);
        pusheens = 0;
        cubes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = pusheens + "/5";
        cubeBar.targetPower = cubes;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;

            if (paused) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        PauseMenu();
    }

    private void Solved() {
        pusheens++;
        GameObject effect = Instantiate(counterAnimation, counterText.gameObject.transform);
        Destroy(effect, 1f);
        FindObjectOfType<AudioManager>().Play("CollectPusheen");
    }

    public void SolvedPuzzle1() {
        Solved();
        puzzle1.active = false;
        Destroy(puzzle1.particles);
    }

    public void SolvedPuzzle2() {
        Solved();
        puzzle2.active = false;
        Destroy(puzzle2.particles);
    }

    public void SolvedPuzzle3() {
        Solved();
        puzzle3.active = false;
        Destroy(puzzle3.particles);
    }

    public void SolvedPuzzle4() {
        Solved();
        puzzle4.active = false;
        Destroy(puzzle4.particles);
    }

    public void SolvedPuzzle5() {
        Solved();
        puzzle5.active = false;
        Destroy(puzzle5.particles);
    }

    public void CollectedCube() {
        cubes++;
        FindObjectOfType<AudioManager>().Play("CollectCube");
    }

    public bool Completed() {
        return pusheens == 5 && cubes == 10;
    }

    public void OpenedExit() {
        cubes = 0;
    }

    public void GameEnded() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void PauseMenu() {
        if (paused) {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void Resume() {
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Quit() { 
        Application.Quit();
    }
}
