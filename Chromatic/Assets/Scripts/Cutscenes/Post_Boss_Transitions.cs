using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post_Boss_Transitions : MonoBehaviour
{
    public GameObject changeSceneObject;
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
            mainChar.transform.position = new Vector3(258.600006f, -19.8099995f, 0);
            //Add an UnGrayscale Object to the level to color in while walking back
            changeSceneObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        




    }
}
