using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Boss_Start : MonoBehaviour
{
    private GameObject mainCam;
    public GameObject Dragon;
    public GameObject Barrier;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Globals.inFight = true;
            Dragon.SetActive(true);
            Barrier.SetActive(true);
            mainCam.GetComponent<CameraFollow>().following = false;
            mainCam.GetComponent<Camera>().orthographicSize = 4.557293f;
            mainCam.GetComponent<CameraFollow>().freezePos = new Vector2(0, 0.779999971f);

            Destroy(gameObject);
        }
    }
}
