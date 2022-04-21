using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_With_Controls : MonoBehaviour
{
    public string keyboardInput;
    public string controllerInput;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        if (Globals.controls == "Keyboard")
        {
            text.text = text.text.Replace("<input>", keyboardInput);
        }
        else
        {
            text.text = text.text.Replace("<input>", controllerInput);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
