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
    public float knockedAmount = 5f;

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

        if (gameObject.tag == "Player" && !playerHit)
        {
            playerHit = true;

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            float push = gameObject.GetComponent<Movement>().pushedAcc * 1000f * gameObject.transform.position.x.CompareTo(other.transform.position.x);
            rb.AddForce(new Vector2(rb.mass * push, 0));
            rb.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);

            ProcessHit(damageDealer.GetDamage());
            StartCoroutine(PlayerHitIndicator());
            StartCoroutine(PlayerImmunity());
        }
        
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
            StartCoroutine(KnockEnemy(other.gameObject, gameObject, knockedAmount));
            ProcessHit(damageDealer.GetDamage());
            StartCoroutine(PlayerHitIndicator());
        }

        if(gameObject.tag == "Player" && !playerHit)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            //The push distance times 1 or -1 depending on direction
            float push = gameObject.GetComponent<Movement>().pushedAcc * 1000f * gameObject.transform.position.x.CompareTo(other.transform.position.x);
            //Debug.Log(gameObject.transform.position.x.CompareTo(other.transform.position.x));

            //If using the bubble, the bubble will pop and player will not be hurt
            if (gameObject.GetComponent<Bubble>().triggered)
            {
                Debug.Log("Bubble Hit!");
                gameObject.GetComponent<Bubble>().triggered = false;
                StartCoroutine(gameObject.GetComponent<Bubble>().Cooldown());
                //knocks the player back then gives immunity (Must use AddForce because moving in x-direction sets velocity)
                rb.AddForce(new Vector2(rb.mass * push, 0));
                rb.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
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
                //knocks the player back then gives immunity (Must use AddForce because moving in x-direction sets velocity)
                rb.AddForce(new Vector2(rb.mass * push, 0));
                rb.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
                StartCoroutine(PlayerHitIndicator());
                StartCoroutine(PlayerImmunity());
                return;
            }
        }

        if(gameObject.tag == "Player" && playerHit)
        {
            return;
        }
    }

    public void ProcessHit(int damage, GameObject player = null)
    {
        health -= damage;

        if (gameObject.tag == "Enemy" || gameObject.tag == "Flying Enemy")
        {
            StartCoroutine(KnockEnemy(player, gameObject, knockedAmount));
            StartCoroutine(PlayerHitIndicator());
        }

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
            health = Globals.maxHealth;
        }

    }

    IEnumerator PlayerImmunity()
    {
        yield return new WaitForSeconds(immunityTimer);
        playerHit = false;
    }

    IEnumerator PlayerHitIndicator()
    {
        //Debug.Log("PlayerHitIndicator Ran");
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }

    void BossDeath()
    {

        if (tag == "First Boss")
        {
            GameObject.Find("First Boss Area").GetComponentInChildren<FreezeCam>().UndoFreeze = true;
            GameObject.FindWithTag("Player").GetComponent<Movement>().colorIn = true;
            Globals.maxHealth = 4;
            GameObject.FindWithTag("Player").GetComponent<Health>().health = 4;
            Destroy(GameObject.Find("Boss1"));
        }

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

        Vector2 velocity = new Vector2(amount * Mathf.Cos(angle), amount * Mathf.Sin(angle));

        return velocity;
    }
    
    IEnumerator KnockEnemy(GameObject player, GameObject enemy, float amount)
    {
        Debug.Log("KnockEnemy() Ran!");
        //No Y velocity for Dragonfly, because it makes returning to starting point glitch.
        if (gameObject.GetComponent<DragonFly>().enabled)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(KnockBack(player, enemy, amount).x, 0);
        } else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = KnockBack(player, enemy, amount);
        }
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public int getHealth()
    {
        return health;
    }

}
