
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    #region ----SINGLETON INSTANCE GAMEMANAGER----
    public static GameManager Instance { get; private set; }
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

    #region ----INSPECTOR PRIVATE VARIABLES----
    [Header("TO DEFINE : Cell Prefab")]
    [SerializeField] private GameObject _cellPrefab;
    [Header("TO DEFINE : Grid Size")]
    [SerializeField] private int _gridSize = 10;
    #endregion

    #region ----MANAGER VARIABLES----
    [Header("Manager Variables")]
    private GridManager _gridManager;
    #endregion

    void Start()
    {
        InitialiseGridManager();
    }

    private void InitialiseGridManager()
    {
        // Generate the boolean grid and the GameObject grid
        GameObject gridManagerContainer = new GameObject("GridManager");
        gridManagerContainer.transform.position = Vector3.zero;
        gridManagerContainer.transform.SetParent(this.gameObject.transform.parent, false);

        _gridManager = gridManagerContainer.AddComponent<GridManager>();
        _gridManager.InitializeGridManager(_cellPrefab, _gridSize);
    }
}
