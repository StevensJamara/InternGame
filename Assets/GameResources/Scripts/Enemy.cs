using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        target = Waypoints.wayPoints[0];
    }

    void Update()
    {
        #region MoveTowardsTarget
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= .2f)
        {
            GetNextWayPoint();
        }
        #endregion

    }
    #region Move Enemy to Next Waypoint
    void GetNextWayPoint()
    {
        if(wavepointIndex >= Waypoints.wayPoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }   

        wavepointIndex++;
        target = Waypoints.wayPoints[wavepointIndex];
    }
    #endregion
}
