using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;
    [Header("Only Effects Player")]
    [SerializeField] float immunityTimer = 2f;

    bool playerHit = false;

    public Vector3 startPos;

    private void Start()
    {
        startPos = gameObject.transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if(!damageDealer || gameObject.tag == other.gameObject.tag)
        {
            return;
        }
        ProcessHit(damageDealer.GetDamage());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if(!damageDealer || gameObject.tag == other.gameObject.tag)
        {
            return;
        }
        
        if (gameObject.tag == "Enemy" || gameObject.tag == "Flying Enemy")
        {
            ProcessHit(damageDealer.GetDamage());
        }

        if(gameObject.tag == "Player" && !playerHit)
        {
            playerHit = true;
            ProcessHit(damageDealer.GetDamage());
            StartCoroutine(PlayerImmunity());
            return;
        }

        if(gameObject.tag == "Player" && playerHit)
        {
            return;
        }
    }

    public void ProcessHit(int damage)
    {
        health -= damage;

        if (health <= 0 && tag == "Enemy")
        {
            Destroy(gameObject);
            
        }

        else if (health <= 0 && tag != "Player")
        {

            BossDeath();
        
        }

        else if (health <= 0 && tag == "Player")
        {
            gameObject.transform.position = startPos;
            health = 3;
        }

    }

    IEnumerator PlayerImmunity()
    {
        yield return new WaitForSeconds(immunityTimer);
        playerHit = false;
    }

    void BossDeath()
    {

        if (tag == "Frog Boss")
        {
            //Play an animation or something
            GameObject.Find("RisingWater").GetComponent<Water_Rise>().rise = true;

            Destroy(gameObject);
            
        }

    }

    
}
