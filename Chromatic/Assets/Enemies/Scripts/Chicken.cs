using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] float maxLeft = 25;
    [SerializeField] float maxRight = 25;
    [SerializeField] float jumpForce = 10;
    //[SerializeField] float maxJumpHeight = 10;
    [SerializeField] float moveSpeed = 2;
    enum Direction {Left, Right}
    [SerializeField] Direction startDirection = Direction.Left;

    private Rigidbody2D rb;
    private Animator am;
    private Direction currDirection;
    private Vector2 origin;
    public bool jumping; // make this private
    public bool canJump;  //this too    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currDirection = startDirection;
        origin = rb.position;
        jumping = false;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Calls this mess once every physics update (1/60 of a second)
    private void FixedUpdate()
    {
        if (Random.Range(0, 250) == 1) // should be 1/1000 chance for (0, 500)
        {
            jumping = true;
        }

        if (canJump && jumping)
        {
            canJump = false; // no jumping while jumping
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            return; //don't move
        }

        if (jumping)
        {
            if (rb.position.y <= origin.y) // if current position is below where it started
            {
                rb.position = new Vector2(rb.position.x, origin.y); // snap it to its starting height (probably look like butt)
                rb.velocity = Vector2.zero;
                jumping = false;
                wait(1.0f);
                canJump = true; // its back on its staring height so it can jump again
            }
        }

        if (currDirection == Direction.Right && !jumping)
        {
            rb.MovePosition(new Vector2(rb.position.x + moveSpeed * Time.fixedDeltaTime, origin.y));
            if (rb.position.x >= origin.x + maxRight)
            {
                currDirection = Direction.Left;
                return;
            }
        }

        if (currDirection == Direction.Left && !jumping)
        {
            rb.MovePosition(new Vector2(rb.position.x - moveSpeed * Time.fixedDeltaTime, origin.y));
            if (rb.position.x <= origin.x - maxLeft)
            {
                currDirection = Direction.Right;
                return;
            }
        }
    }


    IEnumerator wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
