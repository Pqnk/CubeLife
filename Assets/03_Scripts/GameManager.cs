
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cube Prefab")]
    [SerializeField] private GameObject cubePrefab;

    [Header("Grid Dimensions")]
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;

    [Header("Cube In-Between Space")]
    [SerializeField] private float spacing = 0.1f;


    #region Start/ Update

    void Start()
    {
        try
        {
            GridGenerator.GenerateGrid(cubePrefab, gridWidth, gridHeight, spacing);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error generating grid: {ex.Message}");
        }
    }

    void Update()
    {

    }

    #endregion

}
