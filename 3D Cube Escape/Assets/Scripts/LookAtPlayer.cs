using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;

    [SerializeField] private float speed = 1f;

    private void Update() {
        Vector3 targetDirection = player.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
