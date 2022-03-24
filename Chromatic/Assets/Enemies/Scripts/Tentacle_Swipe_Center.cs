using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle_Swipe_Center : MonoBehaviour
{
    float rotationSpeed = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == "Left_Swipe_Center")
        {
            transform.position = new Vector2(-17.0f, 11.0f);
        }
        else if (this.gameObject.name == "Right_Swipe_Center")
        {
            transform.position = new Vector2(17.0f, 11.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Tentacle_Stage_Tracker.currentStage == "SwipeStage")
        {
            if (this.gameObject.name == "Left_Swipe_Center")
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            } 
            else if (this.gameObject.name == "Right_Swipe_Center")
            {
                transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            }
        }
    }
}
