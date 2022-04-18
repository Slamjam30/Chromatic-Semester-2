using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava_Move : MonoBehaviour
{
    public bool move;
    public Transform[] movePoints;
    private int i;
    public float moveSpeed;
    private float step;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        step = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
        {
            step = moveSpeed * Time.deltaTime;
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, movePoints[i].position, step);
        }

        if (Mathf.Abs(gameObject.transform.position.x - movePoints[i].position.x) < 1 && Mathf.Abs(gameObject.transform.position.y - movePoints[i].position.y) < 1)
        {
            i++;
        }

    }
}
