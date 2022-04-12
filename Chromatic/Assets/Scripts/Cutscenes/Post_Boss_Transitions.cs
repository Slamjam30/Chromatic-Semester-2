using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post_Boss_Transitions : MonoBehaviour
{
    private string currentScene;
    private GameObject mainChar;

    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentScene == "BLUE" && Globals.color == "YELLOW")
        {
            mainChar.GetComponent<Health>().raiseToMaxHealth();
            mainChar.GetComponent<Health>().startPos = new Vector3(258.600006f, -19.8099995f, 0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        




    }
}
