using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    //NOTE: Better to use only ONE WaterPhysics object, or else it may put bool swimming as false when you leave one and enter the other

    private float startGravScale;
    [SerializeField] float gravScale = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        startGravScale = GameObject.Find("MainCharacter").GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == true && collision.GetComponent<Fish>() == null)
        {
            collision.attachedRigidbody.gravityScale = gravScale;
            if (collision.GetComponent<Movement>() != null)
            {
                collision.GetComponent<Movement>().swimming = true;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == true && collision.GetComponent<Fish>() == null)
        {
            collision.attachedRigidbody.gravityScale = startGravScale;
            if (collision.GetComponent<Movement>() != null)
            {
                collision.GetComponent<Movement>().swimming = false;
            }
        }


    }

}
