using UnityEngine;
using System.Collections;

// *** TILE SCROLLING CLASS *** //
// ***     AUTHOR: SLIFE    *** //

// --- Shifts Tile dynamically with Camera Pos
// --- Provides an "Endless Map" Effect

public class TileScript : MonoBehaviour 
{
	//Use this for initialization
	void Start () 
    {
	
	}
	
	//Update is called once per frame
	void Update () 
    {
	    //Set Cam Pos
        float Cam_Pos_X = Camera.main.transform.position.x + 25;
        float Cam_Pos_Y = Camera.main.transform.position.y + 14;

        //Shift Tile (Right)
        if (Cam_Pos_X >= this.transform.position.x)
        {
            float NewXPos = this.transform.position.x;
            NewXPos += MapGenerator.GROUND_HOR * this.collider.bounds.size.x;
            this.transform.position = new Vector3(NewXPos, this.transform.position.y, this.transform.position.z);
        }

        //Shift Tile (Left)
        if (Cam_Pos_X <= this.transform.position.x)
        {
            float NewXPos = this.transform.position.x;
            NewXPos -= MapGenerator.GROUND_HOR * this.collider.bounds.size.x;
            this.transform.position = new Vector3(NewXPos, this.transform.position.y, this.transform.position.z);
        }

        //Shift Tile (Up)
        if (Cam_Pos_Y >= this.transform.position.y)
        {
            float NewYPos = this.transform.position.y;
            NewYPos += MapGenerator.GROUND_VER * this.collider.bounds.size.y;
            this.transform.position = new Vector3(this.transform.position.x, NewYPos, this.transform.position.z);
        }

        //Shift Tile (Left)
        if (Cam_Pos_Y <= this.transform.position.y)
        {
            float NewYPos = this.transform.position.y;
            NewYPos -= MapGenerator.GROUND_VER * this.collider.bounds.size.y;
            this.transform.position = new Vector3(this.transform.position.x, NewYPos, this.transform.position.z);
        }
	}
}
