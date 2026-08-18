using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase3 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Fase4");
        }
    }
}
