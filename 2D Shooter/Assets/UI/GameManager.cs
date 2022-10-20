using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnd = false;

    public PlayerControl player;

    public void EndGame() {
        if (gameEnd == false) {
            gameEnd = true;
            SceneManager.LoadScene(2);
            SavePlayer();
        }

    }

    public void SavePlayer() {
        SaveSystem.SavePlayer(player);
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();
        player.maxHealth = data.health;
        player.moveSpeed = data.moveSpeed;
    }


}
