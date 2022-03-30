using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackpos;
    public float attackRange;
    public LayerMask whatisEnemies;
    public int damage;
    private float moveInput;
    private int lastInput;
    // Start is called before the first frame update
    void Start()
    {
        moveInput = 1;
        lastInput = 1;
    }


    //PROBLEM- IT IS PROPERLY CHANGING ATTACKPOS OBJECT VALUE FOR X, BUT IT IS NOT REGISTERING For example, the object value will read -3.5 but will not actually move to that position.

    // Update is called once per frame
    void Update()
    {
        // Takes horizontal speed of gameobject
        moveInput = Input.GetAxis("Horizontal");

        //calls flip method
        Flip(moveInput);

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.F))
            {
                {
                    //Enemies must have the Enemy LAYER for this to work. OverlapCircleAll() uses the Enemy layer as a mask
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackpos.position, attackRange, whatisEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].gameObject.GetComponent<Health>().ProcessHit(damage, gameObject);

                    }

                    timeBtwAttack = startTimeBtwAttack;
                    Debug.Log("MAIN CHAR ATTACKED");
                }
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackpos.position, attackRange);
    }


    // is called to move the attack hitbox to face direction of main char
    void Flip(float horizontalInput)
    {

        // if movement is positive, set the hitbox to the right of the character
        if (horizontalInput > 0)
        {
            attackpos.localScale = new Vector3(Mathf.Abs(-attackpos.localScale.x), 0);
            lastInput = 1;

        }

        // if movement is negative, set it to the left of the character
        else if (horizontalInput < 0)
        {
            attackpos.localScale = new Vector3(- Mathf.Abs(attackpos.localScale.x), 0);
            lastInput = 2;

        }
        // if character jumped, take last input and keep the hitbox there
        else if (Input.GetAxis("Vertical") != 0)
        {
            if (lastInput == 1)
            {
                attackpos.localScale = new Vector3(Mathf.Abs(attackpos.localScale.x), 0);
            }
            else if (lastInput == 2)
            {
                attackpos.localScale = new Vector3(- Mathf.Abs(attackpos.localScale.x), 0);
            }

        }


    }

}




