using UnityEngine;

// The television: every channel shows a clue to the next one. Follow the whole route
// (Autumn: 3 6 1 2 4 5 3 2 5 4) and the last channel shows the cube. A wrong channel
// gives static, and you start again from the first one.
public class ChannelTV : MonoBehaviour
{
    [SerializeField] private TextMesh screenText;
    [Tooltip("The lit picture; off until the TV is switched on.")]
    [SerializeField] private GameObject screen;
    [SerializeField] private int[] route = { 3, 6, 1, 2, 4, 5, 3, 2, 5, 4 };
    [Tooltip("What's on at each stop of the route.")]
    [TextArea] [SerializeField] private string[] pictures = new string[0];
    [TextArea] [SerializeField] private string staticPicture = "~ static ~";
    [Tooltip("Appears when the route is done, e.g. the black cube.")]
    [SerializeField] private GameObject reward;
    [SerializeField] private string setFlag;

    private int next = -1; // stop of the route we're waiting for; -1 while switched off

    public bool On => next >= 0;
    public bool Done => next >= route.Length;
    public int Channel { get; private set; }

    public void TurnOn() {
        if (On) return;
        if (screen != null) screen.SetActive(true);
        next = 0;
        Press(route[0]); // it comes on at the first channel of the route
    }

    public void Press(int channel) {
        if (!On) {
            HintText.Show("It's switched off.");
            return;
        }
        if (Done) return;

        Channel = channel;
        if (channel == route[next]) {
            Show(pictures[next]);
            next++;
        }
        else if (channel == route[0]) {
            Show(pictures[0]); // back to the start of the trail
            next = 1;
        }
        else {
            Show(staticPicture);
            next = 0;
        }

        if (Done) {
            if (reward != null) reward.SetActive(true);
            if (GameManager.instance != null) GameManager.instance.SetFlag(setFlag);
        }
    }

    private void Show(string picture) {
        if (screenText != null) screenText.text = "CH " + Channel + "\n\n" + picture;
    }
}
