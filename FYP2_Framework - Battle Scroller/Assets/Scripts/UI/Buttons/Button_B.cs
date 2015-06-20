using UnityEngine;
using System.Collections;

public class Button_B : Button 
{
    // *** Inherited Virtual Functions *** //
    public override void ExecuteFunction()
    {
    }

    //Use this for initialization
    void Start()
    {
        //Set Type
        this.ButtonType = BType.BUTTON_B;
    }

    //Update is called once per frame
    void Update()
    {
        //Update from Parent
        this.StaticUpdate();
    }
}
