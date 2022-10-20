using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public float health;
    public float moveSpeed;

    public PlayerData(PlayerControl player) {
        health = player.maxHealth;
        moveSpeed = player.moveSpeed;
    }
}
