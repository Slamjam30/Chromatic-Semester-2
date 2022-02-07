using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatesChangeScene : MonoBehaviour
{
    public float x_trigger;
    public float y_trigger;
    public string SceneName;
    string CurrentScene;

    //tGreaterFLesserr = If True, will test for x >= x_trigger
    //Tabovefbelow = If True, will test for y >= y_trigger
    public bool tGreaterFLesserr;
    public bool tAboveFBelow;
    bool ParamX;
    bool ParamY;


    // Start is called before the first frame update
    void Start()
    {

        CurrentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        ParamX = false;
        ParamY = false;
        




    }

    // Update is called once per frame
    void Update()
    {


        Detection();

        //Loads the Scene if both X and Y Parameters are met.
        if ((ParamX == true) & (ParamY == true))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        }



    }

    public void Detection()
    {
        //Uses the inputs for tGreaterFLesserr and tAboveFBelow to detect whether the X and Y Parameters are true

        if (transform.position.x >= x_trigger & tGreaterFLesserr == true)
        {
            ParamX = true;
        }

        if (transform.position.x <= x_trigger & tGreaterFLesserr == false)
        {
            ParamX = true;
        }

        if (transform.position.y >= y_trigger & tAboveFBelow == true)
        {
            ParamY = true;
        }

        if (transform.position.y <= y_trigger & tAboveFBelow == false)
        {
            ParamY = true;
        }


        if (transform.position.x < x_trigger & tGreaterFLesserr == true)
        {
            ParamX = false;
        }

        if (transform.position.x > x_trigger & tGreaterFLesserr == false)
        {
            ParamX = false;
        }

        if (transform.position.y < y_trigger & tAboveFBelow == true)
        {
            ParamY = false;
        }

        if (transform.position.y > y_trigger & tAboveFBelow == false)
        {
            ParamY = false;
        }

    }





}
