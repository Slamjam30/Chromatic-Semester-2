using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]  Rigidbody2D rb;
    [SerializeField] float dashTime;
    public float dashDistance = 15f;
    public bool isDashing;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift) && isDashing == false)
        {
            StartCoroutine(DoDash(-1f));
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift) && isDashing == false)
        {
            StartCoroutine(DoDash(1f));
        }
    }

    IEnumerator DoDash(float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.gravityScale = gravity;
        Debug.Log("yo im totally dashing frfr.");
    }
}
