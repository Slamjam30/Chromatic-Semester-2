using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHealth : MonoBehaviour
{

    public int numOfHearts;
    public int MAX = 7;

    /*public GameObject baseHearts;
    public GameObject greenHeart;
    public GameObject blueHeart;
    public GameObject yellowHeart;
    public GameObject redHeart;*/

    public List<GameObject> hearts;
    private GameObject mainChar;

    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.Find("MainCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        numOfHearts = mainChar.GetComponent<Health>().getHealth();
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i + 1 > numOfHearts)
            {
                hearts[i].SetActive(false);
            } else
            {
                hearts[i].SetActive(true);
            }
        }
    }
}
