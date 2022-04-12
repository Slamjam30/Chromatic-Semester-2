using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static int maxHealth = 5;
    //Make color and tag for the color the same- just go all caps
    public static string color = "WHITE";
    public static Vector2 startLeaveScenePos;
    //Alpha is from 0 to 1. If speed is 0.04f, that means 4%/second b/c UnGrayscale uses Time.deltaTime.
    public static float grayscaleSpeed = 0.1f;

    //If in a boss fight that is its own scene, can reset to the beginning of that scene if the player dies
    public static bool inFight;

}
