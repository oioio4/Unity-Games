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

    // spring stuff
    public bool spring = true;
    public GameObject[] springSlots;
    // flags (for interactions)
    public bool logFlag = false;

    // summer stuff
    public bool summer = false;
    public GameObject[] summerSlots;

    // fall stuff
    public bool fall = false;
    public GameObject[] fallSlots;

    // winter stuff
    public bool winter = false;
    public GameObject[] winterSlots;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
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
