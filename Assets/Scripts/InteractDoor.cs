using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractDoor : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft; // Reference to the door GameObject.
    [SerializeField] private GameObject doorRight; // Reference to the door GameObject.
    [SerializeField] private TextMeshProUGUI tooltipText;  // Reference to the UI Text for the tooltip.
    [SerializeField] private GameObject toolTipDoor;

    public bool gotKey = false;
    private bool isDoorOpen = false;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    // Display the "Press E to open drawer" tooltip.
                    toolTipDoor.SetActive(true);
                    if (!isDoorOpen)
                    {
                        if (playerInRange && gotKey && Input.GetKeyDown(KeyCode.E))
                        {
                        OpenDoors();
                        isDoorOpen = true;
                        }
                        UpdateUI();
                    }
                    else
                    {
                        toolTipDoor.SetActive(false);
                    }
                }
                else
                {
                    toolTipDoor.SetActive(false);
                }

            }
        }
        else
        {
            toolTipDoor.SetActive(false);
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
    private void OpenDoors()
    {
        // Implement the logic to open the door here (e.g., animate the door).
        // Set isDoorOpen to true when the door is open.
        isDoorOpen = true;
        doorLeft.GetComponent<Animator>().Play("Open");
        doorRight.GetComponent<Animator>().Play("Open");
    }

    private void UpdateUI()
    {
        if (playerInRange)
        {
            toolTipDoor.SetActive(true);
            if (gotKey)
            {
                tooltipText.text = "Open Door";
            }
            else
            {
                tooltipText.text = "Need Key";
            }
        }
        else
        {
            toolTipDoor.SetActive(false);
        }
    }
}
