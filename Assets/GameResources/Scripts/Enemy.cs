using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float currentSpeed;

    public float Health = 20;
    public int coinWorth = 5;
    public GameObject deathEffect;

    void Start()
    {
        currentSpeed = startSpeed;
    }


    #region Enemy Take Damage
    public void TakeDamage(float amount)
    {
        Health -= amount;
        if(Health <= 0)
        {
            Die();
        }
    }

    public void Slow(float Percentage)
    {
        currentSpeed = startSpeed * (1f - Percentage);
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


    
}
