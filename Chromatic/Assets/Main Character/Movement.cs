using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 9.0f;
    float gravscale;
    public float ClimbSpeed;
    public bool hasRed = false;
    public bool hasBlue = false;
    public bool hasYellow = false;
    public bool hasGreen = false;
    private bool climbing;
    public Sprite vine;
    public Tilemap background;
    private Vector2 move;
    private ScriptableObject jump;
    private Animator am;
    private SpriteRenderer sr;
    public int KillsForUnfreeze;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gravscale = rb.gravityScale;
        climbing = false;
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Sprite currentSprite = background.GetSprite(new Vector3Int((int)(transform.position.x), (int)transform.position.y, 0));
        float xSpeed = Input.GetAxis("Horizontal");
        float ySpeed = Input.GetAxis("Vertical");
        
        

        // if the A key was pressed this frame
        if (Input.GetKeyDown(KeyCode.A))
        {
            sr.flipX = true;
            am.SetInteger("Current", 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {

            sr.flipX = false;
            am.SetInteger("Current", 0);

        }
        else if (xSpeed == 0 && ySpeed == 0)
        {
            am.SetInteger("Current", 3);
        }


        if (Input.GetKeyDown(KeyCode.F)){
            this.transform.GetChild(0).GetComponent<PlayerAttack>();
        }


        if (Input.GetKeyDown(KeyCode.Space) && !climbing)
        {
            // Calls FeetCollider.cs and allows double jump.
            this.transform.GetChild(0).GetComponent<FeetCollider>().DoubleJump();
            am.SetInteger("Current", 1);
        }
  
        
        if (currentSprite != null && currentSprite.Equals(vine) && !climbing)
        {
            climbing = true;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            am.SetInteger("Current", 2);
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
        transform.Translate(move * Time.deltaTime * moveSpeed);

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "red")
        {
            hasRed = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "blue")
        {
            hasBlue = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "green")
        {
            hasGreen = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "yellow")
        {
            hasYellow = true;
            Destroy(collision.gameObject);
        }
    }
}
