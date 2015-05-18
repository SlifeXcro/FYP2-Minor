using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// *** FRAMEWORK: INVENTORY CLASS *** //
// ***       AUTHOR: SLIFE        *** //

// --- Skeletal Inventory
// --- Adding of item Dynamically
// --- Deleting Item Dynamically, with "Push Back"(s)

public class Inventory : MonoBehaviour
{
    public const short MAX_ITEMS = 20;                  // Maximum amt of Items (Item Expansion Pack?)
    public List<Item> ItemsList = new List<Item>();     // Item Storage
    public GameObject[] Slot;                           // Prefabs to determine pos of slots in the Inventory
    private short UniqueID = 0;                         // ID to access Items
    public Sprite[] ListOfSprites;                      // Sprites for items in the Inventory

    public Item InstantiatedItem; //Placeholder Prefab

    //Use this for initialization
    void Start()
    {

    }

    //Determines the Sprite of the item in the Inventory
    public Sprite DetermineSprite(Item.ItemType IType)
    {
        return ListOfSprites[(int)IType];
    }

    //Adds an Item to the Inventory
    public bool AddItem(Item.ItemType IType)
    {
        //Makes sure storage does not exceed max amount
        if (ItemsList.Count < MAX_ITEMS)                            
        {
            //Generates a new unique ID
            //This value will be constantly increasing
            //It will always be unique to the item
            ++UniqueID;

            //Instantiates the Item on Inventory slot (Slot pos etc to be determined by Slot[] prefab)
            Item TempItem = Instantiate(InstantiatedItem, Slot[ItemsList.Count].gameObject.transform.position, Quaternion.identity) as Item;
            TempItem.IType = IType;                                                             
            TempItem.ID = UniqueID;                                                             
            TempItem.Slot = ItemsList.Count + 1; //Slot starts from 1, not 0
            TempItem.GetComponent<SpriteRenderer>().sprite = DetermineSprite(TempItem.IType);
            ItemsList.Add(TempItem);      

            Debug.Log("Item Added! Item Type: " + (int)IType);
            Debug.Log("Total Item Count: " + ItemsList.Count);

            return true;
        }
        return false;
    }

    //Deletes an Item in the Inventory
    public void DeleteItem(int ItemSlot)
    {
        //Used to determine if Items need to be pushed forward
        bool Push = false;

        //Makes sure an item is in that selected Slot
        if (ItemSlot - 1 < ItemsList.Count)
        {
            Debug.Log("Item Deleted! Item Type: " + (int)ItemsList[ItemSlot - 1].IType);
            Destroy(ItemsList[ItemSlot - 1].gameObject); 
            ItemsList.Remove(ItemsList[ItemSlot - 1]); //Removes item from desired Slot
            Debug.Log("Total Item Count: " + ItemsList.Count);
            Push = true; //Item successfully deleted, proceed to Push 
        }

        if (Push)
        {
            //Push Back all other Items
            for (int i = 0; i < ItemsList.Count; ++i)
            {
                if (ItemsList[i].Slot > ItemSlot) //Push back all items AFTER the deleted item 
                {
                    --ItemsList[i].Slot;

                    //Re-Position Slot
                    ItemsList[i].transform.position = Slot[ItemsList[i].Slot - 1].gameObject.transform.position; 
                }
            }
            Push = false; //Only push items ONCE
        }
    }

    //Update is called once per frame
    void Update()
    {
        //if value stays -1, no items to delete
        int SlotToDelete = -1;

        //Detect if any items need to be deleted
        for (short i = 0; i < ItemsList.Count; ++i)
        {
            if (ItemsList[i].Delete) //Yes
            {
                //Store the slot to delete
                SlotToDelete = ItemsList[i].Slot;

                //Store once, delete, then store again
                //Breaking prevents overwriting of previous value.
                break; 
            }
        }

        //Something needs to be deleted
        if (SlotToDelete != -1)
            DeleteItem(SlotToDelete);
    }
}
