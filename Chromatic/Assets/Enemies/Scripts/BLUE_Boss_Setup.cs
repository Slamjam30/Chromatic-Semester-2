using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLUE_Boss_Setup : MonoBehaviour
{

    private GameObject mainChar;
    private GameObject mainCam;

    public GameObject blockingWall;
    public GameObject octopus;

    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == mainChar)
        {
            //have to change mainChar size
            mainChar.transform.localScale = new Vector3(0.15f, 0.15f, 1);
            blockingWall.SetActive(true);
            octopus.SetActive(true);
            mainChar.GetComponent<Health>().startPos = new Vector3(0, -2.5f, 0);
            mainCam.GetComponent<CameraFollow>().following = false;
            mainCam.transform.position = new Vector3(0, 0.270000011f, 0);
            mainCam.GetComponent<Camera>().orthographicSize = 5f;
            Globals.inFight = true;
            Destroy(gameObject);

            //The end of boss fight stuff is handled in Tentacle_Script_Main in the AllFloatUp() Method

        }
    }
}
