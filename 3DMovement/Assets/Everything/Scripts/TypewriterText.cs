using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypewriterText : MonoBehaviour
{
    [SerializeField] private int textSpeed;

    public Text dialogText;
    public GameObject continueButton;

    [TextArea]
    public string[] instructions;

    private void Start() {
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
}
