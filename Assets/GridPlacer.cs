using UnityEngine;

public class GridPlacer : MonoBehaviour
{
    [Header("Configurações do Grid")]
    public GameObject prefabToPlace;
    public float gridSize = 1.0f;
    public float spawnHeight = 0.5f; // Altura padrão do bloco (Y)
}
