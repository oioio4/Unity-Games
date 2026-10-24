using UnityEngine;
using UnityEngine.Events;

// A combination made of CodeWheels: the safe's 1487, the clock set to 1:10. When every
// wheel shows its part of the answer, it opens.
public class CodeLock : MonoBehaviour
{
    [SerializeField] private CodeWheel[] wheels = new CodeWheel[0];
    [SerializeField] private string[] answer = new string[0];
    [Tooltip("Flags needed before the wheels will turn, e.g. the safe's padlock taken off.")]
    [SerializeField] private string[] needs = new string[0];
    [TextArea] [SerializeField] private string lockedHint;
    [SerializeField] private string setFlag;
    [TextArea] [SerializeField] private string solvedHint;
    [SerializeField] private UnityEvent onSolved = new UnityEvent();
    [Tooltip("The wheels still turn once it's open, e.g. the clock's hands (for time travel).")]
    [SerializeField] private bool keepTurning;

    public bool Solved { get; private set; }
    public bool KeepTurning => keepTurning;
    public string LockedHint => lockedHint;

    public bool Ready {
        get {
            foreach (string flag in needs) {
                if (GameManager.instance == null || !GameManager.instance.HasFlag(flag)) return false;
            }
            return true;
        }
    }

    public void Check() {
        if (Solved || !Ready) return;
        for (int i = 0; i < wheels.Length; i++) {
            if (wheels[i].Value != answer[i]) return;
        }

        Solved = true;
        if (!keepTurning) {
            foreach (var wheel in wheels) wheel.active = false;
        }
        if (GameManager.instance != null) GameManager.instance.SetFlag(setFlag);
        HintText.Show(solvedHint);
        onSolved.Invoke();
    }
}
