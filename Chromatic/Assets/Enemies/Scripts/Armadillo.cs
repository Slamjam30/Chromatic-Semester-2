using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadillo : MonoBehaviour
{
    public float startY;
    public float startX;
    public float stageWidth;
    public float dashSpeed;
    public float jumpTime;
    public float jumpHeight;
    private float grav;
    public float slashTime;
    // A float between 0 and 1 that indicates how much of the slashTime will have the extended claws
    public float slashHurtPercent;

    public float wanderTime;
    public float downedTime;
    private float wTime;
    //time
    public float time;

    //states/bools
    public bool slashin;

    public int phaseVal;
    public float startTime;
    private float xSpeed;
    private float ySpeed;
    private int prevOrient;

    public Transform target;

    public GameObject armadilloVisual;
    private Animator am;
    private bool slashAnimed;

    private GameObject mainChar;

    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindWithTag("Player");
        am = armadilloVisual.GetComponent<Animator>();
        time = 0.0f;
        phaseVal = 8;
        startTime = 0.0f;
        slashin = false;
        grav = -(2 * jumpHeight * jumpHeight) / Mathf.Pow(jumpTime, 2);
        ySpeed = 0.0f;
        xSpeed = 0.0f;
        prevOrient = 1;
        wTime = wanderTime * Random.Range(0.1f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainChar.transform.position.x < gameObject.transform.position.x)
        {
            armadilloVisual.GetComponent<SpriteRenderer>().flipX = false;
            armadilloVisual.transform.rotation = new Quaternion(0, 0, 0.144952312f, 0.989438713f);
        } else
        {
            armadilloVisual.GetComponent<SpriteRenderer>().flipX = true;
            armadilloVisual.transform.rotation = new Quaternion(0, 0, -0.165858865f, 0.986149549f);
        }




        armadilloVisual.transform.position = gameObject.transform.position;

        switch (phaseVal)
        {
            //dash
            case 0:
                dash();
                if (Mathf.Abs(transform.position.x - startX) > stageWidth || slashin)
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
                    transform.position = new Vector2(transform.position.x + xSpeed * Time.deltaTime, startY);
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
            //downed state
            case 3:
                am.SetBool("Slash", false);
                am.SetBool("Downed", true);
                am.SetBool("Rolling", false);
                if (transform.position.y > startY)
                {
                    transform.Translate(0.0f, -0.1f, 0.0f);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x, startY);
                }
                if (time - startTime >= wanderTime)
                {
                    slashin = false;
                    startTime = time;
                    phaseVal = 8;
                }
                break;
            //in between/wanders and chooses next attack
            case 8:
                am.SetBool("Slash", false);
                am.SetBool("Downed", false);
                am.SetBool("Rolling", false);
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
        am.SetBool("Slash", false);
        am.SetBool("Downed", false);
        am.SetBool("Rolling", true);
        if (Mathf.Pow(Mathf.Abs(transform.position.x - target.position.x), 2) + 3.5 * Mathf.Pow(Mathf.Abs(transform.position.y - target.position.y), 2) < 16)
        {
            slashin = true;
        }
        transform.Translate(prevOrient * dashSpeed, 0.0f, 0.0f);
    }

    private void jump()
    {
        am.SetBool("Slash", false);
        am.SetBool("Downed", false);
        am.SetBool("Rolling", true);
        if (!slashin)
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
            if (!slashin)
            {
                slashin = true;
                prevOrient = orient();
                transform.Translate(prevOrient * 2.0f, 0.0f, 0.0f);
            }
            am.SetBool("Slash", true);
            am.SetBool("Downed", false);
            am.SetBool("Rolling", false);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Woter")
        {
            Debug.Log("Yoooo its totaly working yay");
            phaseVal = 3;
            startTime = time;
            slashin = false;
            transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        }
    }

    private IEnumerator TurnOffJump()
    {
        yield return new WaitForSeconds(0.083f);
        am.SetBool("Jump", false);
    }

}
