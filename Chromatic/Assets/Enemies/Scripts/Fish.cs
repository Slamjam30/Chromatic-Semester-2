using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] float moveXRight = 5f;
    [SerializeField] float moveXLeft = 5f;
    [SerializeField] float moveSpeed = 2f;
    [Header("case sensitive left or right")]
    [SerializeField] string direction = "left";

    Vector2 STARTPOS;
    Vector3 waypointRight;
    Vector3 waypointLeft;
    Vector3 moveDistanceRight;
    Vector3 moveDistanceLeft;

    private SpriteRenderer sr;
    private GameObject mainCharacter;

    public float followRangeX = 10;
    public float followRangeY = 20;
    private bool atSpawn = true;

    void Start()
    {
        STARTPOS = transform.position;
        
        mainCharacter = GameObject.FindWithTag("Player");
        moveDistanceRight = new Vector3(moveXRight, 0, 0);
        moveDistanceLeft = new Vector3(moveXLeft, 0, 0);
        waypointRight = this.transform.position + moveDistanceRight;
        waypointLeft = this.transform.position - moveDistanceLeft;
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(STARTPOS);
        if (Mathf.Abs(mainCharacter.transform.position.x - gameObject.transform.position.x) <= followRangeX && Mathf.Abs(mainCharacter.transform.position.y - gameObject.transform.position.y) <= followRangeY)
        {
            atSpawn = false;
            MoveTowardsPlayer();
        }
        else if (atSpawn)
        {
            UpdateX();
        } else 
        {
            MoveTowardsSpawn();
        }

        //To stop from moving forever
        if (Mathf.Abs(transform.position.x - mainCharacter.transform.position.x) > 3.5 || Mathf.Abs(transform.position.y - mainCharacter.transform.position.y) > 3.5)
        { gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); }
    }

    private void UpdateX()
    {
        if (transform.position.x < waypointRight.x && direction == "right")
        {
            MoveRight();

            if (transform.position.x >= waypointRight.x)
            {
                sr.flipX = false;
                direction = "left";
            }
        }

        if (transform.position.x > waypointLeft.x && direction == "left")
        {
            MoveLeft();

            if (transform.position.x <= waypointLeft.x)
            {
                sr.flipX = true;
                direction = "right";
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

    private void MoveTowardsPlayer()
    {
        if (transform.position.x < mainCharacter.transform.position.x)
        { sr.flipX = true; }
        else if (transform.position.x > mainCharacter.transform.position.x)
        { sr.flipX = false; }

        var step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, mainCharacter.transform.position, step);

    }

    private void MoveTowardsSpawn()
    {
        
        var step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, STARTPOS, step);
        if (Mathf.Abs(transform.position.x - STARTPOS.x) <= 1 && Mathf.Abs(transform.position.y - STARTPOS.y) <= 1)
        { atSpawn = true; }

        if (transform.position.x < STARTPOS.x)
        { sr.flipX = true; }
        else if (transform.position.x > STARTPOS.x)
        { sr.flipX = false; }

    }
}
