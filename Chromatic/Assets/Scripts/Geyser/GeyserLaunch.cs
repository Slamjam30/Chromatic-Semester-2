using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserLaunch : MonoBehaviour
{
    private bool inGeyser;
    private Rigidbody2D mainCharRB;

    private void Start()
    {
        inGeyser = false;
    }

    private void FixedUpdate()
    {
        if (inGeyser)
        {
            Debug.Log("Adding Force");
            mainCharRB.AddForce(new Vector2(0f, mainCharRB.mass * 30f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mainCharRB = collision.attachedRigidbody;
            inGeyser = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inGeyser = false;
    }

}
