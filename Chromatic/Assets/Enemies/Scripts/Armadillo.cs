using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadillo : MonoBehaviour
{
    public float dashSpeed;
    public float jumpTime;
    private float grav;
    public float slashTime;
    // A float between 0 and 1 that indicates how much of the slashTime will have the extended claws
    public float slashHurtPercent;

    public float wanderTime;
    private float wTime;
    //time
    private float time;

    //states/bools
    private bool slashin;

    private int phaseVal;
    private float startTime;
    private float xSpeed;
    private float ySpeed;
    private int prevOrient;

    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        phaseVal = 0;
        startTime = 0.0f;
        slashin = false;
        grav = -32 / Mathf.Pow(jumpTime, 2);
        ySpeed = 0.0f;
        xSpeed = 0.0f;
        prevOrient = 1;
        wTime = wanderTime * Random.Range(0.1f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (phaseVal)
        {
            //dash
            case 0:
                dash();
                if (Mathf.Abs(transform.position.x) > 8 || slashin)
                {
                    phaseVal = 2;
                    startTime = time;
                    slashin = false;
                }
                break;
            case 1:
                jump();
                if (time - startTime >= jumpTime)
                {
                    phaseVal = 2;
                    startTime = time;
                    slashin = false;
                    transform.position = new Vector2(transform.position.x + xSpeed * Time.deltaTime, -3.0f);
                }
                break;
            //slash
            case 2:
                slash();
                if (time - startTime >= slashTime)
                {
                    slashin = false;
                    transform.Translate(-prevOrient * 2.0f, 0.0f, 0.0f);
                    phaseVal = 8;
                    startTime = time;
                }
                break;
            //in between/wanders and chooses next attack
            case 8:
                transform.Translate(orient() * 0.05f, 0.0f, 0.0f);
                if (time - startTime >= wTime)
                {
                    slashin = false;
                    phaseVal = (int)((target.position.y < -3) ? Random.Range(0.0f, 1.5f) : Random.Range(0.5f, 1.999f));
                    prevOrient = orient();
                    wTime = wanderTime * Random.Range(0.1f, 1.0f);
                    startTime = time;
                }
                break;
        }
        time += Time.deltaTime;
    }

    private void dash()
    {
        if (Mathf.Pow(Mathf.Abs(transform.position.x - target.position.x), 2) + 3.5 * Mathf.Pow(Mathf.Abs(transform.position.y - target.position.y), 2) < 25)
        {
            slashin = true;
        }
        transform.Translate(prevOrient * dashSpeed, 0.0f, 0.0f);
    }

    private void jump()
    {
        if(!slashin)
        {
            slashin = true;
            xSpeed = orient() * Mathf.Abs(target.position.x - transform.position.x) / jumpTime;
            ySpeed = -grav * jumpTime / 2;
        }
        transform.position = new Vector2(transform.position.x + xSpeed * Time.deltaTime, transform.position.y + ySpeed * Time.deltaTime);
        ySpeed += grav * Time.deltaTime;
    }

    private void slash()
    {
        //build-up
        if (time - startTime < slashTime * (1 - slashHurtPercent))
        {
            transform.localScale = new Vector3(1.5f, 2.0f, 1.0f);
        }
        //return to normal
        else if (time - startTime >= slashTime)
        {
            transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        }
        //SLASH!
        else
        {
            if(!slashin)
            {
                slashin = true;
                prevOrient = orient();
                transform.Translate(prevOrient * 2.0f, 0.0f, 0.0f);
            }
            transform.localScale = new Vector3(4.0f, 2.0f, 1.0f);
        }
    }

    private int orient()
    {
        if (target.position.x > transform.position.x)
        {
            return 1;
        }
        return -1;
    }
}
