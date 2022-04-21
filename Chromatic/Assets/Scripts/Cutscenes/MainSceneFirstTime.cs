using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneFirstTime : MonoBehaviour
{
    public GameObject barriers;
    public GameObject door;
    public GameObject greenBarrier;
    public GameObject blueBarrier;
    public GameObject blueUngrayscale;
    public GameObject yellowTilemap;
    public GameObject fallingSand;
    public GameObject yellowUngrayscale;
    public GameObject yellowBarrier;
    public GameObject fireTree;
    public GameObject redBarrier;
    public GameObject redUngrayscale;
    public GameObject stairs;


    private GameObject mainCam;
    private GameObject mainChar;
    private bool startScene;
    public bool debugColor;
    private float SIZE;
    private Vector3 POSITION;

    private bool blueDone;
    private bool yellowDone;
    private bool redDone;
    private bool doneDone;

    // Start is called before the first frame update
    void Start()
    {
        //stuff not done
        blueDone = false;
        yellowDone = false;
        redDone = false;
        doneDone = false;


        //Camera Settings
        SIZE = 21.19327f;
        POSITION = new Vector3(-6.4000001f, 15.2f, -10);

        mainChar = GameObject.FindGameObjectWithTag("Player");
        mainChar.GetComponent<Health>().raiseToMaxHealth();

        //Manually Set this for debugging
        if (debugColor)
        {
            Globals.color = "RED";
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
            greenBarrier.SetActive(true);
            mainChar.transform.position = new Vector3(31.7099991f, 12.2799997f, 0);
            mainChar.GetComponent<Health>().startPos = new Vector3(31.7099991f, 12.2799997f, 0);
        }

        //YELLOW Settings- GREEN Barrier, BLUE Barrier, Player Position, Spawnpoint, Destroy all BLUE objects and the BLUE Ungrayscale
        if (Globals.color == "YELLOW")
        {
            greenBarrier.SetActive(true);
            blueBarrier.SetActive(true);
            Destroy(blueUngrayscale);
            foreach (GameObject blueObj in GameObject.FindGameObjectsWithTag("BLUE"))
            {
                Destroy(blueObj);
            }
            mainChar.transform.position = new Vector3(29.3099995f, -2, 0);
            mainChar.GetComponent<Health>().startPos = new Vector3(29.3099995f, -2, 0);

        }

        if (Globals.color == "RED")
        {
            greenBarrier.SetActive(true);
            blueBarrier.SetActive(true);
            yellowBarrier.SetActive(true);
            Destroy(blueUngrayscale);
            Destroy(yellowUngrayscale);
            foreach (GameObject blueObj in GameObject.FindGameObjectsWithTag("BLUE"))
            {
                Destroy(blueObj);
            }
            foreach (GameObject yellowObj in GameObject.FindGameObjectsWithTag("YELLOW"))
            {
                Destroy(yellowObj);
            }
            mainChar.transform.position = mainChar.transform.position = new Vector3(-23.3600006f, -1.82000005f, 0);
            mainChar.GetComponent<Health>().startPos = new Vector3(-23.3600006f, -1.82000005f, 0);

        }

        if (Globals.color == "DONE")
        {
            greenBarrier.SetActive(true);
            blueBarrier.SetActive(true);
            yellowBarrier.SetActive(true);
            redBarrier.SetActive(true);
            Destroy(blueUngrayscale);
            Destroy(yellowUngrayscale);
            Destroy(redUngrayscale);
            Destroy(fallingSand);
            foreach (GameObject blueObj in GameObject.FindGameObjectsWithTag("BLUE"))
            {
                Destroy(blueObj);
            }
            foreach (GameObject yellowObj in GameObject.FindGameObjectsWithTag("YELLOW"))
            {
                Destroy(yellowObj);
            }
            foreach (GameObject redObj in GameObject.FindGameObjectsWithTag("RED"))
            {
                Destroy(redObj);
            }
            mainChar.transform.position = new Vector3(-38.6599998f, -1.72000003f, 0);
            mainChar.GetComponent<Health>().startPos = new Vector3(-38.6599998f, -1.72000003f, 0);
            door.GetComponent<Animator>().SetBool("DoorRed", true);

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

        if (Globals.color == "YELLOW" && !yellowDone)
        {
            StartCoroutine(YellowCutscene());
        }

        if (Globals.color == "RED" && !redDone)
        {
            StartCoroutine(RedCutscene());
        }

        if (Globals.color == "DONE" && !doneDone)
        {
            StartCoroutine(DoneCutscene());
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Destroy(mainCam.GetComponent<CameraFollow>());

        startScene = true;
    }

    public IEnumerator DoneCutscene()
    {
        doneDone = true;
        //Show Path to Door
        mainCam.transform.position = new Vector3(-19.2999992f, 3.9000001f, -10);
        mainCam.GetComponent<Camera>().orthographicSize = 9.091533f;
        stairs.GetComponent<Animator>().SetBool("MakePath", true);
        yield return new WaitForSeconds(5f);
        mainCam.transform.position = POSITION;
        mainCam.GetComponent<Camera>().orthographicSize = SIZE;
    }

    public IEnumerator RedCutscene()
    {
        redDone = true;
        //Show Red Door
        mainCam.transform.position = new Vector3(-6.0999999f, 12.5f, -10);
        mainCam.GetComponent<Camera>().orthographicSize = 6f;
        door.GetComponent<Animator>().SetBool("DoorRed", true);
        Destroy(fallingSand);
        yield return new WaitForSeconds(3f);
        mainCam.transform.position = new Vector3(-37.9000015f, 0, -10);
        //Wait until Ungrayscale is over then run falling sand anim- delete grayscale layer just in case it's still there
        yield return new WaitForSeconds(7f);
        fireTree.GetComponent<Animator>().SetBool("onFire", true);
        yield return new WaitForSeconds(2f);
        Destroy(fireTree);
        mainCam.transform.position = POSITION;
        mainCam.GetComponent<Camera>().orthographicSize = SIZE;
    }

    public IEnumerator YellowCutscene()
    {
        yellowDone = true;
        //Show Yellow Door
        mainCam.transform.position = new Vector3(-6.0999999f, 12.5f, -10);
        mainCam.GetComponent<Camera>().orthographicSize = 6f;
        door.GetComponent<Animator>().SetBool("DoorYellow", true);
        yield return new WaitForSeconds(2f);
        mainCam.transform.position = new Vector3(-29.5499992f, 0, -10);
        //Wait until Ungrayscale is over then run falling sand anim- delete grayscale layer just in case it's still there
        yield return new WaitForSeconds(8f);
        Destroy(yellowTilemap);
        fallingSand.GetComponent<Animator>().SetBool("Fall", true);
        yield return new WaitForSeconds(2f);
        Destroy(fallingSand);
        mainCam.transform.position = POSITION;
        mainCam.GetComponent<Camera>().orthographicSize = SIZE;
    }


    public IEnumerator BlueCutscene()
    {
        blueDone = true;
        //Show Blue Door
        mainCam.transform.position = new Vector3(-6.0999999f, 12.5f, -10);
        mainCam.GetComponent<Camera>().orthographicSize = 6f;
        door.GetComponent<Animator>().SetBool("DoorBlue", true);
        yield return new WaitForSeconds(2f);
        //Show Waterfall!
        mainCam.transform.position = new Vector3(26.5f, 1.61000001f, -10);
        //Destroy waterfall collider
        Destroy(GameObject.Find("waterfall_gray").GetComponent<Collider2D>());
        yield return new WaitForSeconds(1.5f);
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
