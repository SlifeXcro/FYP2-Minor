using UnityEngine;
using System.Collections;

public class CollisionRegion : MonoBehaviour 
{
    //Flag to check if Unit has made contact with another Collider
	public bool inRegion = false;

    //Region's Type
    public enum RegionType
    {
        REGION_UNWALKABLE
    } public RegionType Type;

	void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "UNWALKABLE" || col.gameObject.tag == "PLAYER")
            inRegion = true;
    }

    void OnTriggerExit(Collider col)
    {
		if (col.gameObject.tag == "UNWALKABLE" || col.gameObject.tag == "PLAYER")
			inRegion = false;
    }
}