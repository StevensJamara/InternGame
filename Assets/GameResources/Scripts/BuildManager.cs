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


    // Set the tower to build
    private GameObject towerToBuild;

    public GameObject GetTowerToBuild()
    {
        return towerToBuild;
    }

    public void SetTowerToBuild(GameObject tower)
    {
        towerToBuild = tower;
    }

}
