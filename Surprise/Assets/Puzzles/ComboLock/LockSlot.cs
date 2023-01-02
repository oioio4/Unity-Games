using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockSlot : MonoBehaviour
{
    private Text numText;
    private int num = 0;

    private void Awake() {
        numText = GetComponentInChildren<Text>();
        num = 0;
        numText.text = num.ToString();
    }

    public void SwapNumbers() {
        if (num < 9) {
            num++;
        }
        else {
            num = 0;
        }

        numText.text = num.ToString();
    }

    public int getNum() {
        return num;
    }
}
