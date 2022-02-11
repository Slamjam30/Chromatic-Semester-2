using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float timeBetweenShots = 2f;
    float shotCounter;
    private GameObject mainCharacter;

    void Start()
    {
        mainCharacter = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            if (!mainCharacter)
            {
                return;
            }
            Fire();
            shotCounter = timeBetweenShots;
        }
    }

    private void Fire()
    {
        GameObject launchedProjectile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        // Shoot Right
        if (gameObject.transform.position.x > mainCharacter.transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            launchedProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, 0);
        }
        // Shoot Left
        if (this.transform.position.x < mainCharacter.transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            launchedProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
        }
    }
}
