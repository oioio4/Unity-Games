using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A close look through something, e.g. the telescope at the moon. Switches to this camera
// for a few seconds (or until you click), then back to your own eyes.
[RequireComponent(typeof(Camera))]
public class PeekView : MonoBehaviour
{
    [SerializeField] private float seconds = 6f;
    [TextArea] [SerializeField] private string hint;

    private Camera view;
    private bool showing;

    public bool Showing => showing;

    private void Awake() {
        view = GetComponent<Camera>();
        view.enabled = false;
        if (TryGetComponent(out AudioListener listener)) listener.enabled = false;
    }

    public void Show() {
        if (!showing) StartCoroutine(Run());
    }

    private IEnumerator Run() {
        showing = true;
        var hidden = new List<Camera>();
        foreach (var cam in Camera.allCameras) {
            if (cam == view) continue;
            cam.enabled = false;
            hidden.Add(cam);
        }
        // stand still while looking
        var player = GameObject.FindGameObjectWithTag("Player");
        var mover = player != null ? player.GetComponent("FPSController") as Behaviour : null;
        if (mover != null) mover.enabled = false;

        view.enabled = true;
        HintText.Show(hint);
        yield return null; // the click that started this shouldn't end it

        for (float t = 0f; t < seconds && !Input.GetMouseButtonDown(0); t += Time.deltaTime) yield return null;

        view.enabled = false;
        foreach (var cam in hidden) if (cam != null) cam.enabled = true;
        if (mover != null) mover.enabled = true;
        showing = false;
    }
}
