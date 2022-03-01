using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightArea : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> spawnpoints;

    private bool done;

    private void Start()
    {
        done = false;
    }

    private void Update()
    {

        if (done == true && GameObject.Find("MainCharacter").GetComponent<Movement>().KillsForUnfreeze == enemies.Count)
        {
            gameObject.GetComponent<FreezeCam>().UnFreeze();
        }

        /*if (done && finished == false)
        {
            //Runs the unfreezing script from the FreezeCam (THIS SCRIPT MUST BE Attached to the same object as the FreezeCam script for this to work!)
            gameObject.GetComponent<FreezeCam>().UnFreeze();
            finished = true;
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (done != true)
        {
            //Goes through the enemies list and instantiates the prefab at the corresponding spawnpoint in the spawnpoints list
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject element = enemies[i];
                //To protect from errors \/
                Instantiate(element, spawnpoints[i].transform);
                
            }
            GameObject.Find("MainCharacter").GetComponent<Movement>().KillsForUnfreeze = 0;
            done = true;
        }
    }


}
