using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform movePositionTransform;

    private NavMeshAgent navMeshAgent;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        movePositionTransform =  GameObject.Find("End").transform;
    }

    private void Update() {
        navMeshAgent.destination = movePositionTransform.position;
    }
}
