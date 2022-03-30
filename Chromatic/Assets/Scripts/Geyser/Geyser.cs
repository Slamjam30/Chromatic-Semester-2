using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    public bool horizontal = false;
    public float distance = 1;
    [Header("Extra Options:")]
    public float waitAtTop = 0.75f;
    public float movementSpeed = 2f;
    private Vector2 startPos;
    private Vector2 goalPos;
    private bool move = false;
    private bool towardsGoal = true;
    private bool started = false;

    public GameObject geyser;



    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(geyser.transform.position.x, geyser.transform.position.y);


        if (horizontal)
        {
            goalPos = new Vector2(startPos.x + distance, startPos.y);
        }
        else
        {
            goalPos = new Vector2(startPos.x, startPos.y + distance);
        }





    }

    // Update is called once per frame
    void Update()
    {
        if (move && towardsGoal)
        {
            var step = movementSpeed * Time.deltaTime;
            geyser.transform.position = Vector2.MoveTowards(geyser.transform.position, goalPos, step);
            //Step is max delta so as get closer, the MoveTowards kind of gets dialated I think

        }

        if (move && new Vector2(geyser.transform.position.x, geyser.transform.position.y) == goalPos)
        {
            StartCoroutine(WaitAtTop());
        }

        if (move && !towardsGoal)
        {
            var step = movementSpeed * Time.deltaTime;
            geyser.transform.position = Vector2.MoveTowards(geyser.transform.position, startPos, step);
            //Step is max delta so as get closer, the MoveTowards kind of gets dialated I think
        }

        if (move && !towardsGoal && new Vector2(geyser.transform.position.x, geyser.transform.position.y) == startPos)
        {
            move = false;
            towardsGoal = true;
            started = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!move && !started && collision.gameObject.tag == "Player")
        {
            StartCoroutine(GeyserStart());
        }
    }
    private IEnumerator GeyserStart()
    {
        started = true;
        //TO-DO: ANIMATION
        //TO-DO: yield return new WaitForSeconds(ANIMATION TIME);
        yield return new WaitForSeconds(1f);
        geyser.GetComponent<SpriteRenderer>().enabled = true;
        move = true;

    }

    private IEnumerator WaitAtTop()
    {
        yield return new WaitForSeconds(waitAtTop);
        //TO-DO: END OF GEYSER ANIM (Either put the anim on this object or put it on geyser and just tp it when it's done and not translate it
        towardsGoal = false;
        geyser.GetComponent<SpriteRenderer>().enabled = false;
    }

}
