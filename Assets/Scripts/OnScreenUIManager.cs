using UnityEngine;

public class OnScreenUIManager : MonoBehaviour
{
    private GameManager _gm;
    
    private void Start()
    {
        _gm = GameManager.Instance;
    }

    private void Update()
    {
        if (!GameManager.IsGameEnd)
        {
            if (Input.GetKeyDown(KeyCode.I) && !GameManager.IsPaused)
            {
                if (GameManager.IsInventoryOpen)
                {
                    _gm.CloseInventory();
                }
                else
                {
                    _gm.OpenInventory();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameManager.IsPaused)
                {
                    _gm.Resume();
                }
                else
                {
                    _gm.Pause();
                }
            }
        }
    }
}