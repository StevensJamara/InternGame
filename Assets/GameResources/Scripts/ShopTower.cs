using UnityEngine;

public class ShopTower : MonoBehaviour
{
    BuildManager buildManager;


    private void Start()
    {
        buildManager = BuildManager.instance;
    }



    public void PurchaseStandardTower()
    {
        Debug.Log("Standard Tower Purchased!");
        buildManager.SetTowerToBuild(buildManager.standardTowerPrefab);
    }
}
