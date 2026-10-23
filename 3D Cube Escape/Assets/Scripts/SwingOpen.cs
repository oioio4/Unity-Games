using System.Collections;
using UnityEngine;

// Swings a door or lid open (the oven, the safe, the clock case). Call Open(), e.g. from a
// puzzle step's onDone.
public class SwingOpen : MonoBehaviour
{
    [SerializeField] private Vector3 openRotation = new Vector3(0f, -100f, 0f); // local, added to the closed rotation
    [SerializeField] private float seconds = 0.6f;

    private bool opened;

    public void Open() {
        if (opened) return;
        opened = true;
        StartCoroutine(Swing());
    }

    private IEnumerator Swing() {
        Quaternion from = transform.localRotation;
        Quaternion to = from * Quaternion.Euler(openRotation);
        for (float t = 0f; t < seconds; t += Time.deltaTime) {
            transform.localRotation = Quaternion.Slerp(from, to, Mathf.SmoothStep(0f, 1f, t / seconds));
            yield return null;
        }
        transform.localRotation = to;
    }
}
