using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Bullet Attribute")]
    public float speed = 65f;
    public float explosionRadius = 0f;
    public GameObject hitEffect;
    public int DamageDeal = 10;

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
        transform.LookAt(target);
        #endregion
    }

    private void HitTarget()
    {
        //Hit effect
        GameObject effectHit = (GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effectHit, 5f); //Destroy hit effect after 2 seconds

        // Range damage or single target damage
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        //Destroy bullet
        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        
        if (e != null)
        {
            e.TakeDamage(DamageDeal);
        }

    }

    #region Effect Area Damage
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    #endregion
}
