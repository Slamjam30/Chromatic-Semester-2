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

    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindWithTag("Player");
        blowing = true;
        inWind = false;
    }

    // Update is called once per frame
    void Update()
    {
        //When blowing, add acceleration to inWind object
        if (blowing && inWind && !mainChar.GetComponent<Bubble>().getActive())
        {
            mainChar.GetComponent<Rigidbody2D>().AddForce(new Vector2(mainChar.GetComponent<Rigidbody2D>().mass * pushAcceleration, 0.1f));

        }
        //TO-DO: TURN A CANVAS OF WIND PARTICLES ON/OFF WITH BLOWING IF inWind

        if (done)
        {
            StartCoroutine(WindCooldown());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inWind = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inWind = false;
    }

    public IEnumerator WindCooldown()
    {
        done = false;
        Debug.Log("Running WindCooldown()");
        blowing = true;
        yield return new WaitForSeconds(timeBetweenBlows);
        blowing = false;
        yield return new WaitForSeconds(timeBetweenBlows);
        done = true;
    }
}
