using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    private int jumpCounter;
    private Rigidbody2D rb;
    private Animator am;

    // Start is called before the first frame update
    void Start()
    {
        jumpCounter = 2;
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        am = transform.parent.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Resets jump for the collider off the feet
        
        jumpCounter = 2;
        am.SetInteger("Current", 0);
        am.SetInteger("DoubleJump", 0);
    }

    public void DoubleJump()
    {
        if (jumpCounter != 0)
        {
            am.SetInteger("DoubleJump", 1);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
            if (jumpCounter == 1)
            {
                //Adding more force to second jump to combat gravity
                rb.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            }
            jumpCounter--;
        }

    }
}
