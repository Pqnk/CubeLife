using System;
using System.Collections.Generic;
using Unity.AppUI.Redux;
using UnityEngine;

public class CellBehavior : MonoBehaviour
{
    #region ----INSPECTOR PRIVATE VARIABLES----
    [Header("Serialize Variables")]
    [SerializeField] protected MeshRenderer _aliveCellRenderer;
    [SerializeField] protected MeshRenderer _deadCellRenderer;
    [SerializeField] private GameObject _deadCellPrefab;
    [SerializeField] private GameObject _aliveCellPrefab;
    [SerializeField] private Material _matNormal;
    [SerializeField] private Material _matHL;
    [SerializeField] private List<CellBehavior> _neighborCells = new List<CellBehavior>();
    #endregion

    #region ----PROPERTIES CELL BEHAVIOR----
    public Vector3 CellSize { get { return _aliveCellRenderer.bounds.size; } }
    public bool IsAlive { get; private set; } = false;
    public int XPosOnGrid { get; set; }
    public int YPosOnGrid { get; set; }
    public int ZPosOnGrid { get; set; }
    #endregion

    public bool cellStateNextStep = false;

    public void SetCellState(bool isAlive)
    {
        IsAlive = isAlive;
        _aliveCellPrefab.SetActive(IsAlive);
        _deadCellPrefab.SetActive(!IsAlive);
    }

    public void SetNeighborsCells()
    {
        Vector3[] allPossiblePosition = new Vector3[]
            {
                    new Vector3(    XPosOnGrid + 1,  0,  ZPosOnGrid       ),
                    new Vector3(    XPosOnGrid + 1,  0,  ZPosOnGrid + 1   ),
                    new Vector3(    XPosOnGrid,      0,  ZPosOnGrid + 1   ),
                    new Vector3(    XPosOnGrid - 1,  0,  ZPosOnGrid + 1   ),
                    new Vector3(    XPosOnGrid - 1,  0,  ZPosOnGrid       ),
                    new Vector3(    XPosOnGrid - 1,  0,  ZPosOnGrid - 1   ),
                    new Vector3(    XPosOnGrid,      0,  ZPosOnGrid - 1   ),
                    new Vector3(    XPosOnGrid + 1,  0,  ZPosOnGrid - 1   )
            };

        foreach (Vector3 pos in allPossiblePosition)
        {
            if (pos.x >= 0
                && pos.z >= 0
                && pos.x < GridManager.Instance.GridSize
                && pos.z < GridManager.Instance.GridSize
                )
            {
                _neighborCells.Add(GridManager.Instance.CellGrid[(int)pos.x, (int)pos.z].transform.GetChild(0).GetComponent<CellBehavior>());
            }

            //  Making sure the cells on the border have 8 neighbors as weel, by referencing the other end of the grid (open grid case) : 

            if( pos.x < 0 && (pos.z >= 0 && pos.z < GridManager.Instance.GridSize))
            {
                _neighborCells.Add(GridManager.Instance.CellGrid[GridManager.Instance.GridSize -1, (int)pos.z].transform.GetChild(0).GetComponent<CellBehavior>());
            }
            
            if( pos.x < 0 && pos.z < 0)
            {
                _neighborCells.Add(GridManager.Instance.CellGrid[GridManager.Instance.GridSize - 1, GridManager.Instance.GridSize - 1].transform.GetChild(0).GetComponent<CellBehavior>());
            }
            
            if( (pos.x >= 0 && pos.x < GridManager.Instance.GridSize ) && pos.z < 0)
            {
                _neighborCells.Add(GridManager.Instance.CellGrid[(int)pos.x, GridManager.Instance.GridSize - 1].transform.GetChild(0).GetComponent<CellBehavior>());
            }

            if( pos.x >= GridManager.Instance.GridSize && (pos.z >= 0 && pos.z < GridManager.Instance.GridSize))
            {
                _neighborCells.Add(GridManager.Instance.CellGrid[0, (int)pos.z].transform.GetChild(0).GetComponent<CellBehavior>());
            }

            if( pos.x >= GridManager.Instance.GridSize && pos.z >= GridManager.Instance.GridSize)
            {
                _neighborCells.Add(GridManager.Instance.CellGrid[0, 0].transform.GetChild(0).GetComponent<CellBehavior>());
            }

            if( (pos.x >= 0 && pos.x < GridManager.Instance.GridSize) && pos.z >= GridManager.Instance.GridSize)
            {
                _neighborCells.Add(GridManager.Instance.CellGrid[(int)pos.x, 0].transform.GetChild(0).GetComponent<CellBehavior>());
            }
        }
    }

    public void ApplyGameOifeRules()
    {
        int numNeighborsAlive = GetNumberNeighborsAlive() ;

        if( (numNeighborsAlive == 2 || numNeighborsAlive == 3 ) && this.IsAlive)
        {
            cellStateNextStep = true;
        }
        
        if(numNeighborsAlive == 3 && !this.IsAlive)
        {
            cellStateNextStep = true;
        }

        if((numNeighborsAlive < 2 || numNeighborsAlive > 3))
        {
            cellStateNextStep = false;
        }
    }

    private int GetNumberNeighborsAlive()
    {
        int numNeighborsAlive = 0;

        foreach (CellBehavior neighbor in _neighborCells)
        {
            if (neighbor.IsAlive)
                numNeighborsAlive++;
        }

        return numNeighborsAlive;
    }

    public void UpdateCellStateNextStep()
    {
        SetCellState(cellStateNextStep);
    }

    public void HighLightNeighbors(bool highlight)
    {
        foreach (CellBehavior neighbor in _neighborCells)
        {
            if (highlight)
            {
                neighbor._deadCellRenderer.material = _matHL;
                neighbor._aliveCellRenderer.material = _matHL;
            }
            else
            {
                neighbor._deadCellRenderer.material = _matNormal;
                neighbor._aliveCellRenderer.material = _matNormal;
            }
        }

    }
}
