using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Transform body;
    public float mouseSensitivity = 250;
    private float xRot;

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.IsPaused && !GameManager.IsInventoryOpen) {
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90, 90);

        body.Rotate(new Vector3(0, mouseX, 0));
    }
}
