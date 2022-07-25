using UnityEngine;
using UnityEngine.Events;

public class danger : MonoBehaviour
{
    [SerializeField] private UnityEvent playerEnterEvent;
    [SerializeField] private UnityEvent playerExitEvent;

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEnterEvent.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerExitEvent.Invoke();
        }
    }
}
