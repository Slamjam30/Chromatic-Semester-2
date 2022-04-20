using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //bubb is the bubble sprite- a child object of the main character
    public GameObject bubb;
    private Rigidbody2D rigBody;
    [SerializeField] float acceleration = 3.5f;
    [SerializeField] float cooldown = 3f;
    public bool triggered;
    private bool cooled = true;
    private Animator am;

    // Start is called before the first frame update
    void Start()
    {
        rigBody = gameObject.GetComponent<Rigidbody2D>();
        bubb.SetActive(false); //hides object
        am = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooled && Input.GetKey(KeyCode.T))
        { triggered = true; }
        else { triggered = false; }

        if (triggered == true)
        {
            bubb.SetActive(true);
            am.SetBool("isBubble", true);
            if (gameObject.GetComponent<Movement>().swimming == true)
            {
                //rigBody.velocity = new Vector2(rigBody.velocity.x, yVelocity);
                if (rigBody.velocity.y < 0)
                {
                    rigBody.velocity = new Vector2(rigBody.velocity.x, acceleration / 2);
                }
                rigBody.AddForce(new Vector2(0, rigBody.mass * acceleration));
            }
        }
        else
        {
            am.SetBool("isBubble", false);
            bubb.SetActive(false);
        }
    }

    public IEnumerator Cooldown()
    {
        cooled = false;
        yield return new WaitForSeconds(cooldown);
        cooled = true;
    }

    public bool getActive()
    {
        return triggered;
    }

    public bool GetIfCooled()
    {
        return cooled;
    }

}
