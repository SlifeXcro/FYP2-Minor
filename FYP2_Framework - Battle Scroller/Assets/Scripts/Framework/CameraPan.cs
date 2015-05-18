using UnityEngine;
using System.Collections;

// *** CAMERA PANNING CLASS *** //
// ***    AUTHOR: SLIFE     *** //

// --- Pans Camera with Desired Obj Reference
// --- Ability to Freeze X and/or Y Scrolling
// --- Thus suitable for Side Scrollers (Freeze Y Scroll)

public class CameraPan : MonoBehaviour 
{
    //Singleton Structure
    protected static CameraPan mInstance;
    public static CameraPan Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject tempObj = new GameObject();
                mInstance = tempObj.AddComponent<CameraPan>();
                Destroy(tempObj);
            }
            return mInstance;
        }
    }

    //Current Unit
    public Unit theUnit;
    public float ScrollSpeed = 3.7f;

    //Background
    public GameObject Background;

    //Game Objects to be Translated
    public GameObject[] ObjectsList;

    //Buffer Zone for Scrolling
    float ScrollAreaBuffer = 0.0f;

	//Use this for initialization
	void Start () 
    {
	    //Init Instance
        mInstance = this;
	}

    //Camera Panning 
    void PanCamera(GameObject RefObj, float Translation, short Freeze) //0 - Unfreeze; 1 - Freeze X; 2 - Freeze Y;
    {
        //Cap Freeze Value
        if (Freeze > 2)
            Freeze = 0;

        // X
        if (Freeze != 1)
        {
            if (RefObj.transform.position.x > (Camera.main.transform.position.x + Camera.main.rect.size.x * 0.5f) - ScrollAreaBuffer)
            {
                //Tranlsate Camera
                Camera.main.transform.Translate(Translation, 0, 0);

                //Translate Info Displayer
                if (!InfoDisplayer.IsNull())
                    InfoDisplayer.Instance.transform.Translate(Translation, 0, 0);

                //Translate Buttons
                for (short i = 0; i < ObjectsList.Length; ++i)
                    ObjectsList[i].transform.Translate(Translation, 0, 0);

                //Translate BG at 0.5x Speed
                if (Background != null)
                    Background.transform.Translate(Translation * 0.5f, 0, 0);
            }
            else if (RefObj.transform.position.x < (Camera.main.transform.position.x - Camera.main.rect.size.x * 0.5f) + ScrollAreaBuffer)
            {
                //Tranlsate Camera
                Camera.main.transform.Translate(-Translation, 0, 0);

                //Translate Info Displayer
                if (!InfoDisplayer.IsNull())
                    InfoDisplayer.Instance.transform.Translate(-Translation, 0, 0);

                //Translate Buttons
                for (short i = 0; i < ObjectsList.Length; ++i)
                    ObjectsList[i].transform.Translate(-Translation, 0, 0);

                //Translate BG at 0.5x Speed
                if (Background != null)
                    Background.transform.Translate(-Translation * 0.5f, 0, 0);
            }
            else
                Global.StopMovement = false;
        }

        // Y
        if (Freeze != 2)
        {
            if (RefObj.transform.position.y > (Camera.main.transform.position.y + Camera.main.rect.size.y * 0.5f) - ScrollAreaBuffer)
            {
                //Tranlsate Camera
                Camera.main.transform.Translate(0, Translation, 0);

                //Translate Info Displayer
                if (!InfoDisplayer.IsNull())
                    InfoDisplayer.Instance.transform.Translate(0, Translation, 0);

                //Translate Buttons
                for (short i = 0; i < ObjectsList.Length; ++i)
                    ObjectsList[i].transform.Translate(0, Translation, 0);

                //Translate BG at 0.5x Speed
                if (Background != null)
                    Background.transform.Translate(0, Translation * 0.5f, 0);
            }
            else if (RefObj.transform.position.y < (Camera.main.transform.position.y - Camera.main.rect.size.y * 0.5f) + ScrollAreaBuffer)
            {
                //Tranlsate Camera
                Camera.main.transform.Translate(0, -Translation, 0);

                //Translate Info Displayer
                if (!InfoDisplayer.IsNull())
                    InfoDisplayer.Instance.transform.Translate(0, -Translation, 0);

                //Translate Buttons
                for (short i = 0; i < ObjectsList.Length; ++i)
                    ObjectsList[i].transform.Translate(0, -Translation, 0);

                //Translate BG at 0.5x Speed
                if (Background != null)
                    Background.transform.Translate(0, -Translation * 0.5f, 0);
            }
            else
                Global.StopMovement = false;
        }
    }
	
	//Update is called once per frame
	void Update () 
    {
        //Pan Camera
        if (!Global.FreeCam && theUnit != null)
            PanCamera(theUnit.gameObject, ScrollSpeed * Time.deltaTime, 2);
        else
            PanCamera(UserTouch.Instance.gameObject, ScrollSpeed * Time.deltaTime * 2.0f, 2);
	}
}
