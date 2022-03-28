using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;

namespace UnityEngine.Rendering
{
    public class ColorChange : MonoBehaviour
    {
        //UNCHECK the Color Adjustments override used by the volume.
        private GameObject MainChar;
        public Volume selvolume;
        public bool coloring;
        private ColorAdjustments colAdj;
        private readonly float SPEED = .04f;

        // Start is called before the first frame update
        void Start()
        {
            MainChar = GameObject.FindWithTag("Player");

            colAdj = ScriptableObject.CreateInstance<ColorAdjustments>();
            colAdj.saturation.overrideState = true;
            colAdj.saturation.value = -100;

            selvolume.profile.components.Add(colAdj);
        }

        // Update is called once per frame
        void Update()
        {
            
            if (coloring == true && colAdj.saturation.value < 0)
            {
                
                colAdj.saturation.value += SPEED;

            }

            if (MainChar.GetComponent<Movement>().colorIn)
            {
                coloring = true;
            }


        }

    }


}
