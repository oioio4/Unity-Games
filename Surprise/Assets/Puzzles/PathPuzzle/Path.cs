using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject active;
    public GameObject inactive;
    public bool activated = false;

    private void Awake() {
        activated = false;
    }

    private void Update() {
        if (activated) {
            active.SetActive(true);
            inactive.SetActive(false);
        }
        else {
            active.SetActive(false);
            inactive.SetActive(true);
        }
    }

    public void Switch() {
        activated = !activated;
    }
}
