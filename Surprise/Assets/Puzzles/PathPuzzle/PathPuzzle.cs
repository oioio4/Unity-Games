using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PathPuzzle : MonoBehaviour
{
    public List<Path> paths;

    public UnityEvent winAction;

    private void Awake() {
        for (int i = 0; i < paths.Count; i++) {
            paths[i].activated = false;
        }
    }

    private void Update() {
        bool won = true;
        for (int i = 0; i < paths.Count; i++) {
            if (paths[i].activated == false) {
                won = false;
            }
        }

        if (won) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            winAction.Invoke();
        }
    }

    public void SwapSelected(int pipeNum) {
        paths[pipeNum].Switch();
    }
}
