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

    // Prefabs for Effects
    public GameObject buildEffect;
    public GameObject sellEffect;


    // Set the tower to build
    private TurretBlueprint towerToBuild;
    private Nodes selectedNode;

    public nodeUIx NodeUIx;

    // Get the tower to build
    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

   
    public void SelectNodes (Nodes node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        towerToBuild = null;

        NodeUIx.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        NodeUIx.Hide();
    }

    public void SelectTurretToBuild (TurretBlueprint turret)
    {
        towerToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return towerToBuild;
    }
}
