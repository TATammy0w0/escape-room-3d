using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemUIController : MonoBehaviour
{
    public Item item;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    public void RemoveItem()
    {
        gm.RemoveItem(item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {   
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.Type.Apple:
                gm.IncreaseHealth(1);
                break;
            case Item.Type.Fish:
                gm.DecreaseHealth(1);
                break;
            case Item.Type.Key:
                gm.UnlockDoor();
                break;
        }
        RemoveItem();
        Debug.Log("removed, array: " + gm.InventoryItems.Length);
    }
}
