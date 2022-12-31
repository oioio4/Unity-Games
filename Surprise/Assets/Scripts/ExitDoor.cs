using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public Animator openandclose;
    public bool open;
    public Transform Player;
    private GameManager gameManager;

    private bool isInRange;

    private void Start()
    {
        open = false;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        bool playerExists = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var collider in colliders) {
            if (collider.tag == "Player") {
                isInRange = true;
                playerExists = true;
            }
            else if (!playerExists) {
                isInRange = false;
            }
        }
        playerExists = false;
    }

    void OnMouseOver()
    {
        {
            if (Player)
            {
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist < 15)
                {
                    if (open == false && gameManager.Completed())
                    {
                        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
                        {
                            StartCoroutine(opening());
                        }
                    }
                }
            }

        }

    }

    IEnumerator opening()
    {
        print("you are opening the door");
        openandclose.Play("Opening");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator closing()
    {
        print("you are closing the door");
        openandclose.Play("Closing");
        open = false;
        yield return new WaitForSeconds(.5f);
    }
}
