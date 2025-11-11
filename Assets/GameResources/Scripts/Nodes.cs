using UnityEngine;
using UnityEngine.EventSystems;

public class Nodes : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject tower;

    private Renderer rend;
    private Color startColor;



    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();    
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }


    private void OnMouseDown()
    {
        // If there is no tower selected to build, do nothing
        if (buildManager.GetTowerToBuild() == null)
        {
            return;
        }

        // If there is already a tower at node, do nothing
        if (tower != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen");
            return;
        }
        
        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();


        tower = (GameObject) Instantiate(towerToBuild, transform.position + positionOffset, transform.rotation);
    }

    // Turn to hover color when mouse is over node
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (buildManager.GetTowerToBuild() == null)
        {
            return;
        }
        rend.material.color = hoverColor;
    }


    // Turn back to start color when mouse is no longer over node
    private void OnMouseExit()
    {

        rend.material.color = startColor;
    }
}
