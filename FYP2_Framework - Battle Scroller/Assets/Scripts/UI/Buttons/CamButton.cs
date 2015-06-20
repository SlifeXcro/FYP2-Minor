using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CamButton : Button
{
    //Cam Button's Type
    public enum CB_Type
    {
        CAM_CHAR,
        CAM_FREE
    } public CB_Type Type;

    // *** Inherited Virtual Functions *** //
    public override void ExecuteFunction()
    {
        switch (Type)
        {
            case CB_Type.CAM_CHAR:
                Global.FreeCam = false;
                Debug.Log("Character Camera Mode");
                break;
            case CB_Type.CAM_FREE:
                if (!InfoDisplayer.IsNull())
                {
                    Global.FreeCam = true;
                    Debug.Log("Free Camera Mode");
                }
                break;
        }
    }

    //Backdrop
    public Image Backdrop;

    //Use this for initialization
    void Start()
    {
        //Set Button Type
        this.ButtonType = BType.BUTTON_CAM;
    }

    //Update is called once per frame
    void Update()
    {
        //Update from Parent Class
        this.StaticUpdate();

        //Toggle Sprite Change
        if (Global.FreeCam)
        {
            switch (Type)
            {
                case CB_Type.CAM_FREE:
                    Backdrop.sprite = Tex[1];
                    break;
                case CB_Type.CAM_CHAR:
                    Backdrop.sprite = Tex[0];
                    break;
            }
        }
        else
        {
            switch (Type)
            {
                case CB_Type.CAM_FREE:
                    Backdrop.sprite = Tex[0];
                    break;
                case CB_Type.CAM_CHAR:
                    Backdrop.sprite = Tex[1];
                    break;
            }
        }
    }
}
