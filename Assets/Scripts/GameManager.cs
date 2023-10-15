using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance
    
    [Header("HUD UI")]
    public GameObject dotCursor;
    public GameObject pauseScreen;
    public GameObject escapeScreen;
    public GameObject doorUnlockedUI;
    [SerializeField] private TextMeshProUGUI tooltipText;
    
    [Header("Player Stats")]
    // public GameObject playerController;
    static public int PlayerHealth = 1;
    
    [Header("Game Stats")]
    static public bool IsInventoryOpen = false;
    static public bool IsPaused = false;
    static public bool IsDoorLocked = true;
    static public bool IsGameEnd = false;
    
    
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

    private void Initialization()
    {
        dotCursor.SetActive(true);
        pauseScreen.SetActive(false);
        escapeScreen.SetActive(false);
        doorUnlockedUI.SetActive(false);
        inventoryUI.SetActive(false);
        IsPaused = false;
        IsInventoryOpen = false;
        IsDoorLocked = true;
        IsGameEnd = false;

    }
    private void Start()
    {
        LockCursor();
        Initialization();
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

    public void PlayerEscape()
    {
        IsGameEnd = true;
        escapeScreen.SetActive(true);
        UnlockCursor();
    }

    public void QuitGame()
    {
        Application.Quit();
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

        for (int i = 0; i < Items.Count; i++)
        {
            Debug.Log("Processing Item " + i + ": " + Items[i].ItemName);
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            // Assign the values specific to each item
            itemName.text = Items[i].ItemName;
            itemIcon.sprite = Items[i].ItemIcon;
            Debug.Log("Item name: " + itemName.text);
            // foreach (var child in ItemContent.GetComponentsInChildren<InventoryItemUIController>()) Debug.Log("child: " + child.item);
        }

        SetInventoryItems();
    }

    
    public void SetInventoryItems()
    {   
        // Debug.Log("Before, InventoryItems.Length: " + InventoryItems.Length);
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemUIController>();
        // Debug.Log("After, InventoryItems.Length: " + InventoryItems.Length);

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }

    public void UnlockDoor()
    {
        doorUnlockedUI.SetActive(true);
        IsDoorLocked = false;
        tooltipText.text = "Open Door";
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
    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

