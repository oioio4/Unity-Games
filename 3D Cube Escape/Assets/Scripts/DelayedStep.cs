using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Finishes something after a wait, e.g. the egg is boiled a few seconds after it goes
// in the pot. Start it from a puzzle step's onDone with Begin().
public class DelayedStep : MonoBehaviour
{
    [SerializeField] private float seconds = 4f;
    [SerializeField] private string setFlag;
    [TextArea] [SerializeField] private string hint;
    [SerializeField] private UnityEvent onDone;

    public void Begin() {
        StartCoroutine(Run());
    }

    private IEnumerator Run() {
        yield return new WaitForSeconds(seconds);
        if (GameManager.instance != null) GameManager.instance.SetFlag(setFlag);
        HintText.Show(hint);
        onDone.Invoke();
    }
}
