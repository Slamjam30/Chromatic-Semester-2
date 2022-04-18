using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    public float timeBetweenBlows;
    private bool blowing;
    public float pushAcceleration;
    private GameObject mainChar;
    private bool done = true;
    private bool inWind;
    private Animator am;

    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindWithTag("Player");
        blowing = true;
        inWind = false;
        am = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //When blowing, add acceleration to inWind object
        if (blowing && inWind && !mainChar.GetComponent<Bubble>().getActive())
        {
            if (mainChar.GetComponent<Dash>().isDashing)
            {
                mainChar.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            }
            else
            {
                mainChar.GetComponent<Rigidbody2D>().AddForce(new Vector2(mainChar.GetComponent<Rigidbody2D>().mass * pushAcceleration, 0.1f));
            }
            
        }
        //TO-DO: TURN A CANVAS OF WIND PARTICLES ON/OFF WITH BLOWING IF inWind

        if (done)
        {
            StartCoroutine(WindCooldown());
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inWind = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inWind = false;
        }
    }

    public IEnumerator WindCooldown()
    {
        done = false;
        Debug.Log("Running WindCooldown()");
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        am.SetBool("Blowing", true);
        //Waits .663 seconds- the amount of time for the wind to fill the wind box in the animation.
        yield return new WaitForSeconds(.633f);
        blowing = true;
        //Sprite enables and animation runs
        yield return new WaitForSeconds(timeBetweenBlows);
        //Disables SpriteRenderer (will give animation enough time to finish) and returns to no_wind animation
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        am.SetBool("Blowing", false);
        blowing = false;
        yield return new WaitForSeconds(timeBetweenBlows);
        done = true;
    }
}
