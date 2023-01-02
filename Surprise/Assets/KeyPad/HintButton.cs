using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintButton : MonoBehaviour
{
    public GameObject hint;

    private void Awake() {
        hint.SetActive(false);
        this.gameObject.SetActive(true);
    }

    public void EnableHint() {
        hint.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
