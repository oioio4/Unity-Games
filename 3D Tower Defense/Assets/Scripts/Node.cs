using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hovorColor;
    public Color insufficientMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

    private Color startColor;
    private Renderer rend;

    BuildManager buildManager;

    void Start() {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition() {
        return transform.position + positionOffset;
    }

    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        if (!buildManager.CanBuild) {
            return;
        }
        if (turret != null) {
            Debug.Log("Can't build");
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        if (!buildManager.CanBuild) {
            return;
        }
        if (buildManager.HasMoney) {
            rend.material.color = hovorColor;
        }
        else {
            rend.material.color = insufficientMoneyColor;
        }
    }

    void OnMouseExit() {
        rend.material.color = startColor;
    }
}
