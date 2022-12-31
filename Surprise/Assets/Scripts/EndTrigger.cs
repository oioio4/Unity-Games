using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    GameManager gameManager;
    public GameObject confetti;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter() {
        Vector3 offset = new Vector3(2, 2, 0);
        gameManager.FinishGame();
        Instantiate(confetti, transform.position + offset, transform.rotation);
    }
}
