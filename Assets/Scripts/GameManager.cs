using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public GameObject inventoryUI;
    public GameObject dotCursor;
    public GameObject playerController;
    public GameObject pauseScreen;
    public List<Item> Items = new List<Item>();

    static public bool IsInventoryOpen = false;
    static public bool IsPaused = false;
    static public bool IsDoorLocked = true;

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
        //dotCursor.SetActive(false);
        //playerController.SetActive(false);
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
        Items.Add(item);
    }
}

