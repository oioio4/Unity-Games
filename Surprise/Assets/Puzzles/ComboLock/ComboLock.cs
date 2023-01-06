using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ComboLock : MonoBehaviour
{
    public LockSlot slot1;
    public LockSlot slot2;
    public LockSlot slot3;
    public LockSlot slot4;

    [SerializeField] private int ans1;
    [SerializeField]private int ans2;
    [SerializeField] private int ans3;
    [SerializeField] private int ans4;

    public UnityEvent winAction;

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update() {
        if (slot1.getNum() == ans1 && slot2.getNum() == ans2 && slot3.getNum() == ans3 && slot4.getNum() == ans4) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            winAction.Invoke();
        }
    }
}
