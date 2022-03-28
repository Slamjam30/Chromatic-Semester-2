using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneFirstTime : MonoBehaviour
{
    public GameObject barriers;
    //public GameObject door;
    private GameObject mainCam;
    private bool startScene;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (startScene)
        {
            barriers.SetActive(true);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            //StartCoroutine(Door());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        mainCam.transform.position = new Vector3(0.100000001f, 15.1000004f, -10);
        mainCam.GetComponent<Camera>().orthographicSize = 20.01171f;

        Destroy(mainCam.GetComponent<CameraFollow>());

        startScene = true;
    }

    /*IEnumerator Door()
    {
        Debug.Log("Nothing to see here.");
    } */

}
