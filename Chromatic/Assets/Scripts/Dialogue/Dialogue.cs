using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    //name of NPC
    public string name;
    
    //TextArea(min_lines, max_lines)
    [TextArea(3, 10)]
    //string array
    public string[] sentences;
}
