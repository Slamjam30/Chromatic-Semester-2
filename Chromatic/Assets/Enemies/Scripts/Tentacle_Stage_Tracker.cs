using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle_Stage_Tracker : MonoBehaviour
{
    // The stage the boss fight is currently in
    public static string currentStage;

    public int octoHeadHealth = 10;

    public static string initialJabTentacle = "left";

    public static bool startSwingAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        currentStage = "PincerStage_Attack";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
