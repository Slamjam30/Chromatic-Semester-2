using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_StageTracker : MonoBehaviour
{
    public string currentPhase;

    public float timeRemaining;
    public float timeRemainingBeforeDiving;

    public bool targetFireAtPlayer;

    public float fireboltRotation;

    public Transform player;
    public Transform dragon;
    public Transform firebolt;

    public int prevHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = "Flying";
        timeRemaining = 2.0f;
        timeRemainingBeforeDiving = 15.0f;
        targetFireAtPlayer = false;

        prevHealth = 6;;
    }

    // Update is called once per frame
    void Update()
    {
        CountDownInFlight();
        CountDownBeforeDiving();
        GoBackToFlying();
    }

    void CountDownInFlight()
    {
        if (currentPhase == "Flying")
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                firebolt.GetComponent<FireBolt_Script>().targetingPlayer = true;
            }
            else if (timeRemaining <= 0)
            {
                firebolt.GetComponent<FireBolt_Script>().targetingPlayer = false;
                currentPhase = "FlyingAndAttacking";
            }
        }
    }

    void CountDownBeforeDiving()
    {
        if (currentPhase == "Flying" || currentPhase == "FlyingAndAttacking")
        {
            if (timeRemainingBeforeDiving > 0)
            {
                timeRemainingBeforeDiving -= Time.deltaTime;
            }
            else
            {
                firebolt.GetComponent<FireBolt_Script>().targetingPlayer = false;
                currentPhase = "PrepareToDive";
            }
        }
    }

    void GoBackToFlying()
    {
        if (dragon.GetComponent<Health>().octoHeadHealth < prevHealth)
        {
            currentPhase = "Flying";
            timeRemainingBeforeDiving = 15.0f;
            prevHealth = dragon.GetComponent<Health>().octoHeadHealth;
        }
    }

}
