using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    public int gridSize = 10;
    public bool[,] grid;
    public GameObject[,] cubeInstances;
    public Vector3 cellSize;

    public void InitializeGrid(GameObject cubePrefab, GameObject iconPrefab)
    {
        grid = new bool[gridSize, gridSize];
        cubeInstances = new GameObject[gridSize, gridSize];

        FullTrueGrid();
        InstantiateCubeWhereTrue(cubePrefab);
        GenerateEmptyMesh(iconPrefab);
    }

    public void FullTrueGrid()
    {
        for(int x = 0; x < gridSize; x++)
        {
            for(int y = 0; y < gridSize; y++)
            {
                grid[x, y] = true;
            }
        }
    }

    public void ClearGrid()
    {
        grid = new bool[gridSize, gridSize];
    }

    public void InstantiateCubeWhereTrue(GameObject prefab)
    {
        cellSize = prefab.GetComponent<BaseCube>().cubeRenderer.bounds.size;

        GameObject container = new GameObject("----GRIDCONTAINER----");
        container.transform.position = Vector3.zero;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if(grid[x, y])
                {
                    if (!grid[x, y])
                        continue;

                    Vector3 position = new Vector3(
                        x * cellSize.x,
                        0f,
                        y * cellSize.y
                    );

                    //Instantiate(prefab, position, Quaternion.identity);
                    GameObject empty = new GameObject("Cell_"+x+"_"+y);
                    empty.transform.SetParent(container.transform, false);
                    empty.transform.localPosition = position;
                    cubeInstances[x, y] = empty;
                }
            }
        }
    }

    public void GenerateEmptyMesh(GameObject iconPrefab)
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Instantiate(iconPrefab, cubeInstances[x, y].transform.position, Quaternion.identity, cubeInstances[x, y].transform);
            }
        }
    }
}
