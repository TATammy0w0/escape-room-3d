using UnityEngine;
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

    private Camera _cam;
    private GameManager _gm = GameManager.Instance;

    private bool _isDrawerOpen;
    private bool _isDoorOpen;
    private bool _isPlayerInRange;
    LayerMask _collectibleLayer;

    private void Initialization()
    {
        key.SetActive(false);
        toolTip.SetActive(false);
        _isDoorOpen = false;
        _isDoorOpen = false;
        _isPlayerInRange = false;
    }
    void Start()
    {
        GetReferences();
        Initialization();
    }

    private void Update()
    {
        // check if player is in range for something interactable
        if (_isPlayerInRange)
        { 
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            // priority checking - so that item in _collectibleLayer won't be blocked by item in other layers
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _collectibleLayer))
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
                        Item newItem = hit.transform.GetComponent<ItemObject>().item;
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

                    if (!_isDrawerOpen)
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

                    if (!_isDoorOpen)
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
            _isPlayerInRange = true;
        }
        else if (other.CompareTag("WinTrigger"))
        {
            _gm.PlayerEscape();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactables"))
        {
            _isPlayerInRange = false;
            toolTip.SetActive(false);
        }
    }

    private void OpenDrawer()
    {
        // Implement the logic to open the drawer here (e.g., animate the drawer).
        // Set _isDrawerOpen to true when the drawer is open.
        _isDrawerOpen = true;
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
        // Set _isDrawerOpen to false when the drawer is closed.
        _isDrawerOpen = false;
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
        // Set _isDoorOpen to true when the door is open.
        _isDoorOpen = true;
        doorLeft.GetComponent<Animator>().Play("Open");
        doorRight.GetComponent<Animator>().Play("Open");
        GameManager.IsDoorLocked = true;
    }

    private void AddToInventory(Item item)
    {
        _gm.AddItem(item);
        _gm.ListItem();
    }
    private void GetReferences()
    {
        _gm = GameManager.Instance;
        //inventory = GetComponent<InventoryManager>();
        _collectibleLayer = LayerMask.GetMask("Collectibles");
        _cam = Camera.main;
    }
}
