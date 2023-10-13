using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenUIManager : MonoBehaviour
{
    private GameManager gm;
    
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
    }
}