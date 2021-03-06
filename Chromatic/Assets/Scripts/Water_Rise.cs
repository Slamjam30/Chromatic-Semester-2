using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Rise : MonoBehaviour
{

    public bool rise;
    private GameObject water;
    public Vector2 waterPos;
    public float speed = 1;

    private bool scenePlayed;

    // Start is called before the first frame update
    void Start()
    {
        water = gameObject;
        scenePlayed = false;

        //Automatically moves to the z position so it moves from invisible to visible range
    }

    // Update is called once per frame
    void Update()
    {
        if (rise == true && waterPos.y >= water.transform.position.y)
        {
            //moves towards the goal position
            var step = speed * Time.deltaTime;
            water.transform.position = Vector2.MoveTowards(new Vector2(water.transform.position.x, water.transform.position.y), waterPos, step);

        }

        if (rise == true && scenePlayed == false)
        {
            StartCoroutine(Rise());
            scenePlayed = true;
        }

    }

    IEnumerator Rise()
    {
        Debug.Log("Rise() ran,");
        GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = 37.65726f;
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().freezePos = new Vector2(453.9f, -0.3f);

        yield return new WaitForSeconds(1f);


        yield return new WaitForSeconds(3f);
        
        GameObject.Find("Boss Area").GetComponentInChildren<FreezeCam>().UnFreeze();
        Debug.Log(GameObject.Find("Boss Area").GetComponentInChildren<FreezeCam>().name);
    }
}
