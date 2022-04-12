using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneFirstTime : MonoBehaviour
{
    public GameObject barriers;
    public GameObject door;
    private GameObject mainCam;
    private GameObject mainChar;
    private bool startScene;
    private bool blueDone = false;
    public bool debugColor;
    private float SIZE;
    private Vector3 POSITION;

    // Start is called before the first frame update
    void Start()
    {
        //Camera Settings
        SIZE = 28.10219f;
        POSITION = new Vector3(-6.4000001f, 18.8999996f, -10);

        mainChar = GameObject.FindGameObjectWithTag("Player");

        //Manually Set this for debugging
        if (debugColor)
        {
            Globals.color = "BLUE";
        }

        mainCam = GameObject.Find("Main Camera");

        //Sets the Scene according to the color (since scene will always re-load)
        //GREEN Changes
        if (Globals.color != "WHITE")
        {
            //GREEN Changes (No CameraFollow, color temp)
            Destroy(GameObject.Find("Tutorial"));
            Destroy(GameObject.Find("GRAYSCALE OBJECT"));
            Destroy(mainCam.GetComponent<CameraFollow>());
            Color temp = door.GetComponent<SpriteRenderer>().color;
            temp.a = 1;
            door.GetComponent<SpriteRenderer>().color = temp;
        }

        //BLUE Settings- GREEN Barrier, Player Position, Spawnpoint
        if (Globals.color == "BLUE")
        {
            GameObject.Find("GREEN Barrier").SetActive(true);
            mainChar.transform.position = new Vector3(31.7099991f, 12.2799997f, 0);
            mainChar.GetComponent<Health>().startPos = new Vector3(31.7099991f, 12.2799997f, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (startScene)
        {
            mainCam.transform.position = new Vector3(-6.0999999f, 12.5f, -10);
            mainCam.GetComponent<Camera>().orthographicSize = 6f;
            barriers.SetActive(true);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            //Fades in
            if (door.GetComponent<SpriteRenderer>().color.a < 1)
            {
                float SPEED = Globals.grayscaleSpeed * 2;
                Color temp = door.GetComponent<SpriteRenderer>().color;
                temp.a += SPEED * Time.deltaTime;
                door.GetComponent<SpriteRenderer>().color = temp;
            }
            else
            { 
                startScene = false; //So doesn't run eternally
                StartCoroutine(GreenCutscene());
            }
        }

        if (Globals.color == "BLUE" && !blueDone)
        {
            StartCoroutine(BlueCutscene());
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Destroy(mainCam.GetComponent<CameraFollow>());

        startScene = true;
    }

    public IEnumerator BlueCutscene()
    {
        blueDone = true;
        //Show Blue Door
        mainCam.transform.position = new Vector3(-6.0999999f, 12.5f, -10);
        mainCam.GetComponent<Camera>().orthographicSize = 6f;
        door.GetComponent<Animator>().SetBool("DoorBlue", true);
        yield return new WaitForSeconds(1f);
        //Show Waterfall!
        mainCam.transform.position = new Vector3(26.5f, 1.61000001f, -10);
        //Destroy waterfall collider
        Destroy(GameObject.Find("waterfall_gray").GetComponent<Collider2D>());
        yield return new WaitForSeconds(1f);
        mainCam.transform.position = POSITION;
        mainCam.GetComponent<Camera>().orthographicSize = SIZE;
    }

    public IEnumerator GreenCutscene()
    {
        door.GetComponent<Animator>().SetBool("DoorVines", true);
        yield return new WaitForSeconds(2f);
        mainCam.transform.position = POSITION;
        mainCam.GetComponent<Camera>().orthographicSize = SIZE;
    }

}
