using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Animation Reference")]
    [SerializeField] private GameObject key; // Reference to the key GameObject.
    [SerializeField] private GameObject drawer; // Reference to the drawer GameObject.
    [SerializeField] private GameObject doorLeft; // Reference to the left door GameObject.
    [SerializeField] private GameObject doorRight; // Reference to the right door GameObject.
    [SerializeField] private TextMeshProUGUI tooltipText; // Reference to the UI Text for the tooltip.
    [SerializeField] private GameObject toolTip;
    // [SerializeField] private GameObject doorUnlockedUI;

    private Camera cam;
    private GameManager gm = GameManager.instance;

    private bool isDrawerOpen = false;
    private bool isDoorOpen = false;
    private bool playerInRange = false;
    // private InventoryManager inventory;
    LayerMask collectibleLayer;
    //GameManager gameManager = GameManager.instance;

    private void Initialization()
    {
        key.SetActive(false);
        toolTip.SetActive(false);
        isDoorOpen = false;
        isDoorOpen = false;
    }
    void Start()
    {
        GetReferences();
        Initialization();
    }

    private void Update()
    {
        // check if player is in range for something interactable
        if (playerInRange)
        { 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            // priority checking - so that item in collectibleLayer won't be blocked by item in other layers
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, collectibleLayer))
            {
                // if ray hit the object
                if (hit.collider.CompareTag("InventoryObj"))
                {
                    toolTip.SetActive(true);
                    tooltipText.text = "Collect";

                    // press e to collect object
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // add collected item to list and destroy the 3d game obj
                        Item newItem = hit.transform.GetComponent<ItemObject>().Item;
                        // if (newItem != null)
                        Debug.Log("newItem = " + newItem);
                        AddToInventory(newItem);
                        Destroy(hit.transform.gameObject);
                    }
                }
            }            
            else if (Physics.Raycast(ray, out hit)) // checking raycast hit for other non-collectible object
            {
                if (hit.collider.CompareTag("Drawer"))
                {
                    // interaction with drawer
                    toolTip.SetActive(true);

                    if (!isDrawerOpen)
                    {
                        tooltipText.text = "Open Drawer";
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            OpenDrawer();
                        }
                    }
                    else
                    {
                        tooltipText.text = "Close Drawer";
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            CloseDrawer();
                        }
                    }
                }
                else if (hit.collider.CompareTag("Door"))
                {
                    // interaction with door
                    toolTip.SetActive(true);

                    if (!isDoorOpen)
                    {
                        tooltipText.text = GameManager.IsDoorLocked ? "Locked" : "Open Door";
                        if (!GameManager.IsDoorLocked && Input.GetKeyDown(KeyCode.E))
                        {
                            OpenDoors();
                        }
                    }
                    else // door opened, disable tooltip
                    {
                        toolTip.SetActive(false);
                    }
                }
                else // ray cast didn't hit anything with scripted tag, disable tooltip
                {
                    toolTip.SetActive(false);
                }
            }
        }
        else // ray cast didn't hit anything, disable tooltip
        {
            toolTip.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactables"))
        {
            playerInRange = true;
        }
        else if (other.CompareTag("WinTrigger"))
        {
            gm.PlayerEscape();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactables"))
        {
            playerInRange = false;
            toolTip.SetActive(false);
        }
    }

    private void OpenDrawer()
    {
        // Implement the logic to open the drawer here (e.g., animate the drawer).
        // Set isDrawerOpen to true when the drawer is open.
        isDrawerOpen = true;
        drawer.GetComponent<Animator>().Play("Open");
        // activate key when the drawer is opened
        if (key != null)
        {
            key.SetActive(true);
        }
    }

    private void CloseDrawer()
    {
        // Implement the logic to close the drawer here (e.g., animate the drawer).
        // Set isDrawerOpen to false when the drawer is closed.
        isDrawerOpen = false;
        drawer.GetComponent<Animator>().Play("Close");
        // disactivate key if the drawer is closed to avoid raycast error
        if (key != null)
        {
            key.SetActive(false);
        }
        
    }
    private void OpenDoors()
    {
        // Implement the logic to open the door here (e.g., animate the door).
        // Set isDoorOpen to true when the door is open.
        isDoorOpen = true;
        doorLeft.GetComponent<Animator>().Play("Open");
        doorRight.GetComponent<Animator>().Play("Open");
        GameManager.IsDoorLocked = true;
    }

    private void AddToInventory(Item item)
    {
        gm.AddItem(item);
        gm.ListItem();
    }
    private void GetReferences()
    {
        gm = GameManager.instance;
        //inventory = GetComponent<InventoryManager>();
        collectibleLayer = LayerMask.GetMask("Collectibles");
        cam = Camera.main;
    }
}
