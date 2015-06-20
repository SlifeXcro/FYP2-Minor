using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Analog : MonoBehaviour
{
    //Singleton Structure
    protected static Analog mInstance;
    public static Analog Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject tempObj = new GameObject();
                mInstance = tempObj.AddComponent<Analog>();
                Destroy(tempObj);
            }
            return mInstance;
        }
    }

    // *** Variables *** //
    Vector3 TravelDir = Vector3.zero;
    public Transform InitialPos; //Inner
    public bool Move = false,
                WithinAnalogRegion = false,
                onTouch = false;
    public Transform Inner;
    public GameObject Region;

    void OnTriggerStay(Collider col)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (col.tag == "TOUCH")
            onTouch = true;
#endif
    }

    void OnTriggerExit(Collider col)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (col.tag == "TOUCH")
            onTouch = false;
#endif
    }

    public Vector3 GetTravelDir()
    {
        return this.TravelDir;
    }

    void Start()
    {
        mInstance = this;
    }

    //Update is called once per frame
    void Update()
    {
        //Check if Input is within Analog Region
        if (InputScript.InputCollided(Region.GetComponent<Collider>()))
            WithinAnalogRegion = true;
        else
            WithinAnalogRegion = false;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0) && onTouch)
            Move = true;
        if (Move)
        {
            if ((!Input.GetMouseButton(0) && !onTouch) || !WithinAnalogRegion)
                Move = false;
        }
#elif UNITY_ANDROID
        if (InputScript.InputCollided(this.GetComponent<Collider>()))
            onTouch = Move = true;
        else
            onTouch = false;

        bool allClear = true;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended)
                allClear = false;
        }

        if (Move)
        {
            if ((allClear && !onTouch) || !WithinAnalogRegion)
                Move = false;
        }
#endif

        if (Move && WithinAnalogRegion)
        {
            TravelDir = (Inner.transform.position - this.transform.position).normalized;

            Vector3 InnerPos = new Vector3(Mathf.Clamp(UserTouch.Instance.transform.position.x,
                                                       this.transform.position.x - this.GetComponent<Collider>().bounds.size.x,
                                                       this.transform.position.x + this.GetComponent<Collider>().bounds.size.x),
                                           Mathf.Clamp(UserTouch.Instance.transform.position.y,
                                                       this.transform.position.y - this.GetComponent<Collider>().bounds.size.y,
                                                       this.transform.position.y + this.GetComponent<Collider>().bounds.size.y),
                                           Inner.transform.position.z);

            Inner.transform.position = InnerPos;
        }
        else
            Inner.transform.position = InitialPos.position;
    }
}
