using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFly : MonoBehaviour
{
    private GameObject mainCharacter;
    [SerializeField] float moveXRight = 5f;
    [SerializeField] float moveXLeft = 5f;
    [SerializeField] float moveSpeed = 4f;
    [Header("case sensitive left or right")]
    [SerializeField] string direction = "left";

    Vector3 waypointRight;
    Vector3 waypointLeft;
    Vector3 moveDistanceRight;
    Vector3 moveDistanceLeft;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // flying variables
    private float gravScale;
    [Header("Adjust Flap Speed and Rigidbody gravity scale")]
    [SerializeField] float flapSpeed = 2.1f;
    private Vector3 playerPosition;
    private Vector3 startPosition;
    private bool diving;
    private bool positionsGrabbed = false;
    private bool moveTowardsPlayer = true;
    private bool moveTowardsStart = false;
    private bool diveWait = true;
    private bool diveWaitDone = false;

    void Start()
    {
        startPosition = gameObject.transform.position;

        mainCharacter = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        gravScale = rb.gravityScale;

        moveDistanceRight = new Vector3(moveXRight, 0, 0);
        moveDistanceLeft = new Vector3(moveXLeft, 0, 0);
        waypointRight = this.transform.position + moveDistanceRight;
        waypointLeft = this.transform.position - moveDistanceLeft;

        StartCoroutine(Dive());
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(mainCharacter.transform.position.x - gameObject.transform.position.x) <= 10)
        {
            //diving is true after Start()
            if (diving)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(0, 0);
                rb.inertia = 0;

                //diveWait starts as true
                if (diveWait)
                { StartCoroutine(DiveWait()); }

                //diveWaitDone is turned to true after a time in Coroutine(DiveWait())
                if (diveWaitDone)
                {
                    //positionsGrabbed starts as false
                    if (!positionsGrabbed)
                    {
                        startPosition = new Vector2(gameObject.transform.position.x, startPosition.y);
                        playerPosition = mainCharacter.transform.position;
                        positionsGrabbed = true;
                    }

                    var step = moveSpeed * 3f * Time.deltaTime;
                    if (transform.position != playerPosition && moveTowardsPlayer)
                    {
                        //Moves towards the player using variable step (relying on moveSpeed)
                        transform.position = Vector2.MoveTowards(transform.position, playerPosition, step);

                        //Change sprite direction
                        if (transform.position.x >= playerPosition.x)
                        {
                            direction = "left";
                            sr.flipX = false;
                        } 
                        else if (transform.position.x <= playerPosition.x)
                        {
                            direction = "right";
                            sr.flipX = true;
                        }

                        if (transform.position == playerPosition)
                        {
                            moveTowardsPlayer = false;
                            moveTowardsStart = true;
                        }
                    }

                    step = moveSpeed * Time.deltaTime;
                    if (transform.position != startPosition && moveTowardsStart)
                    {
                        //Moves towards its start position DOESN"T MOVE IN NEGATIVE DIRECTION?
                        transform.position = Vector2.MoveTowards(transform.position, startPosition, step);

                        //has a weird glitch where it will stop moving in X direction to move towards, so just test if it reaches its same y position

                        if (Mathf.Abs(transform.position.y - startPosition.y) < 0.2f)
                        { moveTowardsStart = false; }

                        

                    }

                    if (!moveTowardsStart && !moveTowardsPlayer)
                    {
                        rb.inertia = 1;
                        rb.gravityScale = gravScale;

                        diveWait = true;
                        diveWaitDone = false;
                        positionsGrabbed = false;
                        moveTowardsPlayer = true;
                        moveTowardsStart = true;

                        StartCoroutine(Dive());
                    }
                }
            }
        }

        if (!diving)
        {
            if (transform.position.y < waypointRight.y - .5f)
            {
                rb.velocity = new Vector2(0, flapSpeed);
            }
            UpdateX();
        }

        else
        {
            UpdateX();
        }

    }
    
            

    
    private void UpdateX()
    {
        //UpdateX is only called when not diving
        if (transform.position.x < waypointRight.x && direction == "right")
        {
            MoveRight();

            if (transform.position.x >= waypointRight.x)
            {
                direction = "left";
                sr.flipX = false;
            }
        }

        if (transform.position.x > waypointLeft.x && direction == "left")
        {
            MoveLeft();

            if (transform.position.x <= waypointLeft.x)
            {
                direction = "right";
                sr.flipX = true;
            }
        }
    }

    private void MoveRight()
    {
        var step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(waypointRight.x, transform.position.y, transform.position.z), step);

    }

    private void MoveLeft()
    {
        var step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(waypointLeft.x, transform.position.y, transform.position.z), step);
    }

    private IEnumerator Dive()
    {
        diving = false;
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        diving = true;
    }
    private IEnumerator DiveWait()
    {
        diveWait = false;
        yield return new WaitForSeconds(1f);
        diveWaitDone = true;
    }
}
