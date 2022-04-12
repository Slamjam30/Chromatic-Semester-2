using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnGrayscale : MonoBehaviour
{
// Gets all GameObjects with BLUE tag and slowly fades out the alpha once colorIn is true.
    private Color temp;
    private float SPEED;
    private GameObject[] colorObjects;
    private bool colorRan = false;
    //public GameObject blueObject

    public string color;

    // Start is called before the first frame update
    void Start()
    {
        colorObjects = GameObject.FindGameObjectsWithTag(color);

        SPEED = Globals.grayscaleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (color == Globals.color)
        {
            colorRan = false;
            foreach (GameObject colorObject in colorObjects)
            {
                if (colorObject.GetComponent<SpriteRenderer>() != null && colorObject.GetComponent<SpriteRenderer>().color.a > 0)
                {
                    colorRan = true;
                    temp = colorObject.GetComponent<SpriteRenderer>().color;
                    //Alpha is from 0 to 1. If speed is 0.04f, that means 4%/second.
                    temp.a -= SPEED * Time.deltaTime;
                    colorObject.GetComponent<SpriteRenderer>().color = temp;
                }

                if (colorObject.GetComponent<Tilemap>() != null && colorObject.GetComponent<Tilemap>().color.a > 0)
                {
                    colorRan = true;
                    temp = colorObject.GetComponent<Tilemap>().color;
                    //Alpha is from 0 to 1. If speed is 0.04f, that means 4%/second.
                    temp.a -= SPEED * Time.deltaTime;
                    colorObject.GetComponent<Tilemap>().color = temp;
                }


            }

        }

        //Once all objects are at alpha 0, destroy the gameObject so it doesn't cause lag or anything do to the constant calling of GameObjects.
        if (color == Globals.color && !colorRan)
        {
            Destroy(gameObject);
        }
    }
}
