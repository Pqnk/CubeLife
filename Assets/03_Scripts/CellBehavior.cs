using System.Collections.Generic;
using UnityEngine;

public class CellBehavior : MonoBehaviour
{
    #region ----INSPECTOR PRIVATE VARIABLES----
    [Header("Serialize Variables")]
    [SerializeField] private MeshRenderer _aliveCellRenderer;
    [SerializeField] private GameObject _deadCellPrefab;
    [SerializeField] private GameObject _aliveCellPrefab;
    [SerializeField] private List<CellBehavior> _neighborCells = new List<CellBehavior>();
    #endregion

    #region ----PROPERTIES CELL BEHAVIOR----
    public Vector3 CellSize { get { return _aliveCellRenderer.bounds.size; } }
    public bool IsAlive { get; private set; } = false;
    public int XPosOnGrid { get; set; }
    public int YPosOnGrid { get; set; }
    public int ZPosOnGrid { get; set; }
    #endregion

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
        }
    }
}
