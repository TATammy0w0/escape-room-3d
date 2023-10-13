using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    public void Pickup()
    {
        GameManager.instance.AddItem(Item);
        Destroy(gameObject);
    }
}
