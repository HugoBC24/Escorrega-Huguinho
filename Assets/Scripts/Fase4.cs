using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase4 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Fase5");
        }
    }
}
