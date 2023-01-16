using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryBar : MonoBehaviour
{
    GameObject[] itemSlot; //array of itemslots
    List<ItemInfo> inventoryList; //hold reference to inventory list
    public Inventory inventoryScript; //hold reference to inventory script

    // Start is called before the first frame update
    void Start()
    {
        itemSlot = new GameObject[9]; //create empty array of 3 game objects
        inventoryList = new List<ItemInfo>();

        // the item slots will be children of the Inventory Bar
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i] = transform.GetChild(i).gameObject;
        }

        //inventoryScript = NetworkClient.localPlayer.gameObject.GetComponent<Inventory>();
        // hold reference to use later in UpdateBar
    }
    //subscribe UpdateBar to OnInventoryChange event

    void OnEnable()
    {
        Inventory.OnInventoryChange += UpdateBar;
    }

    void OnDisable()
    {
        Inventory.OnInventoryChange -= UpdateBar;
    }


    void UpdateBar()
    {
        inventoryList = inventoryScript.GetInventoryList();
        
        for (int i = 0; i < inventoryList.Count; i++)
        {
            itemSlot[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
            itemSlot[i].transform.GetChild(1).GetComponent<Image>().sprite = inventoryList[i].ItemIcon;
            itemSlot[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = inventoryList[i].StackSize.ToString();
        }
    }


}