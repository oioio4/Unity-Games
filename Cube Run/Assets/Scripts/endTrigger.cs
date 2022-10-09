using UnityEngine;

public class endTrigger : MonoBehaviour
{
    public gamemanager gm;

    void OnTriggerEnter() {
        gm.completeLevel();
    }
}
