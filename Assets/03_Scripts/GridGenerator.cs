using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public static void GenerateGrid(GameObject cubePrefab, int gridWidth = 10, int gridHeight = 10, float spacing = 0.1f)
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Cube Prefab is not assigned.");
            return;
        }

        Vector3 prefafSize = cubePrefab.GetComponent<BaseCube>().cubeRenderer.bounds.size;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(
                    x * (prefafSize.x + spacing),
                    0,
                    y * (prefafSize.z + spacing)
                );
                Instantiate(cubePrefab, position, Quaternion.identity, cubePrefab.transform);
            }
        }
    }
}
