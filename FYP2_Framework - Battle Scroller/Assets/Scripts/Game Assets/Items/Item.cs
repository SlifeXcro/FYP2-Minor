using UnityEngine;
using System.Collections;

//Parent Class for all Items
public class Item : MonoBehaviour
{
    //Item's Type
    public enum ItemType
    {
        ITEM_DEFAULT
    } public ItemType IType = ItemType.ITEM_DEFAULT;

    public bool Delete = false; // Flag to detect if item has been deleted
    public int ID = -1, Slot = -1;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PLAYER")
        {
            //Only one instance of item can be picked
            Global.CurrentItemType = this.IType;
            Global.CurrentItemID = this.ID;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "PLAYER")
        {

        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && InputScript.InputCollided(this.gameObject.GetComponent<Collider>())) // Right Click
            Delete = true;  // Delete the item
    }
}
