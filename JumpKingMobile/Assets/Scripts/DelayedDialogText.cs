using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DelayedDialogText : MonoBehaviour
{
    [SerializeField] private int textSpeed;
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float endDelay = 0.5f;

    public Text dialogText;

    [TextArea]
    public string[] instructions;

    private void Start() {
        StartCoroutine(TypeDialog(instructions));
    }

    public IEnumerator TypeDialog(string[] dialog) {
        yield return new WaitForSeconds(startDelay);
        dialogText.text = "";
        for (int i = 0; i < dialog.Length; i++) {
            foreach (var letter in dialog[i].ToCharArray()) {
                dialogText.text += letter;
                FindObjectOfType<AudioManager>().Play("Text");
                yield return new WaitForSeconds(1f / textSpeed);
            }

            yield return new WaitForSeconds(endDelay);
            dialogText.text = "";  
        }
    }
}