using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAbilities : MonoBehaviour
{
    private GameObject mainChar;
    public Image greenAbility;
    public Image blueAbility;
    public Image yellowAbility;
    public Image redAbility;

    private GameObject FeetCollider;

    private bool greenCooled;
    private bool blueCooled;
    private bool yellowCooled;
    private bool redCooled;

    public float FADE_ALPHA_LEVEL = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        mainChar = GameObject.Find("MainCharacter");
        FeetCollider = GameObject.Find("FeetCollider");

    }

    // Update is called once per frame
    void Update()
    {
        greenCooled = FeetCollider.GetComponent<FeetCollider>().GetJumpCounter() > 0;
        blueCooled = mainChar.GetComponent<Bubble>().GetIfCooled();
        yellowCooled = mainChar.GetComponent<Dash>().GetIfCooled();

        CheckForNewAbility();

        //UnFade/Make solid if cooled and isn't
        if (greenCooled && greenAbility.color.a < 1)
        {
            UnFade(greenAbility);
        }

        if (blueCooled && blueAbility.color.a < 1)
        {
            UnFade(blueAbility);
        }

        if (yellowCooled && yellowAbility.color.a < 1)
        {
            UnFade(yellowAbility);
        }


        //Fade out if not cooled and isn't faded
        if (!greenCooled && greenAbility.color.a == 1)
        {
            FadeOut(greenAbility);
        }

        if (!blueCooled && blueAbility.color.a == 1)
        {
            FadeOut(blueAbility);
        }

        if (!yellowCooled && yellowAbility.color.a == 1)
        {
            FadeOut(yellowAbility);
        }


    }

    private void FadeOut(Image UIElement)
    {
        Color temp = UIElement.color;
        temp.a = FADE_ALPHA_LEVEL;
        UIElement.color = temp;
    }

    private void UnFade(Image UIElement)
    {
        Color temp = UIElement.color;
        temp.a = 1;
        UIElement.color = temp;
    }

    private void CheckForNewAbility()
    {
        if (Globals.color == "WHITE")
        {
            greenAbility.gameObject.SetActive(false);
            blueAbility.gameObject.SetActive(false);
            yellowAbility.gameObject.SetActive(false);
            redAbility.gameObject.SetActive(false);
        }
        else if (Globals.color == "GREEN")
        {
            greenAbility.gameObject.SetActive(true);
            blueAbility.gameObject.SetActive(false);
            yellowAbility.gameObject.SetActive(false);
            redAbility.gameObject.SetActive(false);
        }
        else if (Globals.color == "BLUE")
        {
            greenAbility.gameObject.SetActive(true);
            blueAbility.gameObject.SetActive(true);
            yellowAbility.gameObject.SetActive(false);
            redAbility.gameObject.SetActive(false);
        }
        else if (Globals.color == "YELLOW")
        {
            greenAbility.gameObject.SetActive(true);
            blueAbility.gameObject.SetActive(true);
            yellowAbility.gameObject.SetActive(true);
            redAbility.gameObject.SetActive(false);
        }
        else if (Globals.color == "RED")
        {
            greenAbility.gameObject.SetActive(true);
            blueAbility.gameObject.SetActive(true);
            yellowAbility.gameObject.SetActive(true);
            redAbility.gameObject.SetActive(true);
        }
    }

}
