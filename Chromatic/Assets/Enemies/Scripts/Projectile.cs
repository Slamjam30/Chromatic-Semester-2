using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag 
        != gameObject.tag)
        {
            Destroy(gameObject);
        }
        
    }
}
