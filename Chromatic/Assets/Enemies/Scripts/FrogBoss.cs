using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBoss : MonoBehaviour
{
    //Step 1: Detect when player is near
    //Step 2: Randomize movement to move a certain distance away (have a max andm in xpos so that it won't go out of bounds.)
    //Step 3: Once the movement point is determined, move in an arc/parabola to reach that point
    //Step 4: Incorporate Cooldown to this jumping ability

    //Extra Goal: Tongue attack (diff object for that).

    private bool tongueReady = false;
    private bool jumpReady = true;
    private float dFromPlayer;
    private string direction;
    private Vector2 startPos;
    private Vector3 tonguePos;
    private Vector3 tongueScale;
    private Vector3 startTonguePos;
    private Transform tongue;
    private Animator AM;
    public Sprite JUMP_SPRITE;
    public Sprite MOUTH_SPRITE;
    private Sprite ORIG_SPRITE;

    [SerializeField] GameObject TONGUE_COLLIDER_LEFT;
    [SerializeField] GameObject TONGUE_COLLIDER_RIGHT;
    [SerializeField] float jumpcooldown;
    public GameObject Tongue;


    // Start is called before the first frame update
    void Start()
    {
        tongue = gameObject.transform.GetChild(0);
        startTonguePos = new Vector3(tongue.localPosition.x, tongue.localPosition.y, tongue.localPosition.z);
        tonguePos = new Vector3(-0.703f, -0.07f, 0);
        tongueScale = new Vector3(2.079709f, 1.339473f, 0.03756505f);
        startPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        AM = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x > GameObject.Find("MainCharacter").transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else 
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }


        dFromPlayer = gameObject.transform.position.x - GameObject.Find("MainCharacter").transform.position.x;
        //Player is on left or right side of the Frog
        if (dFromPlayer > 0) { direction = "left"; }
        if (dFromPlayer < 0) { direction = "right"; }
        
        if (Mathf.Abs(dFromPlayer) <= 7 && jumpReady == true)
        {
            FrogJump();
            jumpReady = false;
        }

        if (jumpReady == false && (gameObject.transform.position.y <= startPos.y + 1) && tongueReady == true)
        {

            StartCoroutine(TongueAttack());
            
        }

        //print("TongueReady:" + TongueReady + " jumpReady:" + jumpReady);
        //print(gameObject.transform.position.y <= startPos.y + 1);
    }

    private void FrogJump()
    {
        float height = Random.Range(15, 20);
        float dist = Random.Range(5, 15);

        if (direction == "left")
        {

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-dist, height);

        }

        if (direction == "right")
        {

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(dist, height);

        }

        StartCoroutine(JumpCooldown());


    }

    IEnumerator TongueAttack()
    {
        tongueReady = false;

        

        yield return new WaitForSeconds(0.5f);

        AM.enabled = false;
        //STOP THE CURRENT ANIMATION SO GOES BACK TO EMPTY SO THE SPRITE CHANGE WILL WORK

        //FOR QUATERNION, JUST COPIED FROM PROPERTIES TAB IN UNITY

        if (direction == "left")
        {
            tongue.GetComponent<SpriteRenderer>().enabled = true;
            TONGUE_COLLIDER_RIGHT.SetActive(true);
            tongue.localPosition = new Vector3(tonguePos.x, tonguePos.y, tonguePos.z);
            tongue.localScale = tongueScale;
            tongue.localRotation = new Quaternion(-0.0315254107f, 0.000994864036f, 0.0315258615f, 0.999005139f);
            tongue.GetComponent<SpriteRenderer>().flipX = true;
            gameObject.GetComponent < SpriteRenderer >().sprite = MOUTH_SPRITE;
            Debug.Log("Mouth sprite GO");
        }

        if (direction == "right")
        {
            tongue.GetComponent<SpriteRenderer>().enabled = true;
            TONGUE_COLLIDER_LEFT.SetActive(true);
            tongue.localPosition = new Vector3(-tonguePos.x, tonguePos.y, tonguePos.z);
            tongue.localScale = tongueScale;
            tongue.localRotation = new Quaternion(-0.0315261632f, -0.000994878705f, -0.0315258615f, 0.999005079f);
            tongue.GetComponent<SpriteRenderer>().flipX = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = MOUTH_SPRITE;
        }


        yield return new WaitForSeconds(0.3f);

        TONGUE_COLLIDER_LEFT.SetActive(false);
        TONGUE_COLLIDER_RIGHT.SetActive(false);
        tongue.localPosition = startTonguePos;
        tongue.localScale = new Vector3(0.1f, tongue.localScale.y, tongue.localScale.z);
        tongue.GetComponent<SpriteRenderer>().enabled = false;
        AM.enabled = true;
        //gameObject.GetComponent<SpriteRenderer>().sprite = ORIG_SPRITE;

        jumpReady = true;

    }


    IEnumerator JumpCooldown()
    {
        if (gameObject.transform.position.y <= startPos.y + 1)
        {
            AM.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = JUMP_SPRITE;
            yield return new WaitForSeconds(1);
            AM.enabled = true;
            tongueReady = true;
            //yield return new WaitForSeconds(jumpcooldown);
            //jumpReady = true;
        }


    }


}
