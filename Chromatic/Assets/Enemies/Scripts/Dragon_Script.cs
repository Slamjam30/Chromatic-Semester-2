using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Script : MonoBehaviour
{
    public GameObject fireBoltFlight;
    public GameObject StageTracker;
    public GameObject divingSpotLeft;
    public GameObject divingSpotRight;
    public GameObject firebolt;

    Vector2 pos;

    private float startXPos;
    private float startYPos;
    public float rot;

    private bool rotateTowardsPlayer;

    public Transform player;
    public Transform dragon;

    private float flyingSpeed = 5.0f;
    private float fallingSpeed = 5.0f;
    private string flightDirection = "right";



    // Start is called before the first frame update
    void Start()
    {

        // Set dragon start position
        if (this.gameObject.name == "Dragon")
        {
            startXPos = -8.3f;
            startYPos = 2.9f;
            rot = 0f;
            transform.position = new Vector2(startXPos, startYPos);
            transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }

        rotateTowardsPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        pos = gameObject.transform.position;

        facePlayer();
        dragonFlyingMovement();
        RiseUp();
        DiveIntoTheLava();
        DragonDive();
        WhackADragon();
        DiveBackDownIntoLava();
        TimeToDie();

        transform.position = pos;
    }

    void facePlayer()
    {
        if (/*(StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "Flying")
            || (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "FlyingAndAttacking")*/
            StageTracker.GetComponent<Dragon_StageTracker>().currentPhase != "WhackADragon"
            && StageTracker.GetComponent<Dragon_StageTracker>().currentPhase != "DiverDown"
            && StageTracker.GetComponent<Dragon_StageTracker>().currentPhase != "DragonDive")
        {
            var direction = player.transform.position - transform.position;
            rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
            if (rot < -90f)
            {
                this.transform.Rotate(180f, 0, 0, Space.Self);
            }
        }

    }

    void dragonFlyingMovement()
    {
        if ((StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "Flying") 
            || (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "FlyingAndAttacking"))
        {
            if (flightDirection == "right")
            {
                pos.x += flyingSpeed * Time.deltaTime;
                if (pos.x >= (startXPos * -1)) {
                    flightDirection = "left";
                }
            }
            else if (flightDirection == "left") {
                pos.x -= flyingSpeed * Time.deltaTime;
                if (pos.x <= startXPos)
                {
                    flightDirection = "right";
                }
            }

            if (pos.y < startYPos)
            {
                if (pos.x <= 0)
                {
                    var direction = divingSpotLeft.transform.position - transform.position;
                    rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    pos.x += flyingSpeed * Time.deltaTime * Mathf.Cos((rot * Mathf.PI) / 180);
                    pos.y += flyingSpeed * Time.deltaTime * Mathf.Sin((rot * Mathf.PI) / 180);

                    float distance = Vector3.Distance(dragon.transform.position, divingSpotLeft.transform.position);
                }
                else if (pos.x > 0)
                {
                    var direction = divingSpotRight.transform.position - transform.position;
                    rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    pos.x += flyingSpeed * Time.deltaTime * Mathf.Cos((rot * Mathf.PI) / 180);
                    pos.y += flyingSpeed * Time.deltaTime * Mathf.Sin((rot * Mathf.PI) / 180);

                    float distance = Vector3.Distance(dragon.transform.position, divingSpotRight.transform.position);
                }
            }
        }
        
    }

    void RiseUp()
    {
        if (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "RiseUp")
        {
            if (pos.y < startYPos)
            {
                pos.y += flyingSpeed * Time.deltaTime;
            }
            else
            {
                StageTracker.GetComponent<Dragon_StageTracker>().currentPhase = "Flying";
            }
        }
    }
    

    
    void DiveIntoTheLava()
    {
        if ((StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "PrepareToDive"))
        {
            //Go to designated diving spot
            if (pos.x <= 0)
            {
                var direction = divingSpotLeft.transform.position - transform.position;
                rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                pos.x += flyingSpeed * Time.deltaTime * Mathf.Cos((rot * Mathf.PI) / 180);
                pos.y += flyingSpeed * Time.deltaTime * Mathf.Sin((rot * Mathf.PI) / 180);

                float distance = Vector3.Distance(dragon.transform.position, divingSpotLeft.transform.position);
                if (distance <= 0.5f)
                {
                    StageTracker.GetComponent<Dragon_StageTracker>().currentPhase = "DragonDive";
                }
            }
            else if (pos.x > 0)
            {
                var direction = divingSpotRight.transform.position - transform.position;
                rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                pos.x += flyingSpeed * Time.deltaTime * Mathf.Cos((rot * Mathf.PI) / 180);
                pos.y += flyingSpeed * Time.deltaTime * Mathf.Sin((rot * Mathf.PI) / 180);

                float distance = Vector3.Distance(dragon.transform.position, divingSpotRight.transform.position);
                if (distance <= 0.5f)
                {
                    StageTracker.GetComponent<Dragon_StageTracker>().currentPhase = "DragonDive";
                }
            }

            
        }
    }

    void DragonDive()
    {
        if (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "DragonDive")
        {
            rot = 0.0f;
            this.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);

            if (pos.y > -5.0f)
            {
                pos.y -= fallingSpeed * Time.deltaTime;
            }
            else
            {
                pos.x = ChooseRandomSpawnPoint();
                StageTracker.GetComponent<Dragon_StageTracker>().currentPhase = "WhackADragon";
            }
        }
    }

    void WhackADragon()
    {
        if (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "WhackADragon")
        {
            if (pos.y < -2.0f)
            {
                pos.y += flyingSpeed * Time.deltaTime;

                if ((player.transform.position.x - this.transform.position.x) >= 0)
                {
                    rot = 0.0f;
                }
                else if ((player.transform.position.x - this.transform.position.x) < 0)
                {
                    rot = 180.0f;
                }
                this.transform.Rotate(0f, rot, 0, Space.Self);
            }
        }
    }

    void DiveBackDownIntoLava()
    {
        if (StageTracker.GetComponent<Dragon_StageTracker>().currentPhase == "DiverDown")
        {
            if (pos.y > -6.0f)
            {
                pos.y -= flyingSpeed * Time.deltaTime;
            }
            else
            {
                if ((dragon.GetComponent<Health>().octoHeadHealth == 
                    StageTracker.GetComponent<Dragon_StageTracker>().prevHealth))
                {
                    pos.x = ChooseRandomSpawnPoint();
                    StageTracker.GetComponent<Dragon_StageTracker>().currentPhase = "WhackADragon";
                }
            }
        }
    }

    float ChooseRandomSpawnPoint()
    {
        float spawnXPos = Random.Range(1f, 5f);
        if (spawnXPos < 2)
        {
            spawnXPos = -6.0f;
        }
        else if (spawnXPos >= 2 && spawnXPos < 3)
        {
            spawnXPos = -2.0f;
        }
        else if (spawnXPos >= 3 && spawnXPos < 4)
        {
            spawnXPos = 2.0f;
        }
        else if (spawnXPos >= 4 && spawnXPos <= 5)
        {
            spawnXPos = 6.0f;
        }
        return spawnXPos;
    }

    void TimeToDie()
    {
        if (dragon.GetComponent<Health>().octoHeadHealth < 1)
        {
            StageTracker.GetComponent<Dragon_StageTracker>().currentPhase = "DieAndFlyUp";
        }
    }
    

}
