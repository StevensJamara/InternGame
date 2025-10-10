using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Turret : MonoBehaviour
{
    public float range = 15f;
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float rotateSpeed = 10f;

    private Transform target;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //Find target every 0.5 seconds
    }


    #region Searching For Target
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //Enemies's distance from turret
            if (distanceToEnemy < shortestDistance) //If this enemy is closer than the previous closest
            {
                shortestDistance = distanceToEnemy; //Update shortest distance
                nearestEnemy = enemy; //Update nearest enemy
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform; //Set target to nearest enemy
        }
        else
        {
            target = null; //No target found
        }

    }

    #endregion
    // Update is called once per frame
    void Update()
    {
        #region Checking For Target
        if (target == null)
            return;
        #endregion

        //Rotating Turret's heading towards target
        Vector3 dir = target.position - transform.position; //Direction to target
        Quaternion lookRotation = Quaternion.LookRotation(dir); 
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //Rotate only on y axis

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
