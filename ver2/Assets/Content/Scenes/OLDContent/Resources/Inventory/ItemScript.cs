using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Sprite ItemIcon;
    public int StackSize = 1;
    public string Name;
    public string Description;
    public float Value;
    public float Cost;
    public string StatThatMightBeBoosted = null;
    public ItemType ItemType;
    private ItemInfo item;

    // Start is called before the first frame update
    void Start()
    {
        //create a new Item
        item = new ItemInfo();
        item.Name = Name;
        item.ItemIcon = ItemIcon;
        item.StackSize = StackSize;
        item.Description = Description;
        item.Value = Value;
        item.Cost = Cost;
        item.StatThatMightBeBoosted = StatThatMightBeBoosted;
        item.ItemType = ItemType;
    }

    public ItemInfo GetItem()
    {
        return item;
    }


}