using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject key; // Reference to the key GameObject.
    [SerializeField] private GameObject drawer; // Reference to the drawer GameObject.
    [SerializeField] private GameObject doorLeft; // Reference to the left door GameObject.
    [SerializeField] private GameObject doorRight; // Reference to the right door GameObject.
    [SerializeField] private TextMeshProUGUI tooltipText; // Reference to the UI Text for the tooltip.
    [SerializeField] private GameObject toolTip;

    private bool isDrawerOpen = false;
    private bool isDoorOpen = false;
    private bool playerInRange = false;
    private bool gotKey = false;
    LayerMask collectibleLayer;

    void Start()
    {
        // Create a layer mask that includes all layers except the "Drawer" layer.
        collectibleLayer = LayerMask.GetMask("Collectibles");
    }

    private void Update()
    {
        if (playerInRange)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, collectibleLayer))
            {
                if (hit.collider.CompareTag("Key") && isDrawerOpen)
                {
                    toolTip.SetActive(true);
                    tooltipText.text = "Collect Key";

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        CollectKey();
                    }
                }
            }            
            else if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Drawer"))
                {
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
                    toolTip.SetActive(true);

                    if (!isDoorOpen)
                    {
                        tooltipText.text = gotKey ? "Open Door" : "Need Key";
                        if (gotKey && Input.GetKeyDown(KeyCode.E))
                        {
                            OpenDoors();
                        }
                    }
                    else
                    {
                        toolTip.SetActive(false);
                    }
                }
                else
                {
                    toolTip.SetActive(false);
                }
            }
        }
        else
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactables"))
        {
            playerInRange = false;
            toolTip.SetActive(false);
        }
    }

    private void CollectKey()
    {
        gotKey = true;
        key.SetActive(false);
        tooltipText.text = "Open Door";
    }

    private void OpenDrawer()
    {
        // Implement the logic to open the drawer here (e.g., animate the drawer).
        // Set isDrawerOpen to true when the drawer is open.
        isDrawerOpen = true;
        drawer.GetComponent<Animator>().Play("Open");
    }

    private void CloseDrawer()
    {
        // Implement the logic to close the drawer here (e.g., animate the drawer).
        // Set isDrawerOpen to false when the drawer is closed.
        isDrawerOpen = false;
        drawer.GetComponent<Animator>().Play("Close");
    }

    private void OpenDoors()
    {
        // Implement the logic to open the door here (e.g., animate the door).
        // Set isDoorOpen to true when the door is open.
        isDoorOpen = true;
        doorLeft.GetComponent<Animator>().Play("Open");
        doorRight.GetComponent<Animator>().Play("Open");
        gotKey = false;
    }
}
