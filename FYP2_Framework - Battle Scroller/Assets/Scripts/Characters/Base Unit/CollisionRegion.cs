using UnityEngine;
using System.Collections;

public class CollisionRegion : MonoBehaviour 
{
    //Flag to Check if Unit has made contact with Unwalkable Objects
    public bool CollidedUnwalkable = false;

    //Check if Unit has Collided with Unwalkable Objects
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "UNWALKABLE" || col.gameObject.tag == "UNIT")
            CollidedUnwalkable = true;
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "UNWALKABLE" || col.gameObject.tag == "UNIT")
            CollidedUnwalkable = false;
    }
}
