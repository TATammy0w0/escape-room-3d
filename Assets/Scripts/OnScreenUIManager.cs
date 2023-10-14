using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenUIManager : MonoBehaviour
{
    private GameManager gm;
    // [SerializeField] private GameObject doorUnlockedUI;
    
    private void Start()
    {
        gm = GameManager.instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !GameManager.IsPaused)
        {
            if (GameManager.IsInventoryOpen)
            {
                gm.CloseInventory();
            }
            else
            {
                gm.OpenInventory();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.IsPaused)
            {
                gm.Resume();
            }
            else
            {
                gm.Pause();
            }
        }
        /*
        if (!GameManager.IsDoorLocked)
        {
            doorUnlockedUI.SetActive(true);
        }
        */
    }
}