using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hovorColor;

    private GameObject turret;

    private Color startColor;
    private Renderer rend;

    BuildManager buildManager;

    void Start() {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        if (buildManager.GetTurretToBuild() == null) {
            return;
        }
        if (turret != null) {
            Debug.Log("Can't build");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position, transform.rotation);
    }

    void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        if (buildManager.GetTurretToBuild() == null) {
            return;
        }
        rend.material.color = hovorColor;
    }

    void OnMouseExit() {
        rend.material.color = startColor;
    }
}
