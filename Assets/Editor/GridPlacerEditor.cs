using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridPlacer))]
public class GridPlacerEditor : Editor
{
    private void OnSceneGUI()
    {
        GridPlacer placer = (GridPlacer)target;
        if (placer == null || placer.prefabToPlace == null) return;

        Event currentEvent = Event.current;

        // Desativa a seleção padrão do Unity se o Shift estiver pressionado
        if (currentEvent.shift)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }

        // Detecta o clique do botão esquerdo + tecla Shift pressionada
        if (currentEvent.type == EventType.MouseDown && currentEvent.button == 0 && currentEvent.shift)
        {
            // Cria um raio a partir da câmera da Scene em direção ao mouse
            Ray ray = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            
            // Plano de colisão virtual (no chão do cenário, Y = 0)
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float enterDistance))
            {
                Vector3 hitPoint = ray.GetPoint(enterDistance);

                // Alinha as coordenadas ao tamanho da Grid (Snapping)
                float snappedX = Mathf.Round(hitPoint.x / placer.gridSize) * placer.gridSize;
                float snappedZ = Mathf.Round(hitPoint.z / placer.gridSize) * placer.gridSize;
                Vector3 spawnPosition = new Vector3(snappedX, placer.spawnHeight, snappedZ);

                // Instancia o Prefab mantendo o vínculo com o arquivo original (Boa prática do Unity 6)
                GameObject newCube = (GameObject)PrefabUtility.InstantiatePrefab(placer.prefabToPlace);
                newCube.transform.position = spawnPosition;
                newCube.transform.parent = placer.transform; // Organiza embaixo do objeto pai

                // Registra a ação no sistema de desfazer do Unity (Ctrl + Z funciona!)
                Undo.RegisterCreatedObjectUndo(newCube, "Posicionar Bloco no Grid");

                // Consome o evento para o Unity não selecionar outros objetos ao clicar
                currentEvent.Use();
            }
        }
    }
}
