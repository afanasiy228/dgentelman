using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timereload : MonoBehaviour
{
    [SerializeField] float time = 2;

    public void Invoke()
    {
        Invoke("Reload", time);
    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
