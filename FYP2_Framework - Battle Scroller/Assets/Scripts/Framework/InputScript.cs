using UnityEngine;
using System.Collections;

public class InputScript : MonoBehaviour
{
    //Singleton Structure
    protected static InputScript mInstance;
    public static InputScript Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject tempObj = new GameObject();
                mInstance = tempObj.AddComponent<InputScript>();
                Destroy(tempObj);
            }
            return mInstance;
        }
    }

    public static bool TouchDown = false;

    //Use this for initialization
    void Start()
    {
        mInstance = this;
    }

    //Process User Input
    public static bool InputCollided(Collider col, bool Select = false)
    {
        Vector3 WorldPos, TouchPos;
        WorldPos = TouchPos = Vector3.zero;
        bool Collided = false;

#if UNITY_EDITOR || UNITY_STANDALONE
        WorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TouchPos = new Vector3(WorldPos.x, WorldPos.y, 0);
        if (col.bounds.Contains(TouchPos))
            Collided = true;
#elif UNITY_ANDROID
        foreach (Touch touch in Input.touches)
        {
            WorldPos = Camera.main.ScreenToWorldPoint(touch.position);
            TouchPos = new Vector3(WorldPos.x, WorldPos.y, 0);
            if (col.bounds.Contains(TouchPos))
                Collided = true;
        }
#endif

        if (Collided)
        {
            if (Select)
            {
                bool proceed = false;
#if UNITY_EDITOR || UNITY_STANDALONE
                if (Input.GetMouseButtonUp(0))
                    proceed = true;
#elif UNITY_ANDROID
                bool allClear = true;
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Ended)
                        allClear = false;
                }
                proceed = !allClear;
#endif
                return proceed;
            }
            return true;
        }
        return false;
    }

    //Process User Input (Modifies Pos)
    public static bool InputCollided(Collider col, out Vector3 CollidedPos, bool Select = false)
    {
        Vector3 WorldPos, TouchPos;
        WorldPos = TouchPos = CollidedPos = Vector3.zero;
        bool Collided = false;

#if UNITY_EDITOR || UNITY_STANDALONE
        WorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TouchPos = new Vector3(WorldPos.x, WorldPos.y, 0);
        CollidedPos = TouchPos;
        if (col.bounds.Contains(TouchPos))
            Collided = true;
#elif UNITY_ANDROID
        foreach (Touch touch in Input.touches)
        {
            WorldPos = Camera.main.ScreenToWorldPoint(touch.position);
            TouchPos = new Vector3(WorldPos.x, WorldPos.y, 0);
            CollidedPos = TouchPos;
            if (col.bounds.Contains(TouchPos))
                Collided = true;
        }
#endif

        if (Collided)
        {
            if (Select)
            {
                bool proceed = false;
#if UNITY_EDITOR || UNITY_STANDALONE
                if (Input.GetMouseButtonUp(0))
                    proceed = true;
#elif UNITY_ANDROID
                bool allClear = true;
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Ended)
                        allClear = false;
                }
                proceed = !allClear;
#endif
                return proceed;
            }
            return true;
        }
        return false;
    }

    //Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
            TouchDown = true;
        if (Input.GetMouseButtonUp(0))
            TouchDown = false;
#elif UNITY_ANDROID
        //Analog
        foreach (Touch touch in Input.touches)
        {
            bool Proceed =  false;

            //1st Touch
            if (Input.touches.Length <= 1)
            {
                //Analog not moving, thus indicating firing 
                //   (touching else where on the Screen)
                if (!Analog.Instance.Move)
                    Proceed = true;

                //1 Touch + Analog Moving = Not Firing
                else
                    TouchDown = false;
            }

            // > 1 Touch indicates Firing in progress
            else
                Proceed = true;

            //Firing in Progress
            if (Proceed)
            {
                if (touch.phase != TouchPhase.Ended)
                    TouchDown = true;
                else
                    TouchDown = false;
            }
            else if (touch.phase == TouchPhase.Ended)
                UserTouch.Instance.CurFingerIndex = 0;
        }
#endif
    }
}
