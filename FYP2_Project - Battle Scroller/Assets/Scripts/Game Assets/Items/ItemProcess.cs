using UnityEngine;
using System.Collections;

public class ItemProcess : MonoBehaviour
{
    public Inventory theInventory;
    public Item[] ListOfItemsInWorld; //All items in the World

    void Start()
    {
        for (int i = 0; i < ListOfItemsInWorld.Length; ++i)
            ListOfItemsInWorld[i].ID = -2 * (i + 1); //Makes sure all items in the World has a unique ID
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       
    }
    void OnTriggerExit2D(Collider2D col)
    {
      
    }

    public void Collect()
    {
        Global.StopMovement = true; //Stop Player Movement

        //Adds Item to Inventory
        if (theInventory.AddItem(Global.CurrentItemType))
        {
            //Item Successfully Added
            for (int i = 0; i < ListOfItemsInWorld.Length; ++i)
            {
                //Collect the Item the player is currently interacting with
                if (ListOfItemsInWorld[i].ID == Global.CurrentItemID)
                {
                    //Destroy entity in World after it has been picked up
                    Destroy(ListOfItemsInWorld[i].gameObject);
                    break;
                }
            }
        }

        Global.StopMovement = false; //Resume Player Movement
    }
}
