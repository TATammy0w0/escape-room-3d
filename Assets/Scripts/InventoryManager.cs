using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    public List<Item> collectables = new List<Item>();

    public int inventorySize = 10;
    public Transform ItemContent;
    public GameObject InventoryItem;

    public void AddItem(Item newItem)
    {
        if (collectables.Count < inventorySize)
        {
            collectables.Add(newItem);
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }

    public Item GetItem(int index)
    {
        return collectables[index];
    }

    public void ListItem()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in collectables)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.ItemName;
            itemIcon.sprite = item.ItemIcon;
        }
    }
}
