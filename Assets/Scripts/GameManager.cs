using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("HUD UI")]
    [SerializeField] private GameObject dotCursor;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject escapeScreen;
    [SerializeField] private GameObject doorUnlockedUI;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject inventoryUI;
    
    [Header("Player Stats")]
    // public GameObject playerController;
    public int health;
    
    [Header("Game Stats")]
    static public bool IsInventoryOpen;
    static public bool IsPaused;
    static public bool IsDoorLocked;
    static public bool IsGameEnd;
    
    [Header("Inventory System")] 
    [SerializeField] public List<Item> itemList = new List<Item>();
    [SerializeField] private int inventorySize = 10;
    [SerializeField] private Transform itemContent;
    [SerializeField] private GameObject inventoryItemButton;
    [SerializeField] public InventoryItemUIController[] inventoryItems;


    private void Awake()
    {
        // Ensure there's only one instance of the GameManager.
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
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
        health = 100;
    }

    private void Start()
    {
        // LockCursor();
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
        if (itemList.Count < inventorySize)
        {
            itemList.Add(item);
        }
        else
        {
            Debug.Log("Cannot add item. Inventory is full.");
        } 
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }

    public void ListItem()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < itemList.Count; i++)
        {
            Debug.Log("Processing Item " + i + ": " + itemList[i].itemName);
            GameObject obj = Instantiate(inventoryItemButton, itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            // Assign the values specific to each item
            itemName.text = itemList[i].itemName;
            itemIcon.sprite = itemList[i].itemIcon;
            Debug.Log("Item name: " + itemName.text);
            // foreach (var child in itemContent.GetComponentsInChildren<InventoryItemUIController>()) Debug.Log("child: " + child.item);
        }
        SetInventoryItems();
    }
    
    public void SetInventoryItems()
    {   
        // Debug.Log("Before, inventoryItems.Length: " + inventoryItems.Length);
        inventoryItems = itemContent.GetComponentsInChildren<InventoryItemUIController>();
        // Debug.Log("After, inventoryItems.Length: " + inventoryItems.Length);

        for (int i = 0; i < itemList.Count; i++)
        {
            inventoryItems[i].AddItem(itemList[i]);
        }
    }

    public void UnlockDoor()
    {
        doorUnlockedUI.SetActive(true);
        IsDoorLocked = false;
        tooltipText.text = "Open Door";
    }

    public void ChangeHealth(int value)
    {
        health += value;
        healthText.text = "HP: " + health;
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

