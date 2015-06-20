using UnityEngine;
using System.Collections;

public class PauseButton : Button 
{
    // *** Inherited Virtual Functions *** //
    public override void ExecuteFunction()
    {
        Global.GamePause = true;
        Debug.Log("Game has been paused!");
    }

	//Use this for initialization
	void Start () 
    {
	    //Set Button Type
        this.ButtonType = BType.BUTTON_PAUSE;
	}
	
	//Update is called once per frame
	void Update () 
    {
	    //Update from Parent Class
        this.StaticUpdate();
	}
}
