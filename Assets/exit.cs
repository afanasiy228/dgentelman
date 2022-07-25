using UnityEngine;

public class exit : MonoBehaviour
{
    [SerializeField] private string targetSceneName = "";
    [SerializeField] private float reloadTime = 0.5f;

    public object SceneLoader { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("Reload", reloadTime);
        }
    }


    private void Reload()
    {
        exit2.LoadScene(targetSceneName);
    }
}

