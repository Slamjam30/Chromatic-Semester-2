using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    private bool color = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "turnToRed")
        {
            color = GameObject.Find("Circle").GetComponent<Movement>().hasRed;
            if (color == true)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
        }
        if (gameObject.tag == "turnToBlue")
        {
            color = GameObject.Find("Circle").GetComponent<Movement>().hasBlue;
            if (color == true)
            {
                GetComponent<Renderer>().material.color = Color.blue;
            }
        }
        if (gameObject.tag == "turnToGreen")
        {
            color = GameObject.Find("Circle").GetComponent<Movement>().hasGreen;
            if (color == true)
            {
                GetComponent<Renderer>().material.color = Color.green;
            }
        }
        if (gameObject.tag == "turnToYellow")
        {
            color = GameObject.Find("Circle").GetComponent<Movement>().hasYellow;
            if (color == true)
            {
                GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }
}
