using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Bullet Attribute")]
    public float speed = 65f;
    public GameObject hitEffect;




    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }
    


    void Update()
    {
        // Destroy bullet if there is no target or target is hit
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        #region Moving Bullet Towards Target
        Vector3 bulletDir = target.position - transform.position; //Direction to target
        float distanceThisFrame = speed * Time.deltaTime; //Distance bullet will travel this frame

        //If bullet is close enough to hit target
        if (bulletDir.magnitude <= distanceThisFrame) 
        {
            HitTarget();
            return;
        }

        //Move bullet towards target
        transform.Translate(bulletDir.normalized * distanceThisFrame, Space.World); 

        #endregion
    }

    private void HitTarget()
    {
        //Hit effect
        GameObject effectHit = (GameObject) Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effectHit, 2f); //Destroy hit effect after 2 seconds

        //Destroy bullet
        Destroy(gameObject);
    }
}
