using UnityEngine;

public class ShopTower : MonoBehaviour
{
    public TurretBlueprint standardTower;
    public TurretBlueprint canonTower;
    public TurretBlueprint laserTower;



    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTower()
    {
        Debug.Log("Standard Tower Purchased!");
        buildManager.SelectTurretToBuild(standardTower);
    }

    public void SelectCanonTower()
    {
        Debug.Log("Canon Tower Purchased!");
        buildManager.SelectTurretToBuild(canonTower);
    }
    public void SelectLaserTower()
    {
        Debug.Log("Laser Tower Purchased!");
        buildManager.SelectTurretToBuild(laserTower);
    }
}
