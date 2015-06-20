using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColliderManager : MonoBehaviour
{
    //Flag to Check if Unit has made contact with Unwalkable Objects
    public bool CollidedUnwalkable = false;

    //List of Colliders
    public List<CollisionRegion> CollidersList = new List<CollisionRegion>();

    void Update()
    {
        //Loop through CollidersList
        foreach (CollisionRegion col in CollidersList)
        {
            switch (col.Type)
            {
                case CollisionRegion.RegionType.REGION_UNWALKABLE:
                    CollidedUnwalkable = col.inRegion;
                    break;
            }
        }
    }
}