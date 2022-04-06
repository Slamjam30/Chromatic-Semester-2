using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    private Transform grapplePoint;
    GameObject mainChar;
    public float grappleSpeed;
    public bool grappling = false;
    private GameObject[] grapplePoints;
    public float maxGrappleDistance;
    private float origGravScale;

    // Start is called before the first frame update
    void Start()
    {
        grapplePoints = GameObject.FindGameObjectsWithTag("GrapplePoint");
        string test = " ";
        foreach (GameObject point in grapplePoints)
        {
            test += point.transform.position + " ";
        }
        Debug.Log("GrapplePoints: " + test);
        mainChar = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        //Stops grappling if press G while grappling (MUST COME FIRST in the script)
        if (Input.GetKeyDown(KeyCode.Space) && grappling)
        {
            Reset();
        }

        //Get closest grapplePoint if within a certain distance
        if (Input.GetKeyDown(KeyCode.G) && !grappling)
        {
            StartGrapple();
        }


        //Lock out movement and moveTowards grapple point
        if (grappling)
        {
            GrappleToMove();
        }

        //Unlock movement and stop Grappling, and put it on a cooldown
        if (grapplePoint != null && Mathf.Abs(mainChar.transform.position.x - grapplePoint.transform.position.x) <= 0.5f && Mathf.Abs(mainChar.transform.position.x - grapplePoint.transform.position.x) <= 0.5f)
        {
            Reset();
        }

    }

    public void GrappleToMove()
    {
        mainChar.GetComponent<Movement>().canMove = false;
        mainChar.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        var step = grappleSpeed * Time.deltaTime;
        mainChar.transform.position = Vector2.MoveTowards(new Vector2(mainChar.transform.position.x, mainChar.transform.position.y), new Vector2(grapplePoint.position.x, grapplePoint.position.y), step);
    }

    public void StartGrapple()
    {
        //Just a large number of no significance
        float dFromChar = Mathf.Pow(maxGrappleDistance, 2);
        foreach (GameObject point in grapplePoints)
        {
            if (Mathf.Sqrt(Mathf.Pow(mainChar.transform.position.x - point.transform.position.x, 2) + Mathf.Pow(mainChar.transform.position.y - point.transform.position.y, 2)) < dFromChar)
            {
                //Get the point that is closest to main character
                grapplePoint = point.transform;
                dFromChar = Mathf.Sqrt(Mathf.Pow(mainChar.transform.position.x - point.transform.position.x, 2) + Mathf.Pow(mainChar.transform.position.y - point.transform.position.y, 2));

            }
        }
        //If closest point is closer than the max distance in x direction and in y direction, then grapple
        if (Mathf.Abs(mainChar.transform.position.x - grapplePoint.transform.position.x) <= maxGrappleDistance && Mathf.Abs(mainChar.transform.position.x - grapplePoint.transform.position.x) <= maxGrappleDistance)
        {
            origGravScale = mainChar.GetComponent<Rigidbody2D>().gravityScale;
            mainChar.GetComponent<Rigidbody2D>().gravityScale = 0;
            grappling = true;
        }
    }

    public void Reset()
    {
        mainChar.GetComponent<Movement>().canMove = true;
        grappling = false;
        mainChar.GetComponent<Rigidbody2D>().gravityScale = origGravScale;
        mainChar.GetComponent<FeetCollider>().GrappleResetJump();
    }
}
