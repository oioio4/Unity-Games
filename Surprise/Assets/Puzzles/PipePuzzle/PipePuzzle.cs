using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PipePuzzle : MonoBehaviour
{
    public GameObject PipeHolder;
    public GameObject[] Pipes;

    [SerializeField] private int totalPipes = 0;

    public UnityEvent winAction;

    private void Start() {
        totalPipes = PipeHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++) {
            Pipes[i] = PipeHolder.transform.GetChild(i).gameObject;
        }
    }

    private void Update() {
        bool won = true;
        for (int i = 0; i < Pipes.Length; i++) {
            if (Pipes[i].GetComponent<Pipe>().correct == false) {
                won = false;
            }
        }

        if (won) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            winAction.Invoke();
        }
    }
}
