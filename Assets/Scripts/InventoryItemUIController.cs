using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemUIController : MonoBehaviour
{
    public Item Item;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    public void RemoveItem()
    {
        gm.RemoveItem(Item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        Item = newItem;
    }
}
