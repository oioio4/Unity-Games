using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    BuildManager buildManager;

    void Start() {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret() {
        Debug.Log("Selected turret");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissleLauncher() {
        Debug.Log("Selected launcher");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer() {
        Debug.Log("Selected beamer");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
