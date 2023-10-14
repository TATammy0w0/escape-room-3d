using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance
    
    [Header("HUD UI")]
    public GameObject dotCursor;
    public GameObject pauseScreen;
    
    [Header("Player Stats")]
    // public GameObject playerController;
    static public int PlayerHealth = 1;
    
    [Header("Game Stats")]
    static public bool IsInventoryOpen = false;
    static public bool IsPaused = false;
    static public bool IsDoorLocked = true;
    
    
    [Header("Inventory System")] 
    public GameObject inventoryUI;
    [SerializeField] public List<Item> Items = new List<Item>();
    public int inventorySize = 10;
    public Transform ItemContent;
    public GameObject InventoryItem;
    public InventoryItemUIController[] InventoryItems;
    public int Health;
    public TextMeshProUGUI HealthText;

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
        // InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemUIController>();
    }

    public void OpenInventory()
    {   
        //ListItem();
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
        Items.Remove(item);
    }

    public void ListItem()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.ItemName;
            itemIcon.sprite = item.ItemIcon;
        }

        SetInventoryItems();
        Debug.Log("added, array: " + InventoryItems.Length);
    }

    
    public void SetInventoryItems()
    {   
        Debug.Log("InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemUIController>();" + InventoryItems.Length);
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemUIController>();
        Debug.Log("Before forloop InventoryItems size: " + InventoryItems.Length);

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }

        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (InventoryItems[i] != null)
            {
                Debug.Log("array item: " + InventoryItems[i].item + "  " + InventoryItems.Length);
            }
            else
            {
                Debug.LogWarning("Null reference in InventoryItems at index " + i);
            }
        }
    }


    public void IncreaseHealth(int value)
    {
        Health += value;
        HealthText.text = "HP: " + Health;
    }

    public void DecreaseHealth(int value)
    {
        Health -= value;
        HealthText.text = $"HP:{Health}";
    }
}

