using UnityEngine;
using System.Collections;

public class Button_Transition : Button
{
    // *** Inherited Virtual Functions *** //
    public override void ExecuteFunction()
    {
        Fading.Instance.ResetFade(false);
        Selected = true;
    }
    bool Selected = false;

    //Scene's Name
    public string SceneName;

    //Use this for initialization
    void Start()
    {
        //Set Type
        this.ButtonType = BType.BUTTON_TRANSITION;
    }

    //Update is called once per frame
    void Update()
    {
        //Update from Parent
        this.StaticUpdate();

        //Transit Level
        if (Fading.Instance.isFaded && Selected)
            Application.LoadLevel(SceneName);
    }
}
