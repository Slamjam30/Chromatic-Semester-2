using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    Vector2 position;
    Quaternion rotation;
    public float killLevelYPos;

    // Start is called before the first frame update
    void Start()
    {

        position = transform.position;
        rotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y <= killLevelYPos)
        {

            transform.position = position;
            transform.rotation = rotation;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        
        }



    }





}
