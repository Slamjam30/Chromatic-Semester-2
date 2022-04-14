using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]  Rigidbody2D rb;
    [SerializeField] float dashTime;
    public float dashDistance = 15f;
    public bool isDashing;
    public float cooldownTime = 5f;
    private bool cooled;
    private bool startedCool;

// Start is called before the first frame update
void Start()
    {
        cooled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooled)
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
            {
                StartCoroutine(DoDash(-1f));
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
            {
                StartCoroutine(DoDash(1f));
            }
        }
        else if (!startedCool)
        {
            StartCoroutine(DashCooldown());
        }

    }

    IEnumerator DoDash(float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(direction * dashDistance, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.gravityScale = gravity;
        cooled = false;
        //Debug.Log("yo im totally dashing frfr.");
    }

    IEnumerator DashCooldown()
    {
        startedCool = true;
        yield return new WaitForSeconds(cooldownTime);
        cooled = true;
        startedCool = false;
    }

    public bool GetIfCooled()
    {
        return cooled;
    }

}
