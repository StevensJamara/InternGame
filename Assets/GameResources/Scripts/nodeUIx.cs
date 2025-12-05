using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class nodeUIx : MonoBehaviour
{
    public GameObject uI;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI sellText;
    public Button upgradeButton;
    public Button sellButton;

    private Nodes target;

    public void SetTarget(Nodes _target)
    {
        target = _target;

        transform.position = target.GetBuildPostion();


        if (!target.isUpgraded)
        {
            upgradeCostText.text = target.turretBlueprint.upGradeCost + "$";
            upgradeButton.interactable = true;


            sellText.text = target.turretBlueprint.cost / 3 + "$"; 
        }
        else
        {
            upgradeCostText.text = "MAX";
            upgradeButton.interactable = false;
        }
        uI.SetActive(true);
    }

    // Hide the UI
    public void Hide()
    {
        uI.SetActive(false);
    }

    // Upgrade the tower
    public void Upgrade()
    {
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }
}
