using UnityEngine;
using UnityEngine.Events;

// Something that's only so because of an earlier season (Past.Remember), or only while
// visiting the past: shows and hides objects when the room starts, or when Apply() is
// called, e.g. by the radio finding the right station.
public class Echo : MonoBehaviour
{
    [Tooltip("Only when this room was reached through Winter's clock.")]
    [SerializeField] private bool visiting;
    [Tooltip("Things done in earlier seasons, e.g. \"seed\".")]
    [SerializeField] private string[] remembered = new string[0];
    [Tooltip("Flags needed in this room as well, e.g. the boards off the window.")]
    [SerializeField] private string[] needs = new string[0];
    [SerializeField] private bool applyOnStart = true;
    [SerializeField] private GameObject[] show = new GameObject[0];
    [SerializeField] private GameObject[] hide = new GameObject[0];
    [SerializeField] private string setFlag;
    [TextArea] [SerializeField] private string hint;
    [SerializeField] private UnityEvent onApply = new UnityEvent();

    private bool applied;

    private void Start() {
        if (applyOnStart) Apply();
    }

    public void Apply() {
        if (applied || (visiting && !Past.Visiting)) return;
        foreach (string what in remembered) {
            if (!Past.Remembers(what)) return;
        }
        var gm = GameManager.instance;
        foreach (string flag in needs) {
            if (gm == null || !gm.HasFlag(flag)) return;
        }

        applied = true;
        foreach (var go in hide) if (go != null) go.SetActive(false);
        foreach (var go in show) if (go != null) go.SetActive(true);
        if (gm != null) gm.SetFlag(setFlag);
        HintText.Show(hint);
        onApply.Invoke();
    }
}
