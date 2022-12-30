using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool p1 = false;
    private bool p2 = false;
    private bool p3 = false;
    private bool p4 = false;

    public Interactable puzzle1;
    public Interactable puzzle2;
    public Interactable puzzle3;
    public Interactable puzzle4;

    private int pusheens = 0;
    public Text counterText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = pusheens + "/4";
    }

    public void SolvedPuzzle1() {
        p1 = true;
        pusheens++;
        Destroy(puzzle1.particles);
    }

    public bool Completed() {
        return pusheens == 4;
    }

    public void FinishGame() {

    }
}
