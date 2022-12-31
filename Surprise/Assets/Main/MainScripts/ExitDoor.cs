using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public Animator openandclose;
    public bool open;
    public Transform Player;
    private GameManager gameManager;

    private void Start()
    {
        open = false;
        gameManager = FindObjectOfType<GameManager>();
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
