using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle_Script_Main : MonoBehaviour
{
    Vector2 pos;

    //public string this.gameObject.name;
    private float pincerSpeed = 12f;
    private float pincerRetreatSpeed = 20.0f;
    private float jabSpeed = 25.0f;

    float rotation;

    public Transform player;
    public Transform centerStage;
    public Transform octoHead;
    public Transform rotateAroundTarget;

    float playerXPos = 0.0f;

    float startXPos;
    float startYPos;

    //string swipeTentacleSide = "left";

    //For Health script to call for cooling down
    public bool octoCool;

    // Start is called before the first frame update
    void Start()
    {
        // Set tentacle positions
        // Pincer Tentacles
        if (this.gameObject.name == "PincerAttack_BottomLeft")
        {
            startXPos = -35.0f;
            startYPos = -3.5f;
            transform.position = new Vector2(-25.0f, -3.5f);
            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
        else if (this.gameObject.name == "PincerAttack_MiddleLeft")
        {
            startXPos = -40.0f;
            startYPos = -2.0f;
            transform.position = new Vector2(-30.0f, -2.0f);
            transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        }
        else if (this.gameObject.name == "PincerAttack_TopLeft")
        {
            startXPos = -45.0f;
            startYPos = -0.5f;

            transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        }
        else if (this.gameObject.name == "PincerAttack_BottomRight")
        {
            startXPos = 35.0f;
            startYPos = -3.5f;

            rotation = 180.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        }
        else if (this.gameObject.name == "PincerAttack_MiddleRight")
        {
            startXPos = 40.0f;
            startYPos = -2.0f;

            rotation = 180.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        }
        else if (this.gameObject.name == "PincerAttack_TopRight")
        {
            startXPos = 45.0f;
            startYPos = -0.5f;

            rotation = 180.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        }
        // Jab Tentacles
        else if (this.gameObject.name == "JabAttack_LeftInitial")
        {
            startXPos = -35.0f;
            startYPos = 10.0f;
            transform.position = new Vector2(startXPos, startYPos);
            var direction = centerStage.transform.position - transform.position;
            rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

        }
        else if (this.gameObject.name == "JabAttack_RightInitial")
        {
            startXPos = 30.0f;
            startYPos = 10.0f;
            transform.position = new Vector2(startXPos, startYPos);
            var direction = centerStage.transform.position - transform.position;
            rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        }
        else if (this.gameObject.name == "JabAttack_RightFollow")
        {
            startXPos = 22.0f;
            startYPos = 7.0f;

            var direction = player.transform.position - transform.position;
            rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        }
        else if (this.gameObject.name == "JabAttack_LeftFollow")
        {
            startXPos = -22.0f;
            startYPos = 7.0f;

            var direction = player.transform.position - transform.position;
            rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        }
        // Swipe Tentacles
        else if (this.gameObject.name == "SwipeAttack_Left_1")
        {
            rotation = -90.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            startXPos = rotateAroundTarget.transform.position.x + (Mathf.Cos((rotation * Mathf.PI) / 180) * 15.0f);
            startYPos = rotateAroundTarget.transform.position.y + (Mathf.Sin((rotation * Mathf.PI) / 180) * 15.0f);
        }
        else if (this.gameObject.name == "SwipeAttack_Left_2")
        {
            rotation = -110.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            startXPos = rotateAroundTarget.transform.position.x + (Mathf.Cos((rotation * Mathf.PI) / 180) * 15.0f);
            startYPos = rotateAroundTarget.transform.position.y + (Mathf.Sin((rotation * Mathf.PI) / 180) * 15.0f);
        }
        else if (this.gameObject.name == "SwipeAttack_Left_3")
        {
            rotation = -130.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            startXPos = rotateAroundTarget.transform.position.x + (Mathf.Cos((rotation * Mathf.PI) / 180) * 15.0f);
            startYPos = rotateAroundTarget.transform.position.y + (Mathf.Sin((rotation * Mathf.PI) / 180) * 15.0f);
        }
        else if (this.gameObject.name == "SwipeAttack_Left_4")
        {
            rotation = -150.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            startXPos = rotateAroundTarget.transform.position.x + (Mathf.Cos((rotation * Mathf.PI) / 180) * 15.0f);
            startYPos = rotateAroundTarget.transform.position.y + (Mathf.Sin((rotation * Mathf.PI) / 180) * 15.0f);
        }
        else if (this.gameObject.name == "SwipeAttack_Left_5")
        {
            rotation = -170.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            startXPos = rotateAroundTarget.transform.position.x + (Mathf.Cos((rotation * Mathf.PI) / 180) * 15.0f);
            startYPos = rotateAroundTarget.transform.position.y + (Mathf.Sin((rotation * Mathf.PI) / 180) * 15.0f);
        }
        else if (this.gameObject.name == "SwipeAttack_Right_1")
        {
            rotation = 90.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            startXPos = rotateAroundTarget.transform.position.x + (Mathf.Cos((rotation * Mathf.PI) / 180) * 15.0f);
            startYPos = rotateAroundTarget.transform.position.y + (Mathf.Sin((rotation * Mathf.PI) / 180) * 15.0f);
        }
        else if (this.gameObject.name == "SwipeAttack_Right_2")
        {
            rotation = 110.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            startXPos = rotateAroundTarget.transform.position.x + (Mathf.Cos((rotation * Mathf.PI) / 180) * 15.0f);
            startYPos = rotateAroundTarget.transform.position.y + (Mathf.Sin((rotation * Mathf.PI) / 180) * 15.0f);
        }
        else if (this.gameObject.name == "SwipeAttack_Right_3")
        {
            rotation = 130.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            startXPos = rotateAroundTarget.transform.position.x + (Mathf.Cos((rotation * Mathf.PI) / 180) * 15.0f);
            startYPos = rotateAroundTarget.transform.position.y + (Mathf.Sin((rotation * Mathf.PI) / 180) * 15.0f);
        }
        else if (this.gameObject.name == "SwipeAttack_Right_4")
        {
            rotation = 150.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            startXPos = rotateAroundTarget.transform.position.x + (Mathf.Cos((rotation * Mathf.PI) / 180) * 15.0f);
            startYPos = rotateAroundTarget.transform.position.y + (Mathf.Sin((rotation * Mathf.PI) / 180) * 15.0f);
        }
        else if (this.gameObject.name == "SwipeAttack_Right_5")
        {
            rotation = 170.0f;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            startXPos = rotateAroundTarget.transform.position.x + (Mathf.Cos((rotation * Mathf.PI) / 180) * 15.0f);
            startYPos = rotateAroundTarget.transform.position.y + (Mathf.Sin((rotation * Mathf.PI) / 180) * 15.0f);
        }
        // Octo Head
        else if (this.gameObject.name == "Octo_Head")
        {
            startXPos = 0.0f;
            startYPos = 3.2f;
        }


        transform.position = new Vector2(startXPos, startYPos);
    }

    // Update is called once per frame
    void Update()
    {
        pos = gameObject.transform.position;

        PincerAttack();
        PincerRetreat();
        JabInitialAttack();
        JabFollowAttack();
        JabAttackRetreat();
        checkOctoHealth();
        AllFloatUp();

        // Set position to current position.
        transform.position = pos;
    }

    void checkOctoHealth()
    {
        if (octoHead.GetComponent<Health>().octoHeadHealth < 4 && octoHead.GetComponent<Health>().octoHeadHealth > 0)
        {
            Tentacle_Stage_Tracker.startSwingAttack = true;
        } else if (octoHead.GetComponent<Health>().octoHeadHealth <= 0)
        {
            Tentacle_Stage_Tracker.currentStage = "Dead_In_The_Water";
        }
    }

    void PincerAttack()
    {
        if (Tentacle_Stage_Tracker.currentStage == "PincerStage_Attack")
        {
            if (this.gameObject.name == "PincerAttack_BottomLeft" || this.gameObject.name == "PincerAttack_MiddleLeft"
                || this.gameObject.name == "PincerAttack_TopLeft")
            {
                if (transform.position.x <= -9.0)
                {
                    pos.x += pincerSpeed * Time.deltaTime;
                }
            }
            else if (this.gameObject.name == "PincerAttack_BottomRight" || this.gameObject.name == "PincerAttack_MiddleRight"
              || this.gameObject.name == "PincerAttack_TopRight")
            {
                if (transform.position.x >= 9.0)
                {
                    pos.x -= pincerSpeed * Time.deltaTime;
                }
                else
                {
                    if (this.gameObject.name == "PincerAttack_TopRight")
                    {
                        Tentacle_Stage_Tracker.currentStage = "PincerStage_Retreat";
                    }
                }
            }
        }
    }
    
    void PincerRetreat()
    {
        if (Tentacle_Stage_Tracker.currentStage == "PincerStage_Retreat")
        {
            if (this.gameObject.name == "PincerAttack_BottomLeft" || this.gameObject.name == "PincerAttack_MiddleLeft"
                || this.gameObject.name == "PincerAttack_TopLeft")
            {
                if (transform.position.x > -20.0)
                {
                    pos.x -= pincerRetreatSpeed * Time.deltaTime;
                }
            }
            else if (this.gameObject.name == "PincerAttack_BottomRight" || this.gameObject.name == "PincerAttack_MiddleRight"
              || this.gameObject.name == "PincerAttack_TopRight")
            {
                if (transform.position.x < 20.0)
                {
                    pos.x += pincerRetreatSpeed * Time.deltaTime;
                }
                else
                {
                    if (this.gameObject.name == "PincerAttack_TopRight")
                    {
                        Tentacle_Stage_Tracker.currentStage = "JabStage_InitialJab";
                    }
                }
            }
        }
    }
    
    void JabInitialAttack()
    {
        if (Tentacle_Stage_Tracker.currentStage == "JabStage_InitialJab")
        {
            if ((this.gameObject.name == "JabAttack_LeftInitial") && (Tentacle_Stage_Tracker.initialJabTentacle == "left"))
            {
                if (transform.position.x <= 0.0)
                {
                    pos.x += jabSpeed * Time.deltaTime * Mathf.Cos((rotation * Mathf.PI) / 180);
                    pos.y += jabSpeed * Time.deltaTime * Mathf.Sin((rotation * Mathf.PI) / 180);
                }
                else
                {
                    Tentacle_Rotate_Towards_Script.rotateTowardsPlayer = true;
                    Tentacle_Stage_Tracker.currentStage = "JabStage_FollowJab";
                }
            }
            else if ((this.gameObject.name == "JabAttack_RightInitial") && (Tentacle_Stage_Tracker.initialJabTentacle == "right"))
            {
                if (transform.position.x >= 0.0)
                {
                    pos.x += jabSpeed * Time.deltaTime * Mathf.Cos((rotation * Mathf.PI) / 180);
                    pos.y += jabSpeed * Time.deltaTime * Mathf.Sin((rotation * Mathf.PI) / 180);
                }
                else
                {
                    Tentacle_Rotate_Towards_Script.rotateTowardsPlayer = true;
                    Tentacle_Stage_Tracker.currentStage = "JabStage_FollowJab";
                }
            }
        }
    }
    
    void JabFollowAttack()
    {
        if (Tentacle_Stage_Tracker.currentStage == "JabStage_FollowJab")
        {
            if ((this.gameObject.name == "JabAttack_RightFollow") && (Tentacle_Stage_Tracker.initialJabTentacle == "left"))
            {
                facePlayer();
                if (transform.position.x >= playerXPos)
                {
                    pos.x += jabSpeed * Time.deltaTime * Mathf.Cos((rotation * Mathf.PI) / 180);
                    pos.y += jabSpeed * Time.deltaTime * Mathf.Sin((rotation * Mathf.PI) / 180);
                }
                else
                {
                    Tentacle_Stage_Tracker.currentStage = "JabStage_Retreat";
                }
            }
            else if ((this.gameObject.name == "JabAttack_LeftFollow") && (Tentacle_Stage_Tracker.initialJabTentacle == "right"))
            {
                facePlayer();
                if (transform.position.x <= playerXPos)
                {
                    pos.x += jabSpeed * Time.deltaTime * Mathf.Cos((rotation * Mathf.PI) / 180);
                    pos.y += jabSpeed * Time.deltaTime * Mathf.Sin((rotation * Mathf.PI) / 180);
                }
                else
                {
                    Tentacle_Stage_Tracker.currentStage = "JabStage_Retreat";
                }
            }
        }
    }

    void facePlayer()
    {
        if (Tentacle_Rotate_Towards_Script.rotateTowardsPlayer == true)
        {
            if (this.gameObject.name == "JabAttack_RightFollow")
            {
                playerXPos = player.transform.position.x + 8.0f;
            } 
            else if (this.gameObject.name == "JabAttack_LeftFollow")
            {
                playerXPos = player.transform.position.x - 8.0f;
            }

            if (this.gameObject.name == "JabAttack_RightFollow" || this.gameObject.name == "JabAttack_LeftFollow")
            {
                var direction = player.transform.position - transform.position;
                rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

                Tentacle_Rotate_Towards_Script.rotateTowardsPlayer = false;
            } 
        }
    }

    void JabAttackRetreat()
    {
        if (Tentacle_Stage_Tracker.currentStage == "JabStage_Retreat")
        {
            if (Tentacle_Stage_Tracker.initialJabTentacle == "left")
            {
                if (this.gameObject.name == "JabAttack_LeftInitial")
                {
                    if (transform.position.x > startXPos)
                    {
                        pos.x -= jabSpeed * Time.deltaTime * Mathf.Cos((rotation * Mathf.PI) / 180);
                        pos.y -= jabSpeed * Time.deltaTime * Mathf.Sin((rotation * Mathf.PI) / 180);
                    }
                    else
                    {
                        if (Tentacle_Stage_Tracker.startSwingAttack == true)
                        {
                            Tentacle_Stage_Tracker.currentStage = "SwipeStage";
                        }
                        else
                        {
                            Tentacle_Stage_Tracker.initialJabTentacle = "right";
                            Tentacle_Stage_Tracker.currentStage = "JabStage_InitialJab";
                        }
                    }
                }
                else if (this.gameObject.name == "JabAttack_RightFollow")
                {
                    if (transform.position.x < startXPos)
                    {
                        pos.x -= jabSpeed * Time.deltaTime * Mathf.Cos((rotation * Mathf.PI) / 180);
                        pos.y -= jabSpeed * Time.deltaTime * Mathf.Sin((rotation * Mathf.PI) / 180);
                    }
                }
            }
            else if (Tentacle_Stage_Tracker.initialJabTentacle == "right")
            {
                if (this.gameObject.name == "JabAttack_RightInitial")
                {
                    if (transform.position.x < startXPos)
                    {
                        pos.x -= jabSpeed * Time.deltaTime * Mathf.Cos((rotation * Mathf.PI) / 180);
                        pos.y -= jabSpeed * Time.deltaTime * Mathf.Sin((rotation * Mathf.PI) / 180);
                    }
                    else
                    {
                        if (Tentacle_Stage_Tracker.startSwingAttack == true)
                        {
                            Tentacle_Stage_Tracker.currentStage = "SwipeStage";
                        }
                        else
                        {
                            Tentacle_Stage_Tracker.initialJabTentacle = "left";
                            Tentacle_Stage_Tracker.currentStage = "JabStage_InitialJab";
                        }
                    }
                }
                else if (this.gameObject.name == "JabAttack_LeftFollow")
                {
                    if (transform.position.x > startXPos)
                    {
                        pos.x -= jabSpeed * Time.deltaTime * Mathf.Cos((rotation * Mathf.PI) / 180);
                        pos.y -= jabSpeed * Time.deltaTime * Mathf.Sin((rotation * Mathf.PI) / 180);
                    }
                }

            }
        }
    }

    void AllFloatUp()
    {
        if (Tentacle_Stage_Tracker.currentStage == "Dead_In_The_Water")
        {
            if (transform.position.y < 50.0f)
            {
                pos.y += 5.0f * Time.deltaTime;
            }
            //Weird if camera follows again so not doing it
            //GameObject.FindGameObjectWithTag("Player").transform.localScale = new Vector3(0.418522537f, 0.380475014f, 3.14744139f);
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().following = true;
            Globals.inFight = false;
            Globals.color = "YELLOW";
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().raiseToMaxHealth();
        }
    }

    public IEnumerator OctoHealthCooldown()
    {
        octoCool = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        octoCool = false;
    }
    
    
}
