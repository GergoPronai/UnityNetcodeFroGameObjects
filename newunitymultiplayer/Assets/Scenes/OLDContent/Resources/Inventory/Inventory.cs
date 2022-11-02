using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private List<ItemInfo> inventoryList;

    // delegate event for inventory updates
    public delegate void inventoryDelegate();
    public static event inventoryDelegate OnInventoryChange;

    // Start is called before the first frame update
    void Start()
    {
        this.inventoryList = new List<ItemInfo>();
    }

    public List<ItemInfo> GetInventoryList()
    {
        return this.inventoryList;
    }

    public void AddItem(ItemInfo item)
    {
        //if inventory is empty add item
        if (inventoryList.Count == 0)
        {
            this.inventoryList.Add(item);
        }
        else //inventory is not empty
        {
            bool inList = false; //bool for when item has been added

            foreach (ItemInfo i in this.inventoryList)
            {
                if (item.Name == i.Name) //if there is an item with the same name
                {
                    int temp = i.StackSize + item.StackSize;
                    if (temp <= 99)
                    {
                        i.StackSize += item.StackSize;
                        inList = true;
                    }
                    else
                    {
                        this.inventoryList.Add(item);
                    }
                }
            }

            if (!inList)   //so, if it's can't be stacked, add the item to List
            {
                inventoryList.Add(item);
            }
        }

        OnInventoryChange?.Invoke(); //null reference check, then invoke event
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            ItemScript otherItemScript = other.GetComponent<ItemScript>();
            ItemInfo otherItem = otherItemScript.GetItem();

            AddItem(otherItem);

            Destroy(other.gameObject);
        }
    }

    
    //remove items from inventory (after they are used)
    void RemoveItem(int i)
    {
        ItemInfo itemToRemove = inventoryList[i];

        //if it's the last of the stack, then remove the item and disable the ability
        if (itemToRemove.StackSize == 1)
        {
            inventoryList.Remove(itemToRemove);
        }
        else if (itemToRemove.StackSize > 1)
        {
            //if it's not the last of the stack, then just decrement the stacksize
            itemToRemove.StackSize--;
        }
        OnInventoryChange?.Invoke(); //null reference check, then invoke event
    }
}