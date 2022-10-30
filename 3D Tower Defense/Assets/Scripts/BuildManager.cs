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

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node) {
        if (PlayerStats.Money < turretToBuild.cost) {
            Debug.Log("Not enough money");
            return;
        }
        Vector3 currentLoc = node.GetBuildPosition();
        GameObject turret = Instantiate(turretToBuild.prefab, currentLoc, Quaternion.identity);
        node.turret = turret;
        GameObject effect = Instantiate(buildEffect, currentLoc, Quaternion.identity);
        Destroy(effect, 3f);
        PlayerStats.Money -= turretToBuild.cost; 
    }

    public void SelectTurretToBuild(TurretBlueprint turret) {
        turretToBuild = turret;
    }
}
