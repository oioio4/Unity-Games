using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
        pusheens++;
        puzzle1.active = false;
        Destroy(puzzle1.particles);
    }

    public void SolvedPuzzle2() {
        pusheens++;
        puzzle2.active = false;
        Destroy(puzzle2.particles);
    }

    public void SolvedPuzzle3() {
        pusheens++;
        puzzle3.active = false;
        Destroy(puzzle3.particles);
    }

    public void SolvedPuzzle4() {
        pusheens++;
        puzzle4.active = false;
        Destroy(puzzle4.particles);
    }

    public bool Completed() {
        return pusheens == 4;
    }

    public void FinishGame() {

    }
}
