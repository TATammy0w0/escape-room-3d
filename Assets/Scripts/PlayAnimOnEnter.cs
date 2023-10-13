using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimOnEnter : MonoBehaviour
{
    [SerializeField] private Animator animatedObject = null;

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerInside)
        {
            animatedObject.Play("PlayerIn", 0, 0.0f);
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animatedObject.Play("PlayerOut", 0, 0.0f);
            playerInside = false;
        }
    }
}
