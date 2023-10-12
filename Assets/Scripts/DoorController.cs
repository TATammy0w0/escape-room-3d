using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Animator rightDoor = null;
    [SerializeField] private Animator leftDoor = null;
    [SerializeField] private bool gotKey = true;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject toolTip;

    private bool playerInRange = false;
    private bool doorIsOpen = false;

    private void Update()
    {
        if (!doorIsOpen)
        {
            if (playerInRange && gotKey && Input.GetKeyDown(KeyCode.E))
            {
                OpenDoors();
                doorIsOpen = true;
            }
            UpdateUI();
        }
        else
        {
            toolTip.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void OpenDoors()
    {
        //toolTip.SetActive(false);
        rightDoor.Play("OpenDoor", 0, 0.0f);
        leftDoor.Play("OpenDoor", 0, 0.0f);
        //gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (playerInRange)
        {
            toolTip.SetActive(true);
            if (gotKey)
            {
                messageText.text = "Open Door";
            }
            else
            {
                messageText.text = "Need Key";
            }
        }
        else
        {
            toolTip.SetActive(false);
        }
    }
}

