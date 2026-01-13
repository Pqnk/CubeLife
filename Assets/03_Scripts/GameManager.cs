
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject iconPrefab;

    [Header("Grid Dimensions")]
    [SerializeField] private int gridSize = 10;

    [Header("Cube In-Between Space")]
    [SerializeField] private float spacing = 0.1f;

    [Header("Cube WaitTime between Instantiation")]
    [SerializeField] private float waitTime = 0.2f;

    void Start()
    {
        //StartCubeLifeGame();
        StartCubeLifeGameGrid();
    }


    #region ----CUBE LIFE STARTING GAME----
    private void StartCubeLifeGame()
    {
        // Generate the grid of cubes
        GridGenerator gridGenerator = this.gameObject.AddComponent<GridGenerator>();
        StartCoroutine(gridGenerator.GenerateGrid(cubePrefab, gridSize, spacing, waitTime));
    }

    private void StartCubeLifeGameGrid()
    {
        // Generate the grid of cubes
        GridManager gridManager = this.gameObject.AddComponent<GridManager>();
        gridManager.InitializeGrid(cubePrefab, iconPrefab);
    }

    #endregion
}
