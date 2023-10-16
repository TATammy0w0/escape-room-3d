using UnityEngine;

public class InventoryItemUIController : MonoBehaviour
{
    public Item item;
    private GameManager _gm;

    void Start()
    {
        _gm = GameManager.Instance;
    }

    public void RemoveItem()
    {
        _gm.RemoveItem(item);
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
                _gm.ChangeHealth(10);
                break;
            case Item.Type.Fish:
                _gm.ChangeHealth(-10);
                break;
            case Item.Type.Key:
                _gm.UnlockDoor();
                break;
        }
        RemoveItem();
        Debug.Log("removed, array: " + _gm.inventoryItems.Length);
    }
}
