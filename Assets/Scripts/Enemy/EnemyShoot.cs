using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public float shootingDistance = 10.0f;
    public float rotationSpeed = 5.0f;
    public float bulletSpeed = 10.0f;
    public int maxBulletsPerBurst = 3;
    public float timeBetweenBullets = 1.0f;
    public float scatterWidth = 2.0f;
    public float scatterHeight = 1.0f;

    private float timeSinceLastShot;

    private void Start()
    {
        //Allows a burst of bullets to be fired immediately once enemy object has been initialized
        timeSinceLastShot = timeBetweenBullets;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //If the distance to the player is less than or equal to shooting distance, rotate towards them
        if (distanceToPlayer <= shootingDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            //A timer so that the enemy can only shoot off a cooldown
            if (Time.time - timeSinceLastShot >= timeBetweenBullets)
            {
                ShootBurst();
                timeSinceLastShot = Time.time;
            }
        }
    }

    void ShootBurst()
    {
        for (int i = 0; i < maxBulletsPerBurst; i++)
        {
            //We can set values for scatterWidth and scatterheight which will give the projectiles a shotgun-like effect
            //seeing as they all bullets will be shot at a random value between those values

            float xOffset = Random.Range(-scatterWidth, scatterWidth);
            float yOffset = Random.Range(-scatterHeight, scatterHeight);

            //Calculate the direction of player so that the enemy knows where to shoot
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 scatterDirection = directionToPlayer + transform.right * xOffset + transform.up * yOffset;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

            //Apply velocity to the rigidbody of the bullets so they actually move towards the player
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = scatterDirection.normalized * bulletSpeed;

            Destroy(projectile, 2.0f);
        }
    }
}
