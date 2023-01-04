using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogText : MonoBehaviour
{
    [SerializeField] private int textSpeed;

    public Text dialogText;
    public GameObject continueButton;

    [TextArea]
    public string[] instructions;

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(TypeDialog(instructions));
        continueButton.SetActive(false);
    }

    public IEnumerator TypeDialog(string[] dialog) {
        dialogText.text = "";
        for (int i = 0; i < dialog.Length; i++) {
            foreach (var letter in dialog[i].ToCharArray()) {
                dialogText.text += letter;
                FindObjectOfType<AudioManager>().Play("Text");
                yield return new WaitForSeconds(1f / textSpeed);
            }

            yield return new WaitForSeconds(2f);
            dialogText.text = "";  
        }

        continueButton.SetActive(true);
    }

    public void ContinueToGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndGame() {
        Application.Quit();
    }
}
