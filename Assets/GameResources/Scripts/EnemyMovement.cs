using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;


    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.wayPoints[0];
    }

    void Update()
    {
        #region MoveTowardsTarget
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.currentSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }


        // After out Range of LaserTower turnback current speed to start speed
        enemy.currentSpeed = enemy.startSpeed;
        #endregion

    }
    #region Move Enemy to Next Waypoint
    void GetNextWayPoint()
    {
        if (wavepointIndex >= Waypoints.wayPoints.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.wayPoints[wavepointIndex];
    }


    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
    #endregion

}
