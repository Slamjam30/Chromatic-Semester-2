using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOverrider : MonoBehaviour
{
    private AnimationOverrider amO;
    private Animator am;
    [SerializeField] public AnimatorOverrideController GREEN;
    [SerializeField] public AnimatorOverrideController BLUE;
    [SerializeField] public AnimatorOverrideController YELLOW;
    [SerializeField] public AnimatorOverrideController RED;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        if (Globals.color.Equals("GREEN"))
        {
            am.runtimeAnimatorController = GREEN;
        }
        else if (Globals.color.Equals("BLUE"))
        {
            am.runtimeAnimatorController = BLUE;
        }
        else if (Globals.color.Equals("YELLOW"))
        {
            am.runtimeAnimatorController = YELLOW;
        }
        else if (Globals.color.Equals("RED"))
        {
            am.runtimeAnimatorController = RED;
        }
        else if (Globals.color.Equals("DONE"))
        {
            am.runtimeAnimatorController = RED;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
