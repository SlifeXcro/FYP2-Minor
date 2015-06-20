using UnityEngine;
using System.Collections;

public class Button_Exit : Button
{
    // *** Inherited Virtual Functions *** //
    public override void ExecuteFunction()
    {
        Application.Quit();
        Debug.Log("EXIT APPLICATION");
    }

    //Use this for initialization
    void Start()
    {
        //Set Type
        this.ButtonType = BType.BUTTON_EXIT;
    }

    //Update is called once per frame
    void Update()
    {
        //Update from Parent
        this.StaticUpdate();
    }
}
