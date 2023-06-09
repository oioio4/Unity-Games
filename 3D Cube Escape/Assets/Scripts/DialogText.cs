using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogText : MonoBehaviour
{
    [SerializeField] private int textSpeed;

    public Text dialogText;

    [TextArea]
    public string[] instructions;

    private void Start() {
        StartCoroutine(TypeDialog(instructions));
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
    }
}