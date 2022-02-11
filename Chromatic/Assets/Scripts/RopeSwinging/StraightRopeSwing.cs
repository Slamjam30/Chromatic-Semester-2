using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRopeSwing : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 pos;
    private Vector3 logpos;
    private Vector3[] points;

    public Rigidbody2D log;


    // Start is called before the first frame update
    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        CalculateLine();

    }

    private void CalculateLine()
    {
        pos = gameObject.GetComponent<Transform>().position;
        logpos = log.GetComponent<Transform>().position;

        points = new Vector3[2];
        points[0] = pos;
        points[1] = logpos;

        lineRenderer.SetPositions(points);
    }


}
