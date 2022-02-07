using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private int direction;
    public float dashSpeed;
    private float dashTime;
    public float initialDashTime;
    public float setDashCooldown;
    private float dashCooldown;
    private bool dashTF;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        dashTime = initialDashTime;
        dashCooldown = setDashCooldown;
        dashTF = false;
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        DashCD();
    }
    // Basic dash script
    void Dash()
    {
        if (direction == 0)
        {
            // Determines which directional key is being pressed with left shift
            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift) && dashTF == false)
            {
                direction = 1;
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift) && dashTF == false)
            {
                direction = 2;
            }
        } else
        {
            // If we are done dashing, set the velocity back to 0, sets dashTime back to its initial value, and starts the dash cooldown
            if (dashTime <= 0)
            {
                direction = 0;
                rb.velocity = Vector2.zero;
                dashTime = initialDashTime;
                dashTF = true;
            } else // If we are dashing, adds a short burst of speed until the set dashTime is over
            {
                dashTime -= Time.deltaTime;
                if (direction == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                } else if (direction == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
        }
    }
    // Makes sure that dash cannot be spammed
    void DashCD()
    {
        // Sets off once that the dash is over
        if (dashTF == true)
        {
            if (dashCooldown <= 0)
            {
                dashCooldown = setDashCooldown;
                dashTF = false;
            }
            else
            {
                dashCooldown -= Time.deltaTime;
            }
        }
        
    }
}
