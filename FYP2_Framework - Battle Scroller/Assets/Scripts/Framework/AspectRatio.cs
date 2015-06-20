using UnityEngine;
using System.Collections;

public class AspectRatio : MonoBehaviour
{
    //Singleton Structure
    protected static AspectRatio mInstance;
    public static AspectRatio Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject tempObj = new GameObject();
                mInstance = tempObj.AddComponent<AspectRatio>();
                Destroy(tempObj);
            }
            return mInstance;
        }
    }

    Vector2 OriginalRes, CurrentRes;
    float Scale;
    Vector2 Storage, InitialScale;
    public static bool AspectChanged = false;

    //Use this for initialization
    void Start()
    {
        OriginalRes.Set(1280.0f, 720.0f);

        InitialScale = this.transform.localScale;

        SetScale();
    }

    void SetScale()
    {
        CurrentRes.Set(Screen.width, Screen.height);
        this.Storage = CurrentRes;

        Scale = (CurrentRes.x / CurrentRes.y) / (OriginalRes.x / OriginalRes.y);

        Vector2 TempScale = InitialScale;
        TempScale.x *= Scale;
        TempScale.y *= Scale;

        this.transform.localScale = new Vector3(TempScale.x, TempScale.y, this.transform.localScale.z);
        AspectChanged = false;
    }

    public Vector3 SetObjScale(GameObject Obj)
    {
        Vector2 TempScale = Obj.transform.localScale;
        TempScale.x *= Scale;
        TempScale.y *= Scale;
        return (new Vector3(TempScale.x, TempScale.y, Obj.transform.localScale.z));
    }

    //Update is called once per frame
    void Update()
    {
        if (Storage != new Vector2(Screen.width, Screen.height))
        {
            AspectChanged = true;
            SetScale();
        }
    }
}
