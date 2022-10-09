using UnityEngine;

public class playerCollision : MonoBehaviour
{
    public playerMovement movement;

    void OnCollisionEnter(Collision colinfo) {
        if (colinfo.collider.tag == "obstacle") {
            movement.enabled = false;
            FindObjectOfType<gamemanager>().EndGame();
        }
    }
}
