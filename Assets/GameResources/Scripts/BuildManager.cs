using UnityEditor.PackageManager;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Initialed Singleton
    public static BuildManager instance;

    private void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this; // Singleton pattern

    }
    #endregion


    // Prefabs for towers
    public GameObject standardTowerPrefab;
    public GameObject CanonPrefab;

    // Prefabs for Effects
    public GameObject buildEffect;


    // Set the tower to build
    private TurretBlueprint towerToBuild;

    // Get the tower to build
    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

    // Build tower on node selected
    public void BuildTurretOn(Nodes node)
    {
        if(PlayerStats.Money < towerToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= towerToBuild.cost;


        GameObject turretBuild = (GameObject) Instantiate(towerToBuild.prefab, node.GetBuildPostion(), Quaternion.identity);
        node.tower = turretBuild;

        GameObject buildEff = (GameObject)Instantiate(buildEffect, node.GetBuildPostion(), Quaternion.identity);
        Destroy(buildEff, 5f);


    }


    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        towerToBuild = turret;
    }

}
