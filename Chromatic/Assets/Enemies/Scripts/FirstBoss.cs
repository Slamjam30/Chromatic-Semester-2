using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : MonoBehaviour
{
    public SpriteRenderer sr;
    private GameObject mainCharacter;
    public float moveSpeed = 5f;
    public float dToAttack = 7f;
    private Vector2 startPos;
    private bool returning = false;
    private bool attacking;

    public GameObject hammer;
    public GameObject fakeHammer;
    public ScriptableObject ColorChange;
    void Start()
    {
        startPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        mainCharacter = GameObject.FindWithTag("Player");
        //moveDistanceRight = new Vector3(moveXRight, 0, 0);
        //moveDistanceLeft = new Vector3(moveXLeft, 0, 0);
        //sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Main Camera").GetComponent<CameraFollow>().following)
        {

            if (Mathf.Abs(mainCharacter.transform.position.x - gameObject.transform.position.x) >= dToAttack && !returning && !attacking)
            {
                Debug.Log("MoveTowardsPlayer()");
                MoveTowardsPlayer();
            }
            else if (Mathf.Abs(mainCharacter.transform.position.x - gameObject.transform.position.x) <= dToAttack && !returning)
            {
                Debug.Log("Attack()");
                StartCoroutine(Attack());
            }
            else if (!attacking)
            {
                Debug.Log("ReturnToStart()");
                ReturnToStart();
            }
        }
    }

    private void ReturnToStart()
    {
        returning = true;
        var step = moveSpeed * Time.deltaTime;


        //Turns the entire boss around and turns off the hinge joint
        gameObject.transform.rotation = new Quaternion(0, 180, 0, gameObject.transform.rotation.w);
        hammer.SetActive(false);
        fakeHammer.SetActive(true);

        transform.position = Vector2.MoveTowards(transform.position, new Vector3(startPos.x, startPos.y, transform.position.z), step);

        if (Mathf.Abs(startPos.x - gameObject.transform.position.x) <= 0.0001f)
        {
            returning = false;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, gameObject.transform.rotation.w);
            fakeHammer.SetActive(false);
            hammer.SetActive(true);
        }

    }

    private void MoveTowardsPlayer()
    {
        if (transform.position.x < mainCharacter.transform.position.x)
        { sr.flipX = true; }
        else if (transform.position.x > mainCharacter.transform.position.x)
        { sr.flipX = false; }

        var step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(mainCharacter.transform.position.x, transform.position.y, transform.position.z), step);

    }

    private IEnumerator Attack()
    {
        attacking = true;     

        hammer.GetComponent<HingeJoint2D>().motor = new JointMotor2D { motorSpeed = -60f, maxMotorTorque = 10000f };

        yield return new WaitForSeconds(2f);

        hammer.GetComponent<HingeJoint2D>().motor = new JointMotor2D { motorSpeed = 60f, maxMotorTorque = 10000f };

        //Give it time to come back up before turning around
        yield return new WaitForSeconds(1f);

        attacking = false;
        returning = true;

    }
}
