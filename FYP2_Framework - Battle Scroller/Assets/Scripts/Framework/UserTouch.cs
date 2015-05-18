using UnityEngine;
using System.Collections;

public class UserTouch : MonoBehaviour 
{
    //Singleton Structure
    protected static UserTouch mInstance;
    public static UserTouch Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject tempObj = new GameObject();
                mInstance = tempObj.AddComponent<UserTouch>();
                Destroy(tempObj);
            }
            return mInstance;
        }
    }

    public bool Touch_Unwalkable = false;

    //Use this for initialization
    void Start()
    {
        mInstance = this;
    }

    void OnTriggerEnter(Collider col)
    {
        //Detects if User has touched on an Unwalkable Region
        if (col.gameObject.tag == "UNWALKABLE")
            Touch_Unwalkable = true;
    }
    void OnTriggerExit(Collider col)
    {
        //Detects if User has left his touch on an Unwalkable Region
        if (col.gameObject.tag == "UNWALKABLE")
            Touch_Unwalkable = false;
    }
	
	//Update is called once per frame
	void Update () 
    {
        Vector3 TouchPos = Vector3.zero;

        //Change Touch Position on GUI to World Coordinates
#if UNITY_EDITOR || UNITY_STANDALONE
        TouchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
#elif UNITY_ANDROID
        foreach (Touch touch in Input.touches)
            TouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
#endif
        this.transform.position = new Vector3(TouchPos.x, TouchPos.y, 0);
	}
}
