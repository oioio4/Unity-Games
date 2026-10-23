using UnityEngine;
using UnityEngine.UI;

// The hotbar at the bottom of the screen. Each slot holds one item's icon; keys 1-0
// (or the mouse wheel) select the item you'll use on whatever you click next.
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

    // what's in each slot, e.g. "matches" (from the icon prefab's name)
    private string[] items;

    private static readonly KeyCode[] slotKeys = {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0
    };

    // The item currently selected in the hotbar, or null.
    public string SelectedItem => curSelected != null ? items[curSlotIndex] : null;

    // An item's id is its icon prefab's name, lower case: "Matches" -> "matches".
    public static string IdOf(GameObject icon) {
        return icon.name.Replace("(Clone)", "").Trim().ToLowerInvariant();
    }

    private void Awake() {
        items = new string[slots.Length];
    }

    private void Update() {
        if (Time.timeScale == 0f) return; // paused

        for (int i = 0; i < slotKeys.Length && i < slots.Length; i++) {
            if (Input.GetKeyDown(slotKeys[i])) {
                Toggle(i);
                return;
            }
        }

        // the wheel steps through the slots that have something in them
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0f) {
            int start = curSelected != null ? curSlotIndex : (scroll < 0f ? -1 : slots.Length);
            int step = scroll < 0f ? 1 : -1;
            for (int n = 1; n <= slots.Length; n++) {
                int i = ((start + step * n) % slots.Length + slots.Length) % slots.Length;
                if (isFull[i]) {
                    Select(i);
                    break;
                }
            }
        }
    }

    // Pressing a slot's key selects it, pressing it again puts the item away.
    private void Toggle(int index) {
        if (isSelected[index]) DeselectAll();
        else Select(index);
    }

    public void Select(int index) {
        for (int i = 0; i < isSelected.Length; i++) isSelected[i] = i == index;
        SwapSelected();
    }

    private void SwapSelected() {
        // reset all slots to original starting colors
        for (int i = 0; i < slots.Length; i++) {
            slots[i].GetComponent<Image>().color = isSelected[i] ? selectedColor : defaultColor;
        }

        // put the previous item's icon back to normal
        if (curSelected != null) curSelected.GetComponent<Image>().color = selectedColor;
        curSelected = null;

        for (int i = 0; i < isSelected.Length; i++) {
            if (!isSelected[i]) continue;
            curSlotIndex = i;

            // set the current selected slot
            if (isFull[i] && slots[i].transform.childCount > 0) {
                curSelected = slots[i].transform.GetChild(0).gameObject;
                curSelected.GetComponent<Image>().color = defaultColor;

                // instantiate popup w/ item name
                GameObject popup = Instantiate(itemPopup, curSelected.transform.position + new Vector3(0f, 40f, 0f), Quaternion.identity);
                popup.transform.SetParent(curSelected.transform, true);
                popup.GetComponentInChildren<Text>().text = curSelected.name.Replace("(Clone)", "").Trim();
                Destroy(popup, 3f);
            }
        }
    }

    public void DeselectAll() {
        for (int i = 0; i < isSelected.Length; i++) {
            isSelected[i] = false;
        }

        SwapSelected();
    }

    // Put an item in the first free slot. False if the hotbar is full.
    public bool Add(GameObject icon) {
        for (int i = 0; i < slots.Length; i++) {
            if (isFull[i]) continue;
            isFull[i] = true;
            items[i] = IdOf(icon);
            Instantiate(icon, slots[i].transform, false);
            return true;
        }
        return false;
    }

    public void DestroyCur() {
        if (curSelected == null) return;
        curSelected.transform.SetParent(null); // Destroy waits for the end of the frame; leave the slot empty now
        Destroy(curSelected);
        curSelected = null;
        isFull[curSlotIndex] = false;
        items[curSlotIndex] = null;
        DeselectAll();
    }

    // Swap the selected item for another in the same slot (the pot, now full of water).
    public void ReplaceCur(GameObject icon) {
        if (curSelected == null) return;
        int slot = curSlotIndex;
        curSelected.transform.SetParent(null);
        Destroy(curSelected);
        curSelected = null;
        items[slot] = IdOf(icon);
        Instantiate(icon, slots[slot].transform, false);
        DeselectAll();
    }
}
