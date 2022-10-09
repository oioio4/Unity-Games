using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public GameObject start;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            player.transform.position = start.transform.position;
        }
    }
}
