using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelChange : MonoBehaviour
{
    public UnityEvent changeLevel;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            changeLevel.Invoke();;
        }
    }
}
