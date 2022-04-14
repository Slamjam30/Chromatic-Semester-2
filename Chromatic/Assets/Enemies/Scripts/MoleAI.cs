using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleAI : MonoBehaviour
{
    private GameObject mainCharacter;
    private bool burrowed;
    private bool playerTrigger; // makes the wakeup routine only trigger once
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator am;
    private Collider2D wakeUpRange;
    private CapsuleCollider2D collisionBox;
    private float gravity;
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpForce = 50f;
    [SerializeField] float timeToChase = 2f;
    private Vector2 targetPos;

    public float FOLLOW_RANGE = 10f;

    private bool switchDir;
    private bool switching;

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.FindWithTag("Player");
        burrowed = true;
        playerTrigger = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        wakeUpRange = gameObject.GetComponentInChildren<BoxCollider2D>();
        gravity = rb.gravityScale;
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.enabled = false;
        collisionBox = gameObject.GetComponent<CapsuleCollider2D>();
        rb.gravityScale = 0;
        collisionBox.enabled = false;
        am = gameObject.GetComponent<Animator>();

        switching = false;
        switchDir = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called once per physics frame (1/60 sec)
    private void FixedUpdate()
    {
        if (!burrowed && Mathf.Abs(mainCharacter.transform.position.x - gameObject.transform.position.x) <= FOLLOW_RANGE && !switchDir)
        {
            am.SetBool("Moving", true);

            if (mainCharacter.transform.position.x - gameObject.transform.position.x > 0)
            {
                //Player is on right
                if (sr.flipX == true)
                {
                    switchDir = true;
                }
                sr.flipX = false;
            } else
            {
                //Player is on left
                if (sr.flipX == false)
                {
                    switchDir = true;
                }
                sr.flipX = true;
            }

            targetPos = new Vector2(mainCharacter.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);
        } 
        else if (!burrowed)
        {
            am.SetBool("Moving", false);
        }

        if (switchDir && !switching)
        {
            //Makes it take a second (not literally 1 second) to switch directions
            StartCoroutine(SwitchDirection());

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" & burrowed && !playerTrigger)
        {
            //Debug.Log("Player entered wakeup range");
            wakeUpRange.enabled = false; //turns off wakeup trigger so it doesn't try to wakeup again
            playerTrigger = true; // makes it so it only triggers once per frame
            StartCoroutine(Awaken());
            
        }
    }

    private IEnumerator Awaken()
    {
        am.SetBool("Peek", true);
        yield return new WaitForSeconds(1f);
        JumpUp();
        rb.gravityScale = gravity;
        collisionBox.enabled = true;
        yield return new WaitForSeconds(timeToChase);
        burrowed = false;
        //Debug.Log("Mole Awake");
    }

    private void JumpUp()
    {
        sr.enabled = true;
        am.SetBool("PopOut", true);
        //Debug.Log("Mole Jumping");
        rb.AddForce(new Vector2(0f, jumpForce),ForceMode2D.Impulse);
        //Vector2 jumpVector = new Vector2(transform.position.x, transform.position.y + jumpApex);
        //transform.position = Vector2.Lerp(transform.position, jumpVector, jumpSpeed * Time.deltaTime);
    }

    private IEnumerator SwitchDirection()
    {
        switching = true;
        yield return new WaitForSeconds(1f);
        switchDir = false;
        switching = false;
    }
}
