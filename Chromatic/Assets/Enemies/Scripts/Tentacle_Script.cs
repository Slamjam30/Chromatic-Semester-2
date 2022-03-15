using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Tentacle_Script : MonoBehaviour
{
    Vector2 pos;
    bool weShmoovin = false;

    private string currentPhase;
    public string tentacleSide;
    public float tentacleSpeed = 10f;
    public int tentacleOrder;

    // Determines which attack this tentacle is being used for
    public string thisTentacleAttack;
    
    // Determines which side of the screen the jab attack will start from.
    static string initialJabTentacle;

    // Indicates the start of a jab attack.
    bool weJabbin = false;

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = "Pincer_Attack_Phase_1";
        StartCoroutine(waitBeforeShmoovin(tentacleOrder / 3f));
        StartCoroutine(waitBeforeJabbin());
    }


    // Update is called once per frame
    void Update()
    {
        // Variable for tentacle position.
        pos = gameObject.transform.position;

        if ((Tentacle_Stage_Tracker.currentStage == "PincerStage") && (thisTentacleAttack == "PincerAttack"))
        {
            if (currentPhase == "Pincer_Attack_Phase_1")
            {
                PincerAttackPhase1(tentacleSide);
            }
            else if (currentPhase == "Pincer_Attack_Phase_2")
            {
                PincerAttackPhase2(tentacleSide);
            }
            else if (currentPhase == "Pincer_Attack_Phase_3")
            {
                PincerAttackPhase3(tentacleSide);
            }
        }
        else if ((Tentacle_Stage_Tracker.currentStage == "JabStage") && (thisTentacleAttack == "JabAttack"))
        {
            JabAttackPhase1();
        }

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

    // Pulls the Pincer Attack tentacles back off screen
    void PincerAttackPhase3(string tentacleSide)
    {
        if(weShmoovin == true)
        {
            if (((tentacleSide == "left") && (pos.x > -20.00)) || ((tentacleSide == "right") && (pos.x < 20.00)))
            {
                if (tentacleSide == "left")
                {
                    pos.x -= tentacleSpeed * Time.deltaTime;
                }
                else if (tentacleSide == "right")
                {
                    pos.x += tentacleSpeed * Time.deltaTime;
                }
            }
            else
            {
                weShmoovin = false;
                weJabbin = true;
                // Once all the pincer tentacles are off-screen, the stage is changed to JabStage
                if ((tentacleSide == "left") && (tentacleOrder == 3))
                {
                    Tentacle_Stage_Tracker.currentStage = "JabStage";
                }
            }
        }
    }

    // Moves the first tentacle in a Jab Attack.
    void JabAttackPhase1()
    {
        if ((initialJabTentacle == "left") && (tentacleSide == "left"))
        {
            if (pos.x <= -2.00)
            {
                pos.x += tentacleSpeed * Time.deltaTime;
                pos.y -= tentacleSpeed * Time.deltaTime;
            }
        }
        else if ((initialJabTentacle == "right") && (tentacleSide == "right"))
        {
            if (pos.x >= 2.00)
            {
                pos.x -= tentacleSpeed * Time.deltaTime;
                pos.y -= tentacleSpeed * Time.deltaTime;
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

    IEnumerator waitBeforeJabbin()
    {
        while (true)
        {
            if (weJabbin == true)
            {
                setInitialJabTentacle();
                weJabbin = false;
            }
            yield return null;
        }
    }

    // Determines if the Jab Attack starts from the left or right side of the screen.
    static void setInitialJabTentacle()
    {
        Random rd = new Random();

        // Chooses 0 or 1 randomly
        int leftOrRight = rd.Next(0, 1);

        // Uses the random number from leftOrRight to decide which side the jab attack will start on.
        if (leftOrRight == 0)
        {
            initialJabTentacle = "left";
        } else
        {
            initialJabTentacle = "right";
        }
    }
}
