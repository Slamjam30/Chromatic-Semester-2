using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;
    [Header("Only Effects Player")]
    [SerializeField] float immunityTimer = 2f;

    bool playerHit = false;
    public bool knockback;
    public float KNOCKBACK_AMOUNT = 500;

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
            if (gameObject.GetComponent<Bubble>().triggered)
            {
                gameObject.GetComponent<Bubble>().triggered = false;
                StartCoroutine(gameObject.GetComponent<Bubble>().Cooldown());
                StartCoroutine(PlayerImmunity());
                return;
            }
            else
            {
                playerHit = true;
                ProcessHit(damageDealer.GetDamage());
                /*if (other.gameObject.GetComponent<Health>() != null && other.gameObject.GetComponent<Health>().knockback)
                {

                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(KnockBack(gameObject, other.gameObject, other.gameObject.GetComponent<Health>().KNOCKBACK_AMOUNT));
                    //other.gameObject.transform.position = Vector2.MoveTowards(other.transform.position, transform.position, -other.gameObject.GetComponent<Health>().KNOCKBACK_AMOUNT); 
                }*/
                StartCoroutine(PlayerImmunity());
                return;
            }
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

    Vector2 KnockBack(GameObject player, GameObject enemy, float amount)
    {
        float dX = player.transform.position.x - enemy.transform.position.x;
        float dY = player.transform.position.y - enemy.transform.position.y;

        float angle = Mathf.Atan(dY / dX);

        //For 2nd and 3rd Quadrant Angles
        if ((dX < 0 && dY > 0) || (dX < 0 && dY < 0))
        { angle = -angle; }

        Vector2 test = new Vector2(amount * Mathf.Cos(angle), amount * Mathf.Sin(angle));

        return test;
    }
    
}
