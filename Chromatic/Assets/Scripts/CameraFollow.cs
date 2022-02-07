using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Both following and freezePos will be set through a different script
    public bool following = true;
    public Vector2 freezePos;

    // Start is called before the first frame update
    public Transform target;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (following == true)
        {
            transform.position = new Vector3(target.position.x, target.position.y, -10f);
        }

        if (following == false)
        {
            transform.position = new Vector3(freezePos.x, freezePos.y, -10f);
        }
        
    }
}
//test