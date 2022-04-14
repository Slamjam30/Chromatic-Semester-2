using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Unload_Enemies : MonoBehaviour
{
    public List<GameObject> enemiesToDestroy;
    public GameObject parentOfEnemiesToLoad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < enemiesToDestroy.Count; i++)
            {
                Destroy(enemiesToDestroy[i]);
            }

            if (parentOfEnemiesToLoad != null)
            {
                parentOfEnemiesToLoad.SetActive(true);
            }

            Destroy(this);
        }
    }

}
