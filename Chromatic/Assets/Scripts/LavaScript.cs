using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    [SerializeField] float bounceHeight;
    private Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D other)
    {
        rb = other.gameObject.GetComponent<Rigidbody2D>();
        Vector3 newVelocity = new Vector2(rb.velocity.x , bounceHeight);
        rb.velocity = newVelocity;
    }
}
