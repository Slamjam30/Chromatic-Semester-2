using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle_Script : MonoBehaviour
{
    Vector2 pos;
    bool weShmoovin = false;

    public string currentPhase;
    public string tentacleSide;
    public float tentacleSpeed = 10f;
    public int tentacleOrder;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitBeforeShmoovin(tentacleOrder / 2f));
    }


    // Update is called once per frame
    void Update()
    {
        // Variable for tentacle position.
        pos = gameObject.transform.position;

        if (currentPhase == "Pincer_Attack_Phase_1")
        {
            PincerAttackPhase1(tentacleSide);
        }
        else if (currentPhase == "Pincer_Attack_Phase_2")
        {
            PincerAttackPhase2(tentacleSide);
        }

        Debug.Log(weShmoovin);

        // Set position to current position.
        transform.position = pos;

    }

    void PincerAttackPhase1(string tentacleSide)
    {
        if (weShmoovin == true)
        {
            if ((pos.x <= -17.00) || (pos.x >= 17.00))
            {
                if (tentacleSide == "left")
                {
                    pos.x += tentacleSpeed * Time.deltaTime;
                }
                else if (tentacleSide == "right")
                {
                    pos.x -= tentacleSpeed * Time.deltaTime;
                }
            } else
            {
                weShmoovin = false;
                currentPhase = "Pincer_Attack_Phase_2";
            }
        }
    }

    void PincerAttackPhase2(string tentacleSide)
    {
        if (weShmoovin == true)
        {
            if ((pos.x <= -10.00) || (pos.x >= 10.00))
            {
                if (tentacleSide == "left")
                {
                    pos.x += tentacleSpeed * Time.deltaTime;
                }
                else if (tentacleSide == "right")
                {
                    pos.x -= tentacleSpeed * Time.deltaTime;
                }
            }
            else
            {
                weShmoovin = false;
                currentPhase = "Pincer_Attack_Phase_3";
            }
        }
    }


    IEnumerator waitBeforeShmoovin(float time)
    {
        while (true)
        {
            if ((weShmoovin == false))
            {
                yield return new WaitForSeconds(time);
                weShmoovin = true;
            }
            yield return null;
        }
    }

}
