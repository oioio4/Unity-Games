using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScoreText : MonoBehaviour
{
    [SerializeField] private string curScore;

    public Text dialogText;
    [SerializeField] private int textSpeed;

    private GameManager gm;

    private void Start() {
        gm = FindObjectOfType<GameManager>();

        float minutes = Mathf.FloorToInt(gm.currentTime / 60) % 60;
        float seconds = Mathf.FloorToInt(gm.currentTime % 60);
        //float milliseconds = Mathf.FloorToInt(t * 1000f) % 1000;

        curScore = string.Format("{0:00}:{1:00}", minutes, seconds);

        StartCoroutine(TypeDialog(curScore));
    }

    public IEnumerator TypeDialog(string dialog) {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray()) {
            dialogText.text += letter;
            FindObjectOfType<AudioManager>().Play("Text");
            yield return new WaitForSeconds(1f / textSpeed);
        }
    }
}
