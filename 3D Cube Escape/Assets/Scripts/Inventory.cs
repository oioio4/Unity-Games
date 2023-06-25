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
    public GameObject curSelected = null;
    public int curSlotIndex = 0;
    public bool[] isSelected;
    public Color defaultColor;
    public Color selectedColor;

    // item popup
    public GameObject itemPopup;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 0)
                    isSelected[i] = false;
            }
            isSelected[0] = !isSelected[0];
            SwapSelected();
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 1)
                    isSelected[i] = false;
            }
            isSelected[1] = !isSelected[1];
            SwapSelected();
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 2)
                    isSelected[i] = false;
            }
            isSelected[2] = !isSelected[2];         
            SwapSelected();
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 3)
                    isSelected[i] = false;
            }
            isSelected[3] = !isSelected[3];  
            SwapSelected();        
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 4)
                    isSelected[i] = false;
            }
            isSelected[4] = !isSelected[4];
            SwapSelected();        
        } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 5)
                    isSelected[i] = false;
            }
            isSelected[5] = !isSelected[5]; 
            SwapSelected();      
        } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            for (int i = 0; i < isSelected.Length; i++) {
                if (i != 6)
                    isSelected[i] = false;
            }
            isSelected[6] = !isSelected[6];
            SwapSelected();         
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


        bool selection = false;

        for (int i = 0; i < isSelected.Length; i++) {
            if (isSelected[i]) {
                selection = true;
                curSlotIndex = i;

                // reset image color 
                if (curSelected != null) {
                    curSelected.GetComponent<Image>().color = selectedColor;
                }
                
                // set the current selected slot
                if (slots[i].transform.childCount > 0) {
                    curSelected = slots[i].transform.GetChild(0).gameObject;
                    curSelected.GetComponent<Image>().color = defaultColor;
                } else {
                    curSelected = null;
                }

                // instantiate popup w/ item name
                if (curSelected != null) {
                    GameObject popup = Instantiate(itemPopup, curSelected.transform.position + new Vector3(0f, 40f, 0f), Quaternion.identity);
                    popup.transform.SetParent(curSelected.transform, true);
                    popup.GetComponentInChildren<Text>().text = curSelected.name.Substring(0, curSelected.name.Length - 7);
                    Destroy(popup, 3f);                
                }
            }
        }

        // handles case of selecting and unselecting same slot 
        if (!selection && curSelected != null) {
            curSelected.GetComponent<Image>().color = selectedColor;
            curSelected = null;
        }
    }

    public void DeselectAll() {
        for (int i = 0; i < isSelected.Length; i++) {
            isSelected[i] = false;
        }

        SwapSelected();
    }
}
