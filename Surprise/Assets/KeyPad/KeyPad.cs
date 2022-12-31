using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class KeyPad : MonoBehaviour
{
    private int digit1 = 0;
    private int digit2 = 0;
    private int digit3 = 0;
    private int digit4 = 0;
    private int counter = 0;
    private int ans1 = 1;
    private int ans2 = 2;
    private int ans3 = 4;
    private int ans4 = 3;

    public Text numberText;
    public UnityEvent winAction;

    private void Update() {
        numberText.text = digit1 + " " + digit2 + " " + digit3 + " " + digit4;
    }

    public void addNum(int number) {
        if (counter == 0) {
            digit1 = number;
            counter++;
        }
        else if (counter == 1) {
            digit2 = number;
            counter++;
        }
        else if (counter == 2) {
            digit3 = number;
            counter++;
        }
        else if (counter == 3) {
            digit4 = number;
            counter++;
        }
    }

    public void Clear() {
        counter = 0;
        digit1 = 0;
        digit2 = 0;
        digit3 = 0;
        digit4 = 0;
    }

    public void Submit() {
        if (digit1 == ans1 && digit2 == ans2 && digit3 == ans3 && digit4 == ans4) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            winAction.Invoke();
        }
        else {
            Clear();
        }
    }
}
