using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    private ShootPistol shootPistol; 

    private void Start()
    {
        shootPistol = FindObjectOfType<ShootPistol>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);

            if (shootPistol != null)
                shootPistol.IncrementBulletsLeft();

            Destroy(collision.gameObject);
            shootPistol.EnemiesLeft();
        }
        
        Destroy(gameObject);
    }
}
