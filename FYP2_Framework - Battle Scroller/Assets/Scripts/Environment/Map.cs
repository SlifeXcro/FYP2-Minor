using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour 
{
    //Map's Model
    public Model MapModel;

	//Use this for initialization
	void Start () 
    {
	    //Init Tag
        MapModel.gameObject.tag = this.gameObject.tag = "UNWALKABLE";
	}
	
	//Update is called once per frame
	void Update () 
    {
	
	}
}
