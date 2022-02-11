using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    //To swim higher, press space
    //ADD COLLISION IN MOVEMENT SCRIPT TO ENABLE INFINITE JUMPS ONTRIGGERSTAY2D

    private float gravScale = 1;
    public float xForce;
    public float yForce;
    private Vector2 outofwater;
    

    // Start is called before the first frame update
    void Start()
    {
        //So that don't have to start entering decimals in the X_ and Y_ force public input
        xForce = xForce * 0.025f;
        yForce = yForce * 0.025f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == true)
        {
            //Using Translate and not AddForce, because we want a constant movement, not an acceleration
            collision.attachedRigidbody.transform.Translate(new Vector2(xForce, 0));
            collision.attachedRigidbody.AddForce(new Vector2(0, yForce), ForceMode2D.Impulse);
            collision.attachedRigidbody.gravityScale = -0.2f;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == true)
        {
            collision.attachedRigidbody.gravityScale = gravScale;
            collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, 5);
        }


    }



}
