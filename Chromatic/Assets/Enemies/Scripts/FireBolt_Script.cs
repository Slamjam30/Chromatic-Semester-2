using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolt_Script : MonoBehaviour
{
    public GameObject StageTracker;

    Vector2 pos;

    public Transform player;
    public Transform dragon;
    public Transform firebolt;

    private float startXPos;
    private float startYPos;
    private float rot;

    private SpriteRenderer sprite;

    private float fireSpeed;

    public bool targetingPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        fireSpeed = 15.0f;
        sprite = GetComponent<SpriteRenderer>();
        targetingPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        pos = gameObject.transform.position;

        FlightPhase();
        resetPhaseToFlying();
        controlVisibility();
        targetPlayer();
        InCaseFireboltGoesFlying();
        ShootPlayerAfterPoppingUp();

        transform.position = pos;
    }

    void FlightPhase() 
    {
        if (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "Flying")
        {
            
            if (pos.x >= 0) {
                pos.y = dragon.transform.position.y + Mathf.Abs(Mathf.Sin((dragon.GetComponent<Dragon_Script>().rot * Mathf.PI) / 180));
                pos.x = dragon.transform.position.x + Mathf.Abs(Mathf.Cos((dragon.GetComponent<Dragon_Script>().rot * Mathf.PI) / 180));
            }
            else if (pos.x < 0) 
            {
                pos.y = dragon.transform.position.y + Mathf.Abs(Mathf.Sin((dragon.GetComponent<Dragon_Script>().rot * Mathf.PI) / 180));
                pos.x = dragon.transform.position.x - Mathf.Abs(Mathf.Cos((dragon.GetComponent<Dragon_Script>().rot * Mathf.PI) / 180));
            }
            
        }
    }

    void resetPhaseToFlying()
    {
        if ((pos.y < -4.5f) && 
            (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "FlyingAndAttacking"))
        {
            StageTracker.GetComponent<Dragon_StageTracker>().timeRemaining = 2.0f;
            if (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase != "WhackADragon"
                && StageTracker.GetComponent<Dragon_StageTracker>().currentPhase != "DiverDown")
            {
                StageTracker.GetComponent<Dragon_StageTracker>().currentPhase = "Flying";
            }
            pos.y = 
                dragon.transform.position.y + 
                Mathf.Abs(Mathf.Sin((dragon.GetComponent<Dragon_Script>().rot * Mathf.PI) / 180));
        }
    }

    void targetPlayer()
    {
        if (targetingPlayer == true)
        {
            var direction = player.transform.position - transform.position;
            rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }
        if ((StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "FlyingAndAttacking" 
            || StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "FreeFalling"
            || StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "PlayDead")
            && pos.y > -4.5f && (pos.y < 7.0f))
        {
            pos.x += fireSpeed * Time.deltaTime * 
                Mathf.Cos((rot * Mathf.PI) / 180);
            pos.y += fireSpeed * Time.deltaTime * 
                Mathf.Sin((rot * Mathf.PI) / 180);
        }
    }

    void controlVisibility()
    {
        
        if (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase != "FlyingAndAttacking")
        {
            if ((StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "DragonDive"
                || StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "PlayDead")
                && pos.y > -4.5f)
            {
                sprite.sortingOrder = 0;
                targetingPlayer = false;
                pos.x += fireSpeed * Time.deltaTime *
                Mathf.Cos((rot * Mathf.PI) / 180);
                pos.y += fireSpeed * Time.deltaTime *
                    Mathf.Sin((rot * Mathf.PI) / 180);
            }
            else
            {
                sprite.sortingOrder = -10;
            }
        }
        else if (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "FlyingAndAttacking")
        {
            sprite.sortingOrder = 0;
        }
        
    }

    void InCaseFireboltGoesFlying()
    {
        if ((pos.x > 10.0f || pos.x < -10.0f || pos.y >= 4.0f))
        {
            if (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase != "WhackADragon"
                && StageTracker.GetComponent<Dragon_StageTracker>().currentPhase != "DiverDown"
                && StageTracker.GetComponent<Dragon_StageTracker>().currentPhase != "PrepareToDive")
            {
                //StageTracker.GetComponent<Dragon_StageTracker>().currentPhase = "Flying";
            }
        }
    }

    void ShootPlayerAfterPoppingUp()
    {
        if (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "WhackADragon")
        {
            if (dragon.transform.position.y >= -2.0f)
            {
                sprite.sortingOrder = 0;
                if (pos.x > -15.0f && pos.x < 15.0f && pos.y < 10.0f && pos.y > -10.0f)
                {
                    pos.x += fireSpeed * Time.deltaTime *
                        Mathf.Cos((rot * Mathf.PI) / 180);
                    pos.y += fireSpeed * Time.deltaTime *
                        Mathf.Sin((rot * Mathf.PI) / 180);
                }
                else
                {
                    StageTracker.GetComponent<Dragon_StageTracker>().currentPhase = "DiverDown";
                }
            }
            else
            {
                sprite.sortingOrder = -10;
                pos.x = dragon.transform.position.x;
                pos.y = dragon.transform.position.y;

                if ((player.transform.position.x - this.transform.position.x) >= 0)
                {
                    rot = 0.0f;
                }
                else if ((player.transform.position.x - this.transform.position.x) < 0)
                {
                    rot = 180.0f;
                }
                this.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);

                /*
                var direction = player.transform.position - transform.position;
                rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
                */
            }
        }
    }

}