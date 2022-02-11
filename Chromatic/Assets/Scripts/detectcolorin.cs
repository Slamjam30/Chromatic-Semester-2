using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectcolorin : MonoBehaviour
{
    private bool moved = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponentInParent<colorin>().moved = moved;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            moved = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            moved = false;
        }
    }

}
