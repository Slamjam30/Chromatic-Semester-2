using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    public AudioSource playerAttack;
    public AudioSource playerDamaged;
    public AudioSource playerDeath;
    public AudioSource playerBubble;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerAttack()
    {
        playerAttack.Play();
    }

    public void PlayerDamaged()
    {
        playerDamaged.Play();
    }

    public void PlayerDeath()
    {
        playerDeath.Play();
    }

    public void PlayerBubble()
    {
        playerBubble.Play();
    }

}
