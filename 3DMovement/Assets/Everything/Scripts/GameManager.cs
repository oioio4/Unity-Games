using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioManager am;

    GlitchEffect glitch;

    private void Awake() {
        am = FindObjectOfType<AudioManager>();
    }

    private void Update() {
    }
}
