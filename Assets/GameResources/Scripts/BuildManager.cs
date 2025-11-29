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

    public GameObject standardTowerPrefab;
    public GameObject CanonPrefab;

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

        Debug.Log("Turret built! Money left: " + PlayerStats.Money);
    }


    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        towerToBuild = turret;
    }

}
