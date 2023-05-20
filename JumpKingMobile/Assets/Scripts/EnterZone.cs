using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnterZone : MonoBehaviour
{
    public TMP_Text zoneText;
    public Animator zoneAnimator;
    public string zoneName;

    public bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            zoneText.text = zoneName;
            triggered = true;

            zoneAnimator.Play("Appear");
        }
    }
}
