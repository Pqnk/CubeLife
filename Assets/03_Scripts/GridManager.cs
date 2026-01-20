using System;
using System.IO;
using Unity.AppUI.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
    public int GridSize { get; private set; } = 10;
    public bool[,] BoolGrid { get; private set; }
    public GameObject[,] CellGrid { get; private set; }

    private GameObject gridContainer = null;

    public GameObject cellPrefab;

    public bool GridGenerated { get { return gridContainer != null; } }
    #endregion
    /*
    #region ----EVENTS MANAGEMENT----
    public static event Action<Vector3> OnGridParametersChanged;

    public static void EmitGridParameters(Vector3 position)
    {
        OnGridParametersChanged?.Invoke(position);
    }
    #endregion
    */
    public void InitializeGridManager(int gridSize)
    {
        GridSize = gridSize;
        BoolGrid = new bool[GridSize, GridSize];
        CellGrid = new GameObject[GridSize, GridSize];
        InstantiateDeadCellsGrid();
    }
    
    private void InstantiateDeadCellsGrid()
    {
        Vector3 cellSize = cellPrefab.GetComponent<CellBehavior>().CellSize;

        gridContainer = new GameObject("----GRIDCONTAINER----");
        gridContainer.transform.position = Vector3.zero;
        gridContainer.layer = LayerMask.NameToLayer("Cells");

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

                GameObject empty = new GameObject("Cell_" + x + "_" + "0" + "_" + z);
                empty.layer = LayerMask.NameToLayer("Cells");
                empty.transform.SetParent(gridContainer.transform, false);
                empty.transform.position = position;
                CellGrid[x, z] = empty;
                GameObject newCell = Instantiate(cellPrefab, CellGrid[x, z].transform.position, Quaternion.identity, CellGrid[x, z].transform);
                newCell.layer = LayerMask.NameToLayer("Cells");
                newCell.GetComponent<CellBehavior>().SetCellState(false);
                newCell.GetComponent<CellBehavior>().XPosOnGrid = x;
                newCell.GetComponent<CellBehavior>().ZPosOnGrid = z;
            }
        }

        foreach (GameObject c in CellGrid)
        {
            c.transform.GetChild(0).GetComponent<CellBehavior>().SetNeighborsCells();
        }

        if (GridSize > 0)
            EventManager.EmitGridParameters(CellGrid[GridSize - 1, GridSize - 1].transform.position);
    }

    public void ResetGridToAllDead()
    {
        foreach (GameObject c in CellGrid)
        {
            c.transform.GetChild(0).GetComponent<CellBehavior>().SetCellState(false);
        }
    }

    public void DeleteGrid()
    {
        if (gridContainer != null)
            Destroy(gridContainer);
    }

    public void ApplyGameOfLifeRulesForEachCell()
    {
        foreach (GameObject c in CellGrid)
        {
            c.transform.GetChild(0).GetComponent<CellBehavior>().ApplyGameOifeRules();
        }
    }

    public void UpdateAllCellStatesNextStep()
    {
        foreach (GameObject c in CellGrid)
        {
            c.transform.GetChild(0).GetComponent<CellBehavior>().UpdateCellStateNextStep();
        }
    }

    public void ResetAllCellMaterials()
    {
        foreach (GameObject c in CellGrid)
        {
            c.transform.GetChild(0).GetComponent<CellBehavior>().HighLightNeighbors(false);
        }
    }
}
