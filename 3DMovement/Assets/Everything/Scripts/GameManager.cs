using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioManager am;

    GlitchEffect glitch;

    bool trigger = false;

    private void Awake() {
        am = FindObjectOfType<AudioManager>();

        glitch = FindObjectOfType<Camera>().GetComponent<GlitchEffect>();
    }

    private void Update() {
        if (trigger) {
            glitch.enabled = true;
        }
        else {
            glitch.enabled = false;
        }
    }
}
