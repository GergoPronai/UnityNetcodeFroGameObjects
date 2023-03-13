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
    public void RemoveItem(int i)
    {
        ItemInfo itemToRemove = inventoryList[i];
        float durationInSeconds = 60f;

        switch (itemToRemove.ItemType)
        {
            case ItemType.StatBoost:
                foreach (AttackInfo attackInfo in this.transform.GetComponent<PlayergameObjScript>().attackInfos)
                {
                    // randomly choose an attribute to increase
                    int attributeIndex = Random.Range(0, 3); // 0 = Damage, 1 = Accuracy, 2 = CritChance


                    // apply a random increase to the chosen attribute
                    switch (attributeIndex)
                    {
                        case 0:
                            // apply random increase to Damage
                            float originalDamage = attackInfo.Damage;
                            attackInfo.Damage += itemToRemove.Value;
                            StartCoroutine(DecreaseAttributeOverTime(attackInfo, originalDamage, attackInfo.Damage, durationInSeconds, attributeIndex));
                            break;
                        case 1:
                            // apply random increase to Accuracy
                            float originalAccuracy = attackInfo.Accuracy;
                            attackInfo.Accuracy += itemToRemove.Value;
                            StartCoroutine(DecreaseAttributeOverTime(attackInfo, originalAccuracy, attackInfo.Accuracy, durationInSeconds, attributeIndex));
                            break;
                        case 2:
                            // apply random increase to CritChance
                            float originalCritChance = attackInfo.CritChance;
                            attackInfo.CritChance += itemToRemove.Value;
                            StartCoroutine(DecreaseAttributeOverTime(attackInfo, originalCritChance, attackInfo.CritChance, durationInSeconds, attributeIndex));
                            break;

                    }
                }

                break;
            case ItemType.Healing:
                this.transform.GetComponent<PlayergameObjScript>().AddHealth(itemToRemove.Value);
                break;
            case ItemType.Plot:
                break;
        }
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


    IEnumerator DecreaseAttributeOverTime(AttackInfo attackInfo, float initialValue, float finalValue, float duration,int attributeIndex)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float currentValue = Mathf.Lerp(finalValue, initialValue, t);

            // apply the current attribute value to the AttackInfo object
            switch (attributeIndex)
            {
                case 0:
                    attackInfo.Damage = currentValue;
                    break;
                case 1:
                    attackInfo.Accuracy = currentValue;
                    break;
                case 2:
                    attackInfo.CritChance = currentValue;
                    break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // set the final attribute value to the AttackInfo object
        switch (attributeIndex)
        {
            case 0:
                attackInfo.Damage = initialValue;
                break;
            case 1:
                attackInfo.Accuracy = initialValue;
                break;
            case 2:
                attackInfo.CritChance = initialValue;
                break;
        }
    }

}