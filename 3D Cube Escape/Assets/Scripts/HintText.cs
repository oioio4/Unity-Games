using UnityEngine;
using UnityEngine.UI;

// One line of text above the hotbar for "that doesn't work" moments, hints and flavour.
// Builds its own overlay canvas the first time something is said, so scenes need no setup.
public class HintText : MonoBehaviour
{
    private const float ShowFor = 3.5f;
    private const float FadeFor = 0.6f;

    private static HintText current;
    private Text text;
    private float shownAt = -99f;

    public static void Show(string message) {
        if (string.IsNullOrEmpty(message)) return;
        if (current == null) current = Create();
        current.text.text = message;
        current.shownAt = Time.unscaledTime;
    }

    private static HintText Create() {
        var root = new GameObject("HintText", typeof(Canvas), typeof(CanvasScaler));
        var canvas = root.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;
        var scaler = root.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1280f, 720f);

        var label = new GameObject("Text", typeof(Text), typeof(Shadow));
        label.transform.SetParent(root.transform, false);
        var rect = (RectTransform)label.transform;
        rect.anchorMin = new Vector2(0.1f, 0.25f);   // just above the hotbar
        rect.anchorMax = new Vector2(0.9f, 0.35f);
        rect.offsetMin = rect.offsetMax = Vector2.zero;

        var hint = root.AddComponent<HintText>();
        hint.text = label.GetComponent<Text>();
        hint.text.alignment = TextAnchor.MiddleCenter;
        hint.text.fontSize = 26;
        hint.text.raycastTarget = false;
        // the game's own font if the hotbar's item popup has one
        var inventory = FindFirstObjectByType<Inventory>();
        var popupText = inventory != null && inventory.itemPopup != null ? inventory.itemPopup.GetComponentInChildren<Text>(true) : null;
        hint.text.font = popupText != null ? popupText.font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        return hint;
    }

    private void Update() {
        float age = Time.unscaledTime - shownAt;
        var c = text.color;
        c.a = 1f - Mathf.Clamp01((age - ShowFor) / FadeFor);
        text.color = c;
    }
}
