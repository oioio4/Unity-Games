using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // singleton stuff
    public static BuildManager instance;

    void Awake() {
        if (instance != null) {
            Debug.Log("Too many build managers");
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject missleLauncherPrefab;
    private GameObject turretToBuild;

    void Start() {
        turretToBuild = null;
    }

    public GameObject GetTurretToBuild() {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret) {
        turretToBuild = turret;
    }
}
