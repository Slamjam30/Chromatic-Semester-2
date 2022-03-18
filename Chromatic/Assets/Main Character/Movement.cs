using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 500.0f;
    private float climbSpeed = 250.0f;
    private float xSpeed;
    private float ySpeed;
    public bool facingRight = true;
    public float ClimbSpeed;
    private bool climbing;
    public Sprite vine;
    public Tilemap background;
    private Vector2 move;
    private ScriptableObject jump;
    private Animator am;
    private SpriteRenderer sr;
    public int KillsForUnfreeze;
    public bool swimming = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        climbing = false;
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Sprite currentSprite = background.GetSprite(new Vector3Int((int)(transform.position.x), (int)transform.position.y, 0));
        xSpeed = Input.GetAxis("Horizontal");
        ySpeed = Input.GetAxis("Vertical");
        
        
        // TODO: change all keycodes into mappable controls
        // if the A key was pressed this frame
       // if (Input.GetKeyDown(KeyCode.A))
       // {
       //     sr.flipX = true;
       //     am.SetInteger("Current", 0);
       // }
       // else if (Input.GetKeyDown(KeyCode.D))
       // {
       //
       //     sr.flipX = false;
       //     am.SetInteger("Current", 0);
       //
       // }
       // else if (xSpeed == 0 && ySpeed == 0)
       // {
       //     am.SetInteger("Current", 3);
       // }


        if (Input.GetKeyDown(KeyCode.F)){
            this.transform.GetChild(0).GetComponent<PlayerAttack>();
        }


        if (Input.GetKeyDown(KeyCode.Space) && !climbing)
        {
            // Calls FeetCollider.cs and allows double jump.
            this.transform.GetChild(0).GetComponent<FeetCollider>().DoubleJump();
            //am.SetInteger("Current", 1);
        }
  
        
        if (currentSprite != null && currentSprite.Equals(vine) && !climbing && Input.GetAxis("Vertical") != 0) // holding up or down in front of a vine sprite
        {
            climbing = true;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            //am.SetInteger("Current", 2);
        }
        else if (climbing && (currentSprite == null || !currentSprite.Equals(vine)))
        {
            rb.gravityScale = 1;
            climbing = false;
        }
        if (!climbing)
        {
            ySpeed = 0;
        }
        move = new Vector2(xSpeed, ySpeed);

        //animator state machine updater
        if (rb.velocity.x != 0)
        {
            am.SetBool("isMoving", true);
        }
        else
        {
            am.SetBool("isMoving", false);
        }
    }

    // FixedUpdate is called once per physics tic
    private void FixedUpdate()
    {
        if (climbing)
        {
            Vector3 climbVelocity = new Vector2(xSpeed * 0.5f * climbSpeed * Time.fixedDeltaTime, ySpeed * climbSpeed * Time.fixedDeltaTime);
            rb.velocity = climbVelocity;
            am.SetBool("isClimbing", true);
        }
        else
        {
            Vector3 newVelocity = new Vector2(xSpeed * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
            rb.velocity = newVelocity;
            am.SetBool("isClimbing", false);
        }
        if (rb.velocity.x > 0 && !facingRight)
        {
            FlipSprite();
        }
        if (rb.velocity.x < 0 && facingRight)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1;
        transform.localScale = flipScale;
        facingRight = !facingRight;
    }
}
