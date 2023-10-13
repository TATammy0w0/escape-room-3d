using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenUIManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !GameManager.IsPaused)
        {
            if (GameManager.IsInventoryOpen)
            {
                GameManager.instance.CloseInventory();
            }
            else
            {
                GameManager.instance.OpenInventory();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.IsPaused)
            {
                GameManager.instance.Resume();
            }
            else
            {
                GameManager.instance.Pause();
            }
        }
    }
}

