using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Turret : MonoBehaviour
{
    [Header("Hidden Attributes")]
    private Transform target;
    private Enemy targetEnemy;



    [Header("Turret Properties")]
    public float range = 15f; 
    public Transform partToRotate;

    [Header("Tower use Bullets (default)")]
    public GameObject bulletPrefab;
    public float shootRate = 1.2f;
    private float shootCountdown = 0f;

    [Header("Tower use Laser (optional)")]
    public bool useLaser = false;
    public int laserDoT = 6;
    public float slowAmount = 0.5f;
    public LineRenderer beamRenderer;
    public ParticleSystem beamEffect;
    public Light beamLight;

    [Header("Unity Setup Fields")]
    public float rotateSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform firePoint;




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
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
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
        {
            if (useLaser && beamRenderer.enabled)
            {
                beamRenderer.enabled = false;
                beamEffect.Stop();
                beamLight.enabled = false;
            }
            return;
        }
        #endregion

        LockOnTarget();

        #region Shooting
        if (useLaser)
        {
            LaserBeam();
        }
        else
        {
            if (shootCountdown <= 0f)
            {
                Shoot();
                shootCountdown = 1f / shootRate;
            }

            shootCountdown -= Time.deltaTime;
        }
        #endregion
    }

    #region Type Bullet used of Shooting
    void LockOnTarget()
    {
        //Rotating Turret's heading towards target
        Vector3 dir = target.position - transform.position; //Direction to target
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //Rotate only on y axis
    }

    void LaserBeam()
    {
        //Damage over time to enemy
        //Caused Slow for enemy
        targetEnemy.GetComponent<Enemy>().TakeDamage(laserDoT * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        #region Beam Effect
        if (!beamRenderer.enabled)
        {
            beamRenderer.enabled = true;
            beamEffect.Play();
            beamLight.enabled = true;
        }
        beamRenderer.SetPosition(0, firePoint.position);
        beamRenderer.SetPosition(1, target.position);

        Vector3 beamEffectDirection = firePoint.position - target.position;
        
        beamEffect.transform.position = target.position + beamEffectDirection.normalized * 0.5f;

        beamEffect.transform.rotation = Quaternion.LookRotation(beamEffectDirection);
        #endregion
    }
    #endregion


    void Shoot()
    {
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    #region Draw Range shooting Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    #endregion
}
