using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Fase2");
        }
    }
}
