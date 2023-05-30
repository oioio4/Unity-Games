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

    public GameObject pauseMenu;
    [SerializeField] private bool paused = false;

    public float currentTime;
    private float highScoreTime = float.MaxValue;

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

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            PauseMenu();
        }
    }

    public void levelIncrement() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void levelDecrement() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
        PauseMenu();
    }

    public void Quit() { 
        Application.Quit();
    }

    public void Save() {
        TimerText timer = FindObjectOfType<TimerText>();
        currentTime = timer.t;

        if (currentTime < highScoreTime) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
            
            PlayerData data = new PlayerData();
            data.fastestTime = currentTime;

            bf.Serialize(file, data);
            file.Close();
        }
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            highScoreTime = data.fastestTime;
        }
    }

    [Serializable]
    class PlayerData {
        public float fastestTime;
    }
}
