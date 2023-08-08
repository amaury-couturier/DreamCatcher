using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 1f;

    [SerializeField] private AudioSource deathEffect;

    public void TakeDamage(float damage)
    {
        health -= damage;
        deathEffect.Play();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
