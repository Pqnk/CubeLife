using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public sealed class GridManager : MonoBehaviour
{
    #region ----SINGLETON INSTANCE GRIDMANAGER----
    public static GridManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    #region ----GRID MANAGER PROPERTIES----
    [Header("Grid Manager Properties")]
    public int GridSize { get; private set; }
    public bool[,] BoolGrid { get; private set; }
    public GameObject[,] CellGrid { get; private set; }
    #endregion

    public void InitializeGridManager(GameObject cellPrefab, int gridSize)
    {
        GridSize = gridSize;
        BoolGrid = new bool[GridSize, GridSize];
        CellGrid = new GameObject[GridSize, GridSize];
        InstantiateDeadCellsGrid(cellPrefab);
    }

    public void InstantiateDeadCellsGrid(GameObject cellPrefab)
    {
        Vector3 cellSize = cellPrefab.GetComponent<CellBehavior>().CellSize;

        GameObject container = new GameObject("----GRIDCONTAINER----");
        container.transform.position = Vector3.zero;

        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                BoolGrid[x, z] = false;

                Vector3 position = new Vector3(
                    x * cellSize.x,
                    0f,
                    z * cellSize.z
                );

                GameObject empty = new GameObject("Cell_" + x +"_"+ "0" +"_" + z);
                empty.transform.SetParent(container.transform, false);
                empty.transform.position = position;
                CellGrid[x, z] = empty;
                GameObject newCell = Instantiate(cellPrefab, CellGrid[x, z].transform.position, Quaternion.identity, CellGrid[x, z].transform);
                newCell.GetComponent<CellBehavior>().SetCellState(false);
                newCell.GetComponent<CellBehavior>().XPosOnGrid = x;
                newCell.GetComponent<CellBehavior>().ZPosOnGrid = z;
            }
        }

        foreach(GameObject c in CellGrid)
        {
           c.transform.GetChild(0).GetComponent<CellBehavior>().SetNeighborsCells();
        }
    }

    public void ResetGridToAllDead()
    {
        foreach (GameObject c in CellGrid)
        {
            c.transform.GetChild(0).GetComponent<CellBehavior>().SetCellState(false);
        }
    }
}
