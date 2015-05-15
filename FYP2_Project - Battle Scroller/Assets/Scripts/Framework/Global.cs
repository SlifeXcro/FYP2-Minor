using UnityEngine;
using System.Collections;

//Global Class
public class Global : MonoBehaviour
{
	// *** Global Variables *** //
    public static bool GamePause = false;
    public static bool StopMovement = false; //Stops Player Movement
    public static Item.ItemType CurrentItemType = Item.ItemType.ITEM_DEFAULT; //Only 1 instance of Item's type
    public static int CurrentItemID = -1; //Only 1 instance of Item's ID
    public static bool FreeCam = false; //Detect if Camera is "Free"

    //Start Function
    void Start()
    {
        //Set Screen Orientation
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    //Update Function
    void Update()
    {
        //Stop Char Movement during Free Cam
        if (FreeCam)
            StopMovement = true;
    }
}
