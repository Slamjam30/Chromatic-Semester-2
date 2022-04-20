using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    private int jumpCounter;
    private Rigidbody2D rb;
    private Animator am;
    private LayerMask Default = 0;
    public bool noAbility;

    // Start is called before the first frame update
    void Start()
    {
        noAbility = false;
        jumpCounter = 2;
        //rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        rb = GetComponentInParent<Rigidbody2D>();
        //am = transform.parent.gameObject.GetComponent<Animator>();
        am = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Happened");
        // Resets jump for the collider off the feet
        if (collision.collider.IsTouchingLayers(Default))
        {
            jumpCounter = 2;
            am.SetBool("isJumping", false);
            am.SetBool("isDoubleJump", false);
        }
       // am.SetInteger("Current", 0);
       // am.SetInteger("DoubleJump", 0);
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Untagged")
        {
            jumpCounter = 2;
            am.SetBool("isJumping", false);
            am.SetBool("isDoubleJump", false);
            am.SetBool("isFalling", false);
        }
    }
    public void DoubleJump()
    {
        if (jumpCounter != 0 && !GetComponentInParent<Movement>().swimming && !(jumpCounter < 2 && noAbility))
        {
            //am.SetInteger("DoubleJump", 1);
            am.SetBool("isJumping", true);
            am.SetBool("isFalling", false);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
            if (jumpCounter == 1)
            {
                am.SetBool("isDoubleJump", true);
                am.SetBool("isFalling", false);
                //Adding more force to second jump to combat gravity
                rb.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            }
            jumpCounter--;
        }

        //Blunted and single jump if swimming
        if (jumpCounter > 1 && GetComponentInParent<Movement>().swimming)
        {
            am.SetBool("isJumping", true);
            am.SetBool("isFalling", false);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            jumpCounter--;
        }

    }

    public void EnterFallingAnim()
    {
        am.SetBool("isFalling", true);
    }

    public void GrappleResetJump()
    {
        jumpCounter = 2;
        am.SetBool("isJumping", false);
        am.SetBool("isDoubleJump", false);
    }

    public int GetJumpCounter()
    {
        return jumpCounter;
    }
}
