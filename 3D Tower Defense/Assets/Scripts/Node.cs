using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hovorColor;
    public Color insufficientMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

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

        if (turret != null) {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild) {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint) {
        if (PlayerStats.Money < blueprint.cost) {
            Debug.Log("Not enough money");
            return;
        }
        PlayerStats.Money -= blueprint.cost; 
        
        Vector3 currentLoc = GetBuildPosition();
        GameObject _turret = Instantiate(blueprint.prefab, currentLoc, Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
        GameObject effect = Instantiate(buildManager.buildEffect, currentLoc, Quaternion.identity);
        Destroy(effect, 3f);

    }

    public void UpgradeTurret() {
        if (PlayerStats.Money < turretBlueprint.upgradeCost) {
            Debug.Log("Not enough money to upgrade");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost; 

        Destroy(turret);

        Vector3 currentLoc = GetBuildPosition();
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, currentLoc, Quaternion.identity);
        turret = _turret;
        GameObject effect = Instantiate(buildManager.buildEffect, currentLoc, Quaternion.identity);
        Destroy(effect, 3f);

        isUpgraded = true;
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
