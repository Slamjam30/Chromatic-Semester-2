using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingVine : MonoBehaviour
{

    Rigidbody2D rgbd2;
    Vector2 position;


    public float vineSpeed;
    public float vineLeft_Angle;
    public float vineRight_Angle;

    bool clockwise;

    void Start()
    {
        rgbd2 = GetComponent<Rigidbody2D>();
        clockwise = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Debug.Log(transform.rotation.z);
        Debug.Log(rgbd2.angularVelocity);



    }

    public void ChangeDir()
    {



        if (transform.rotation.z > vineRight_Angle)
        {
            clockwise = false;
            
        }
            
        if (transform.rotation.z < vineLeft_Angle)
        {
            clockwise = true;
            
        }

        

    }

    public void Move()
    {
        ChangeDir();

        
        if (clockwise)
        {
            rgbd2.angularVelocity = vineSpeed;
        }
        if (!clockwise)
        {
            rgbd2.angularVelocity = -1 * vineSpeed;
        }
            

    }

}
