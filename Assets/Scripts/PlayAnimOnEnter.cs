using UnityEngine;

public class PlayAnimOnEnter : MonoBehaviour
{
    [SerializeField] private Animator animatedObject;

    private bool _playerInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_playerInside)
        {
            animatedObject.Play("PlayerIn", 0, 0.0f);
            _playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animatedObject.Play("PlayerOut", 0, 0.0f);
            _playerInside = false;
        }
    }
}
