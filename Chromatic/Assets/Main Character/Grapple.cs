using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    private float move;
    public float swingDistance = 5f;
    private Transform grapplePoint;
    private Transform grappleSwing;
    private GameObject grappleSwingBox;
    GameObject mainChar;
    public float grappleSpeed;
    public bool grappling = false;
    private GameObject[] grapplePoints;
    private GameObject[] grappleSwings;
    public float maxGrappleDistance;
    private float origGravScale;
    //True if grappling to, false if grapple swinging
    private bool grappleTo;

    // Start is called before the first frame update
    void Start()
    {
        grapplePoints = GameObject.FindGameObjectsWithTag("GrapplePoint");
        grappleSwings = GameObject.FindGameObjectsWithTag("GrappleSwing");

        //New array that is the length of GrapplePoints and Swings combined

        string test = " ";
        foreach (GameObject point in grapplePoints)
        {
            test += point.transform.position + " ";
        }
        Debug.Log("GrapplePoints: " + test);
        mainChar = GameObject.FindWithTag("Player");
        Debug.Log("mainChar:" + mainChar);
        origGravScale = mainChar.GetComponent<Rigidbody2D>().gravityScale;

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
        if (grappling && grappleTo)
        {
            GrappleToMove();
        }

        //When grappleTO and reached point, Unlock movement and stop Grappling, and put it on a cooldown
        if (grappleTo && grapplePoint != null && Mathf.Abs(mainChar.transform.position.x - grapplePoint.transform.position.x) <= 0.5f && Mathf.Abs(mainChar.transform.position.x - grapplePoint.transform.position.x) <= 0.5f)
        {
            Reset();
        }

        //Debug.Log("grappling: " + grappling + " grappleTo: " + grappleTo);
        if (grappling && !grappleTo)
        {
            GrappleSwing();
            grappleSwing.gameObject.GetComponent<SpringJoint2D>().distance = 5f;
        }

    }

    public void GrappleSwing()
    {
        //Have G reveal rope and attach to it

        grappleSwingBox = grappleSwing.GetComponent<SimplifiedRopeSwing>().objectThatIsHangingFromTheRope.gameObject;

        mainChar.transform.position = grappleSwingBox.transform.position;


        move = Input.GetAxis("Horizontal");

        grappleSwingBox.GetComponent<Rigidbody2D>().AddForce(new Vector2(move, 0));


        //getComponent ObjectThatIsHanging from SimplifiedRopeSwing





        /*float distance = Mathf.Abs(mainChar.transform.position.x - grappleSwing.position.x);

        if (mainChar.transform.position.x > grappleSwing.position.x)
        {
            Debug.Log("Adding force to left: " + (new Vector2(-mainChar.GetComponent<Rigidbody2D>().mass * (swingDistance - distance) * 1000 * Time.deltaTime, 0)));
            //Add force to left based on distance mainChar.mass * 9.81
            //mainChar.GetComponent<Rigidbody2D>().AddForce(new Vector2(-mainChar.GetComponent<Rigidbody2D>().mass * (swingDistance - distance) * 1000 * Time.deltaTime, 0));
        }

        if (mainChar.transform.position.x < grappleSwing.position.x)
        {
            Debug.Log("Adding force to right: " + (new Vector2(mainChar.GetComponent<Rigidbody2D>().mass * (swingDistance - distance) * 100 * Time.deltaTime, 0)));
            //Add force to left based on distance mainChar.mass * 9.81
            //mainChar.GetComponent<Rigidbody2D>().AddForce(new Vector2(mainChar.GetComponent<Rigidbody2D>().mass * (swingDistance - distance) * 100 * Time.deltaTime, 0));
        }*/


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
        //Sets closest point to grapplePoint
        foreach (GameObject point in grapplePoints)
        {
            if (Mathf.Sqrt(Mathf.Pow(mainChar.transform.position.x - point.transform.position.x, 2) + Mathf.Pow(mainChar.transform.position.y - point.transform.position.y, 2)) < dFromChar)
            {
                //Get the point that is closest to main character
                grapplePoint = point.transform;
                dFromChar = Mathf.Sqrt(Mathf.Pow(mainChar.transform.position.x - point.transform.position.x, 2) + Mathf.Pow(mainChar.transform.position.y - point.transform.position.y, 2));

            }
        }

        //Just a large number of no significance
        dFromChar = Mathf.Pow(maxGrappleDistance, 2);
        //Sets closest swing to grappleSwing
        foreach (GameObject swing in grappleSwings)
        {
            if (Mathf.Sqrt(Mathf.Pow(mainChar.transform.position.x - swing.transform.position.x, 2) + Mathf.Pow(mainChar.transform.position.y - swing.transform.position.y, 2)) < dFromChar)
            {
                //Get the point that is closest to main character
                grappleSwing = swing.transform;
                dFromChar = Mathf.Sqrt(Mathf.Pow(mainChar.transform.position.x - swing.transform.position.x, 2) + Mathf.Pow(mainChar.transform.position.y - swing.transform.position.y, 2));

            }
        }

        //If grapplePoint is closer than grappleSwing then use grapplePoint/You are grapplingTo. If not, use grappleSwing/You are not grapplingTo.
        Debug.Log("GrapplePoint: " + grapplePoint);
        Debug.Log("GrapplePoint: " + grappleSwing);

        if (grapplePoint != null && (grappleSwing == null || Mathf.Sqrt(Mathf.Pow(mainChar.transform.position.x - grapplePoint.transform.position.x, 2) + Mathf.Pow(mainChar.transform.position.y - grapplePoint.transform.position.y, 2)) < Mathf.Sqrt(Mathf.Pow(mainChar.transform.position.x - grappleSwing.transform.position.x, 2) + Mathf.Pow(mainChar.transform.position.y - grappleSwing.transform.position.y, 2))))
        {
            grappleTo = true;
        } else 
        { 
            grappleTo = false; 
        }

        //If GRAPPLING TO and closest point is closer than the max distance in x direction and in y direction, then grapple
        if (grappleTo && (Mathf.Abs(mainChar.transform.position.x - grapplePoint.transform.position.x) <= maxGrappleDistance && Mathf.Abs(mainChar.transform.position.x - grapplePoint.transform.position.x) <= maxGrappleDistance))
        {
            mainChar.GetComponent<Rigidbody2D>().gravityScale = 0;
            grappling = true;
        }

        if (!grappleTo && (Mathf.Abs(mainChar.transform.position.x - grappleSwing.transform.position.x) <= maxGrappleDistance && Mathf.Abs(mainChar.transform.position.x - grappleSwing.transform.position.x) <= maxGrappleDistance))
        {
            grappleSwing.gameObject.GetComponent<LineRenderer>().enabled = true;
            //grappleSwing.gameObject.GetComponent<DistanceJoint2D>().connectedBody = mainChar.GetComponent<Rigidbody2D>();
            //grappleSwing.gameObject.GetComponent<DistanceJoint2D>().distance = swingDistance;
            //mainChar.GetComponent<Rigidbody2D>().mass = 100f;
            //grappleSwing.gameObject.GetComponent<SpringJoint2D>().connectedBody = mainChar.GetComponent<Rigidbody2D>();
            //grappleSwing.gameObject.GetComponent<SpringJoint2D>().distance = 5f;
            mainChar.GetComponent<Rigidbody2D>().gravityScale = 0;
            grappling = true;
        }
    }

    public void Reset()
    {
        mainChar.GetComponent<Movement>().canMove = true;
        grappling = false;
        mainChar.GetComponent<Rigidbody2D>().gravityScale = origGravScale;
        GameObject.Find("FeetCollider").GetComponent<FeetCollider>().GrappleResetJump();
    }
}
