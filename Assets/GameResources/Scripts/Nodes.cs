using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject tower;

    private Renderer rend;
    private Color startColor;


    void Start()
    {
        rend = GetComponent<Renderer>();    
        startColor = rend.material.color;
    }


    private void OnMouseDown()
    {
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
        rend.material.color = hoverColor;
    }


    // Turn back to start color when mouse is no longer over node
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
