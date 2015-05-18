using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// *** STATIC TIMER CLASS *** //
// ***   AUTHOR: SLIFE    *** //

// --- Dynamically Calculates Passed Time
// --- Dynamically Calculates Elapsed Time

public class Timer : MonoBehaviour 
{
    //Returns True if reaches Time desired
    public static List<float> CurTime = new List<float>();
    public static short GetExecuteID(float Seconds) 
    { CurTime.Add(Seconds); return (short)CurTime.Count; }
    public static bool ExecuteTime(float Seconds, short ID)
    {
        if (ID > CurTime.Count)
            return false;

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
    public static short GetElapsedID(float Seconds)
    { ElapsedTime.Add(Seconds); return (short)ElapsedTime.Count; }
    public static float GetElapsedTime(float Seconds, short ID)
    {
        if (ID > ElapsedTime.Count)
            return -1;

        ElapsedTime[ID-1] -= Time.deltaTime;
        if (ElapsedTime[ID-1] <= 0)
            ElapsedTime[ID-1] = Seconds;

        return ElapsedTime[ID-1];
    }
}
