using UnityEngine;
using UnityEngine.EventSystems;

public class Nodes : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;


    [Header("Optional")]
    public GameObject tower;

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
        // If there is no tower selected to build, do nothing
        if (!buildManager.CanBuild)
        {
            return;
        }

        // If there is already a tower at node, do nothing
        if (tower != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen");
            return;
        }

        buildManager.BuildTurretOn(this);
    }

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
        rend.material.color = hoverColor;
    }


    // Turn back to start color when mouse is no longer over node
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
