using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour 
{
    //Returns True if reaches Time desired
    public static List<float> CurTime = new List<float>();
    public static bool ExecuteTime(float Seconds, short ID)
    {
        if (CurTime.Count < ID)
            CurTime.Add(Seconds);

        CurTime[ID-1] -= Time.deltaTime;
        if (CurTime[ID-1] <= 0)
        {
            CurTime[ID-1] = Seconds;
            return true;
        }
        return false;
    }

    //Returns Elapsed Time with reset
    public static List<float> ElapsedTime = new List<float>();
    public static float GetElapsedTime(float Seconds, short ID)
    {
        if (ElapsedTime.Count < ID)
            ElapsedTime.Add(Seconds);

        ElapsedTime[ID-1] -= Time.deltaTime;
        if (ElapsedTime[ID-1] <= 0)
            ElapsedTime[ID-1] = Seconds;

        return ElapsedTime[ID-1];
    }

	//Use this for initialization
	void Start () 
    {
        CurTime.Clear();
        ElapsedTime.Clear();
	}
	
	//Update is called once per frame
	void Update () 
    {
        
	}
}
