using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;
    [Header("Only Effects Player")]
    [SerializeField] float immunityTimer = 2f;

    public int octoHeadHealth;

    bool playerHit = false;

    public Vector3 startPos;
    public float knockedAmount = 5f;

    private void Start()
    {
        startPos = gameObject.transform.position;

        octoHeadHealth = 7;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        // Deal damage to Octopus Head when the Bubble hits it
        //Test for 1) this is Octo Boss Head / 2) Other object is Player / 3) Player is bubbling / 4) Octo health is not on cooldown
        if (this.tag == "Octo Boss Head" && other.gameObject.tag == "Player" && other.gameObject.GetComponent<Bubble>().triggered && !this.GetComponent<Tentacle_Script_Main>().octoCool)
        {
            octoHeadHealth -= 1;
            StartCoroutine(this.GetComponent<Tentacle_Script_Main>().OctoHealthCooldown());
        }

        if (!damageDealer || gameObject.tag == other.gameObject.tag)
        {
            return;
        }

        if (gameObject.name == "Dragon" && other.gameObject.name == "Player")
        {
            ProcessHit(damageDealer.GetDamage());
        }

        if (gameObject.tag == "Player" && !playerHit)
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

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        // Deal damage to Octopus Head when the Bubble hits it
        if (this.tag == "Octo Boss Head" && other.tag == "Bubble")
        {
            octoHeadHealth -= 1;
        }

        if (!damageDealer || gameObject.tag == other.gameObject.tag)
        {
            return;
        }
        
        if (gameObject.tag == "Enemy" || gameObject.tag == "Flying Enemy")
        {
            StartCoroutine(KnockEnemy(other.gameObject, gameObject, knockedAmount));
            ProcessHit(damageDealer.GetDamage());
            StartCoroutine(PlayerHitIndicator());
        }

        if (gameObject.tag == "Player" && !playerHit)
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

        if (gameObject.tag == "Player" && playerHit)
        {
            return;
        }
    }

    public void ProcessHit(int damage, GameObject player = null)
    {
        health -= damage;

        if (gameObject.name == "Dragon")
        {
            octoHeadHealth -= damage;
        }

        if (gameObject.tag == "Enemy" || gameObject.tag == "Flying Enemy" || gameObject.tag == "Frog Boss" || gameObject.tag == "Dragon Boss")
        {
            StartCoroutine(KnockEnemy(player, gameObject, knockedAmount));
            StartCoroutine(PlayerHitIndicator());
        }

        if (gameObject.tag == "Armadillo Boss")
        {
            StartCoroutine(ArmadilloHitIndicator());
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
            if (Globals.inFight)
            {
                string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
            }
            else
            {
                gameObject.transform.position = startPos;
                health = Globals.maxHealth;
            }
        }

    }

    IEnumerator PlayerImmunity()
    {
        playerHit = true;
        yield return new WaitForSeconds(immunityTimer);
        playerHit = false;
    }

    IEnumerator PlayerHitIndicator()
    {
        //Debug.Log("PlayerHitIndicator Ran");
        //Because dragonflies may be colored yellow and then would return to not yellow if hit
        Color orig = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().color = orig;
    }

    IEnumerator ArmadilloHitIndicator()
    {
        //Debug.Log("PlayerHitIndicator Ran");
        //Because dragonflies may be colored yellow and then would return to not yellow if hit
        Color orig = gameObject.GetComponent<Armadillo>().armadilloVisual.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<Armadillo>().armadilloVisual.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<Armadillo>().armadilloVisual.GetComponent<SpriteRenderer>().color = orig;
    }

    void BossDeath()
    {

        if (tag == "First Boss")
        {
            GameObject.Find("First Boss Area").GetComponentInChildren<FreezeCam>().UndoFreeze = true;
            Globals.color = "GREEN";
            GameObject.FindWithTag("Player").GetComponent<Movement>().colorIn = true;
            Globals.maxHealth = 4;
            GameObject.FindWithTag("Player").GetComponent<Health>().health = 4;
            Destroy(GameObject.Find("Boss1"));
        }

        if (tag == "Frog Boss")
        {
            //Play an animation or something
            GameObject.Find("RisingWater").GetComponent<Water_Rise>().rise = true;
            Globals.color = "BLUE";
            Globals.maxHealth = 5;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().raiseToMaxHealth();
            gameObject.GetComponent<FrogBoss>().returnToMainSceneTriggerObj.SetActive(true);
            gameObject.GetComponent<FrogBoss>().tutorialCanvas.SetActive(false);


            Destroy(gameObject);
            
        }

        //BLUE Boss is done in Octo script

        if (tag == "Armadillo Boss")
        {
            Globals.color = "RED";
            Globals.maxHealth = 7;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().raiseToMaxHealth();
            Globals.inFight = false;

            //GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            //mainCam.GetComponent<Camera>().orthographicSize = 9f;
            //mainCam.GetComponent<CameraFollow>().following = true;

            gameObject.GetComponent<Armadillo>().afterBossActivate.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<Grapple>().RecalculatePoints();
            Destroy(GameObject.Find("Exit"));
            Destroy(gameObject.GetComponent<Armadillo>().fullArmadillo);
        }

        if (tag == "Dragon Boss")
        {
            Globals.color = "DONE";
            Globals.inFight = false;

            GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            mainCam.GetComponent<Camera>().orthographicSize = 3.538258f;
            mainCam.GetComponent<CameraFollow>().following = true;

            Destroy(GameObject.Find("Exit"));
            Destroy(GameObject.Find("Barrier"));
            Destroy(GameObject.Find("Dragon Stuff"));
        }


    }

    Vector2 KnockBack(GameObject player, GameObject enemy, float amount)
    {
        if (enemy != null && player != null)
        {
            float dX = player.transform.position.x - enemy.transform.position.x;
            float dY = player.transform.position.y - enemy.transform.position.y;

            float angle = Mathf.Atan(dY / dX);

            int dirX = -1;

            //For 2nd and 3rd Quadrant Angles
            if ((dX < 0 && dY > 0) || (dX < 0 && dY < 0))
            { 
                angle = -angle;
                dirX = 1;
            }

            Vector2 velocity = new Vector2(dirX * amount * Mathf.Cos(angle), amount * Mathf.Sin(angle));
            Debug.Log("Knock Back:" + new Vector2(dirX * amount * Mathf.Cos(angle), amount * Mathf.Sin(angle)));
            return velocity;
        }
        else
        {
            return Vector2.zero;
        }
    }
    
    IEnumerator KnockEnemy(GameObject player, GameObject enemy, float amount)
    {
        Debug.Log("KnockEnemy() Ran!");
        //No Y velocity for Dragonfly, because it makes returning to starting point glitch.
        if (enemy != null)
        {
            if (gameObject.GetComponent<DragonFly>() != null)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(KnockBack(player, enemy, amount).x, 0);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = KnockBack(player, enemy, amount);
            }
            yield return new WaitForSeconds(1f);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void raiseToMaxHealth()
    {
        health = Globals.maxHealth;
    }

}
