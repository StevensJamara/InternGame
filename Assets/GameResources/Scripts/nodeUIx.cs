using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeUIx : MonoBehaviour
{
    public GameObject uI;
    private Nodes target;

    public void SetTarget(Nodes _target)
    {
        target = _target;
        transform.position = target.GetBuildPostion();

        uI.SetActive(true);
    }

    public void Hide()
    {
        uI.SetActive(false);
    }
}
