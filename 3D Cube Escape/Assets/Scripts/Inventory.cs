using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // basic tracking of objects in bar
    public bool[] isFull;
    public GameObject[] slots;

    // selecting object from bar
    public GameObject curSelected;
    public bool[] isSelected;
    public Color defaultColor;
    public Color selectedColor;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 0)
                    isSelected[i] = false;
            }
            isSelected[0] = !isSelected[0];
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 1)
                    isSelected[i] = false;
            }
            isSelected[1] = !isSelected[1];
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 2)
                    isSelected[i] = false;
            }
            isSelected[2] = !isSelected[2];         
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 3)
                    isSelected[i] = false;
            }
            isSelected[3] = !isSelected[3];          
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 4)
                    isSelected[i] = false;
            }
            isSelected[4] = !isSelected[4];        
        } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 5)
                    isSelected[i] = false;
            }
            isSelected[5] = !isSelected[5];       
        } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 6)
                    isSelected[i] = false;
            }
            isSelected[6] = !isSelected[6];         
        }

        SwapSelected();
        for (int i = 0; i < isSelected.Length; i++) {
            if (isSelected[i]) {
                curSelected = slots[i];
            }
        }
    }

    private void SwapSelected() {
        // reset all slots to original starting colors
        for (int i = 0; i < slots.Length; i++) {
            if (isSelected[i]) {
                slots[i].GetComponent<Image>().color = selectedColor;
            } else {
                slots[i].GetComponent<Image>().color = defaultColor;
            }
        }
    }
}
