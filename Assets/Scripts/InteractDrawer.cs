using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractDrawer : MonoBehaviour
{
    [SerializeField] private GameObject drawer; // Reference to the drawer GameObject.
    [SerializeField] private TextMeshProUGUI tooltipText;  // Reference to the UI Text for the tooltip.
    [SerializeField] private GameObject toolTip;

    private bool isDrawerOpen = false;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Drawer"))
                {
                    // Display the "Press E to open drawer" tooltip.
                    toolTip.SetActive(true);

                    if(!isDrawerOpen)
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
        }
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
        // Implement the logic to open the drawer here (e.g., animate the drawer).
        // Set isDrawerOpen to true when the drawer is open.
        isDrawerOpen = false;
        drawer.GetComponent<Animator>().Play("Close");
    }
}

