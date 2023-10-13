using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractKey : MonoBehaviour
{
    [SerializeField] private GameObject key; // Reference to the door GameObject.
    [SerializeField] private GameObject toolTipKey;

    public bool gotKey = false;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Key"))
                {
                    Debug.Log("Looking at key");
                    toolTipKey.SetActive(true);
                    if (!gotKey)
                    {
                        if (playerInRange && Input.GetKeyDown(KeyCode.R))
                        {
                        CollectKey();
                        }
                        UpdateUI();
                    }
                    else
                    {
                        key.SetActive(false);
                    }
                }
                else
                {
                    toolTipKey.SetActive(false);
                }

            }
        }
        else
        {
            toolTipKey.SetActive(false);
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

    private void CollectKey()
    {
        key.SetActive(false);
        gotKey = true;
        toolTipKey.SetActive(false);
    }

    private void UpdateUI()
    {
        if (playerInRange)
        {
            toolTipKey.SetActive(true);
        }
        else
        {
            toolTipKey.SetActive(false);
        }
    }
}
