using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance
    
    public GameObject dotCursor;
    public GameObject playerController;
    public GameObject pauseScreen;
    

    static public bool IsInventoryOpen = false;
    static public bool IsPaused = false;
    static public bool IsDoorLocked = true;
    static public int PlayerHealth = 1;

    // Inventory Manager
    public GameObject inventoryUI;
    static public List<Item> Items = new List<Item>();
    public int inventorySize = 10;
    public Transform ItemContent;
    public GameObject InventoryItem;
    private void Awake()
    {
        // Ensure there's only one instance of the GameManager.
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LockCursor();
        IsPaused = false;
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
        dotCursor.SetActive(false);
        //playerController.SetActive(false);
        IsInventoryOpen = true;
        UnlockCursor();
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
        //playerController.SetActive(true);
        dotCursor.SetActive(true);
        IsInventoryOpen = false;
        LockCursor();
    }

    public void Pause()
    {
        IsPaused = true;
        pauseScreen.SetActive(true);
        UnlockCursor();
    }

    public void Resume()
    {
        IsPaused = false;
        pauseScreen.SetActive(false);
        //dotCursor.SetActive(true);
        //playerController.SetActive(true);
        LockCursor();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void AddItem(Item item)
    {
        if (Items.Count < inventorySize)
        {
            Items.Add(item);
        }
        else
        {
            Debug.Log("Cannot add item. Inventory is full.");
        }
        
    }

    public void RemoveItem(Item item)
    {
        if (item != null)
        {
            Items.Remove(item);
            Destroy(item.Prefab);
        }
        else
        {
            Debug.Log("No item to be removed");
        }
    }

    public void ListItem()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in GameManager.Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.ItemName;
            itemIcon.sprite = item.ItemIcon;
        }
    }
}

