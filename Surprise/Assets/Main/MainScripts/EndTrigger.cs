using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    GameManager gameManager;
    public GameObject confetti;

    private bool ended = false;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter() {
        if (!ended) {
            Vector3 offset = new Vector3(2, 2, 0);
            FindObjectOfType<AudioManager>().Stop("Theme1");
            FindObjectOfType<AudioManager>().Play("PartyHorn");
            Instantiate(confetti, transform.position + offset, transform.rotation);
            StartCoroutine(FindObjectOfType<ExitDoor>().closing());
            StartCoroutine(BirthdaySong());
            ended = true;
        }
    }

    private IEnumerator BirthdaySong() {
        yield return new WaitForSeconds(2f);

        FindObjectOfType<AudioManager>().Play("HappyBirthday");

        yield return new WaitForSeconds(20f);

        FindObjectOfType<AudioManager>().Play("EndTheme");

        FindObjectOfType<GameManager>().GameEnded();
    }
}
