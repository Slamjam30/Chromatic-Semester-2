using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnGrayscale : MonoBehaviour
{

    public GameObject grayscaleSprite;
    private Color temp;
    private float SPEED;
    public bool colorIn;

    // Start is called before the first frame update
    void Start()
    {
        SPEED = Globals.grayscaleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (grayscaleSprite.GetComponent<SpriteRenderer>().color.a > 0 && colorIn)
        {
            temp = grayscaleSprite.GetComponent<SpriteRenderer>().color;
            temp.a -= SPEED;
            grayscaleSprite.GetComponent<SpriteRenderer>().color = temp;
        }
    }
}
