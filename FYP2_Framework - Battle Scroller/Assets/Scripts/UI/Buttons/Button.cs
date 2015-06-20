using UnityEngine;
using System.Collections;

//Parent Class for all Buttons
public class Button : MonoBehaviour
{
    //Flag to determine if Button has been selected
    public bool Selected = false,
                Execute = false,
                OnClick = true;

    // 0 - Default Tex
    // 1 - Selected Tex
    public Sprite[] Tex = new Sprite[2];
    public Sprite SpriteToChange;

    public enum BType
    {
        BUTTON_DEFAULT,
        BUTTON_PAUSE,
        BUTTON_COMBAT,
        BUTTON_CAM,
        BUTTON_A,
        BUTTON_B,
        BUTTON_TRANSITION,
        BUTTON_EXIT
    } public BType ButtonType = BType.BUTTON_DEFAULT;

    //Every Button has its own Function
    public virtual void ExecuteFunction()
    {
        Debug.Log("Button Clicked!");
    }

    //Parent Update
    public void StaticUpdate()
    {
        if (InputScript.InputCollided(this.gameObject.GetComponent<Collider>(), OnClick))
        {
            SpriteToChange = Tex[1]; //Change to Clicked Sprite
            Selected = Execute = true;
            ExecuteFunction();
        }
        else
        {
            SpriteToChange = Tex[0]; //Reset to Default Sprite
            Selected = Execute = false;
        }
    }

    //Update is called once per frame
    void Update()
    {
        StaticUpdate();
    }
}
