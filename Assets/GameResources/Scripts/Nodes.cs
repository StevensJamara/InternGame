using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Nodes : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;


    [HideInInspector] public GameObject tower;
    [HideInInspector] public TurretBlueprint turretBlueprint;
    [HideInInspector] public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;



    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();    
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPostion()
    {
        return transform.position + positionOffset; 
    }

    // Build tower on node when mouse is clicked
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // If there is no tower selected to build, do nothing
        if (!buildManager.CanBuild)
        {
            return;
        }

        // If there is already a tower at node, do nothing
        if (tower != null)
        {
            buildManager.SelectNodes(this);
            return;
        }


        BuildTower(buildManager.GetTurretToBuild());
    }

    #region Build and Upgrade Tower
    void BuildTower(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;


        GameObject _turretBuild = (GameObject)Instantiate(blueprint.prefab, GetBuildPostion(), Quaternion.identity);
        tower = _turretBuild;

        turretBlueprint = blueprint;

        GameObject buildEff = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPostion(), Quaternion.identity);
        Destroy(buildEff, 5f);
    }

    public void UpgradeTower()
    {
        if (PlayerStats.Money < turretBlueprint.upGradeCost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upGradeCost;

        // Remove the old tower
        Destroy(tower);


        // Build the upgraded tower
        GameObject _turretBuild = (GameObject)Instantiate(turretBlueprint.upGradedPrefab, GetBuildPostion(), Quaternion.identity);
        tower = _turretBuild;

        GameObject buildEff = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPostion(), Quaternion.identity);
        Destroy(buildEff, 5f);

        isUpgraded = true;
    }

    public void SellTower()
    {
        // Add money to player stats
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        // Show sell effect
        GameObject sellEff = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPostion(), Quaternion.identity);
        Destroy(sellEff, 5f);

        // Remove the tower
        Destroy(tower);
        turretBlueprint = null;
    }
    #endregion


    // Turn to hover color when mouse is over node
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        // Change color based on if player has enough money to build
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }


    // Turn back to start color when mouse is no longer over node
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
