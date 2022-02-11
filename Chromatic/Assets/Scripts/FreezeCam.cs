using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCam : MonoBehaviour
{
    //Check 'Undo Freeze' to make the trigger execute an undo. Triggers can only be used once. To change this, remove the done variable.
    //Apply this script to the Detection box. The player must have the Player tag for this to work
    //block and blockpos are the positions of the walls to block the player inside the block area. If there are multiple walls, just make block and blockpos off of the parent object.
    public Vector2 freezePos;
    public Vector3 blockpos;
    public GameObject block;
    public bool UndoFreeze = false;
    public float camSize;
    private float origcamSize;

    private bool done;
    private Vector3 origblockpos;

    // Start is called before the first frame update
    void Start()
    {
        origcamSize = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize;
        origblockpos = block.transform.position;
    }

    // Update is called once per frame
    void Update()
    {



    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (done == false)
        {
            //Freezes camera if UndoFreeze is false
            if (collision.gameObject.tag == "Player" && UndoFreeze == false)
            {
                Freeze();
                done = true;
            }

            //Unfreezes camera if UndoFreeze is true
            if (collision.gameObject.tag == "Player" && UndoFreeze == true)
            {

                UnFreeze();
                done = true;

            }
        }
    }

    public void Freeze()
    {
        if (block != null)
        {
            block.transform.position = blockpos;
        }

        //Changes the variables in the CameraFollow script to freeze the camera, and inserts this script's freezePos.
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().freezePos = freezePos;
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().following = false;

        if (camSize > 0)
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = camSize;
        }

    }

    public void UnFreeze()
    {
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().following = true;
        if (block != null)
        {
            Destroy(block);
        }

        GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = origcamSize;
    }



}
