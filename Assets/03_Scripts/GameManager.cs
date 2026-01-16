
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum GameState
{
    Running,
    Paused,
    Stopped
}

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
    [Header("Cell Prefab")]
    [SerializeField] private GameObject _cellPrefab;

    [Header("Grid Size")]
    [SerializeField] private int _gridSize = 10;

    [Header("Game Speed")]
    [Range(1, 100)]
    [SerializeField] private int _gameSpeed = 1;

    [Header("Desired end step")]
    [SerializeField] private int _desiredEndStep = 0;
    #endregion

    #region ----MANAGER VARIABLES----
    [Header("Manager Variables")]
    private GridManager _gridManager;
    #endregion

    #region ----GAMEMANAGER PROPERTIES and VARIABLES----
    private Coroutine _gameCoroutine = null;
    public int NumberStep { get; private set; } = 0;
    public bool GameStarted { get { return GameState == GameState.Running || GameState == GameState.Paused; } }
    public GameState GameState { get; private set; } = GameState.Stopped;
    public bool DesiredEndStepReached { get { return (_desiredEndStep > 0 && NumberStep >= _desiredEndStep); } }
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

    #region ----GAME RUNNING COROUTINE----
    private IEnumerator StartStepCounting()
    {
        while (GameState == GameState.Running && !DesiredEndStepReached)
        {
            yield return new WaitForSeconds((1f / _gameSpeed) * 0.5f);
            NumberStep++;

            if (DesiredEndStepReached)
            {
                StopGame();
            }

            UIManager.Instance.UpdateUI();
        }
    }
    #endregion

    #region ----GAME CONTROL METHODS----

    public void ToggleStartPauseGame()
    {
        switch (GameState)
        {
            case GameState.Running:
                PauseGame();
                break;

            case GameState.Paused:
                ResumeGame();
                break;

            case GameState.Stopped:

                if(!DesiredEndStepReached)
                {
                    StartGame();
                }
                break;
        }
    }

    public void StartGame()
    {
        if (_gameCoroutine == null && !GameStarted)
        {
            GameState = GameState.Running;
            _gameCoroutine = StartCoroutine(StartStepCounting());
        }
    }

    public void PauseGame()
    {
        if (_gameCoroutine != null && GameStarted)
        {
            GameState = GameState.Paused;
            StopCoroutine(_gameCoroutine);
        }
    }

    public void ResumeGame()
    {
        if (_gameCoroutine != null && GameStarted)
        {
            GameState = GameState.Running;
            _gameCoroutine = StartCoroutine(StartStepCounting());
        }
    }

    public void StopGame()
    {
        if (_gameCoroutine != null && GameStarted)
        {
            GameState = GameState.Stopped;
            StopCoroutine(_gameCoroutine);
            _gameCoroutine = null;
        }
    }

    public void ResetGame()
    {
        if (_gameCoroutine != null)
        {
            StopCoroutine(_gameCoroutine);
            _gameCoroutine = null;
        }
        NumberStep = 0;
        GridManager.Instance.ResetGridToAllDead();
        GameState = GameState.Stopped;
    }

    #endregion


}
