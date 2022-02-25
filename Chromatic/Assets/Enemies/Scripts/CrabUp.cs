using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabUp : MonoBehaviour
{
    public bool horizontal = false;
    public float distance = 1;
    [Header("Extra Options:")]
    public float maxCooldown = 2f;
    public float minCooldown = 0.5f;
    public float waitAtTop = 0.75f;
    public float movementSpeed = 2f;
    private Vector2 startPos;
    private Vector2 goalPos;
    private bool move = false;
    private bool towardsGoal = true;
    private bool unCooled = true;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);


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
        if (!move && unCooled)
        {
            StartCoroutine(Cooldown());
        }

        if (move && towardsGoal)
        {
            var step = movementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, goalPos, step);
            //Step is max delta so as get closer, the MoveTowards kind of gets dialated I think

        }

        if (move && new Vector2(transform.position.x, transform.position.y) == goalPos)
        {
            StartCoroutine(WaitAtTop());
        }

        if (move && !towardsGoal)
        {
            var step = movementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, startPos, step);
            //Step is max delta so as get closer, the MoveTowards kind of gets dialated I think
        }

        if (move && !towardsGoal && new Vector2(transform.position.x, transform.position.y) == startPos)
        {
            move = false;
            towardsGoal = true;
            unCooled = true;
        }

    }

    private IEnumerator Cooldown()
    {
        move = false;
        unCooled = false;
        yield return new WaitForSeconds(Random.Range(minCooldown, maxCooldown));
        move = true;

    }

    private IEnumerator WaitAtTop()
    {
        yield return new WaitForSeconds(waitAtTop);
        towardsGoal = false;
    }


}
