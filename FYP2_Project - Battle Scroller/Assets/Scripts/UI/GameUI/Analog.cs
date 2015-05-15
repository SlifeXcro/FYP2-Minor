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
    public bool Move = false;
    public Transform Inner;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "TOUCH")
        {
            Move = true;
            TravelDir = (col.transform.position - this.transform.position).normalized;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "TOUCH")
            Move = false;
    }

    public Vector3 GetTravelDir()
    {
        return TravelDir;
    }

    void Start()
    {
        mInstance = this;
    }

    //Update is called once per frame
    void Update()
    {
        if (Move)
            Inner.transform.position = new Vector3(UserTouch.Instance.transform.position.x, UserTouch.Instance.transform.position.y, Inner.transform.position.z);
        else
            Inner.transform.position = InitialPos.position;
    }
}
