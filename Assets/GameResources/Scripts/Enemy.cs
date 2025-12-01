using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public int Health = 20;
    public int coinWorth = 5;
    public GameObject deathEffect;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        target = Waypoints.wayPoints[0];
    }


    #region Enemy Take Damage
    public void TakeDamage(int amount)
    {
        Health -= amount;
        if(Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Reward Player with Money
        PlayerStats.Money += coinWorth;

        // Death Effect
        GameObject dieEff = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(dieEff, 5f);

        // Destroy Enemy
        Destroy(gameObject);
    }
    #endregion


    void Update()
    {
        #region MoveTowardsTarget
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
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
