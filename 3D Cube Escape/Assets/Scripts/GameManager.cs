using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private AudioManager am;

    public GameObject pauseMenu;
    [SerializeField] private bool paused = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        pauseMenu.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            PauseMenu();

            if (paused) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    // pause menu stuff 
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
        PauseMenu();
    }

    public void Quit() { 
        Application.Quit();
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

    // saving and loading system
    public void Save() {
        //TimerText timer = FindObjectOfType<TimerText>();
        //currentTime = timer.t;
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        
        PlayerData data = new PlayerData();
        //data.fastestTime = currentTime;

        bf.Serialize(file, data);
        file.Close();

    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //highScoreTime = data.fastestTime;
        }
    }

    [Serializable]
    class PlayerData {
        //public float fastestTime;
    }
}
